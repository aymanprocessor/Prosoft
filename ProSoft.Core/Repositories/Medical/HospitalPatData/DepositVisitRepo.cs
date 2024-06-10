using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class DepositVisitRepo : Repository<Deposit, int>, IDepositVisitRepo
    {
        private readonly IMapper _mapper;

        public DepositVisitRepo(AppDbContext Context , IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<DepositViewDTO>> GetAllDepositesAsync(int id)
        {
            List<DepositViewDTO> depositDTOs = await _Context.Deposits.Where(obj => obj.MasterId == id)
                 .Select(obj => new DepositViewDTO()
                 {
                     DpsSer = (int)obj.DpsSer,
                     PostRecipt = (int)obj.PostRecipt,
                     DpsType = (int)obj.DpsType,
                     DpsVal =(decimal)obj.DpsVal,
                     DepositDesc = obj.DepositDesc,
                     //EinvType = (int)obj.EinvType,
                     //WinvItemsFlg = obj.WinvItemsFlg,
                 })
                 .ToListAsync();
            return depositDTOs;
        }
        public async Task<PatAdmissionViewDTO> GetPatData(int id)
        {
            PatAdmission patAdmission = await _Context.PatAdmissions.FirstOrDefaultAsync(obj=>obj.MasterId ==id);
            //get pat to get patname
            Pat pat = await _Context.Pats.FirstOrDefaultAsync(obj => obj.PatId == patAdmission.PatId);

            PatAdmissionViewDTO patAdmissionDTO = _mapper.Map<PatAdmissionViewDTO>(patAdmission);
            //set patname
            patAdmissionDTO.PatName = pat.PatName;

            return patAdmissionDTO;
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.DpsSer);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }

        public async Task<DepositEditAddDTO> GetEmptyDepositAsync(int id)
        {
            DepositEditAddDTO depositDTO = new DepositEditAddDTO();

            //filter banks
            EisPosting eisPosting = await _Context.EisPostings.FirstOrDefaultAsync(obj => obj.PostId == 13);
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.Where(obj=>obj.MainCode == eisPosting.MainCode).ToListAsync();

            depositDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);
            return depositDTO;
        }

        public async Task<DepositEditAddDTO> GetDepositByIdAsync(int id)
        {
            Deposit deposit = await _Context.Deposits.FirstOrDefaultAsync(obj=>obj.DpsSer ==id);

            DepositEditAddDTO depositDTO = _mapper.Map<DepositEditAddDTO>(deposit);

            //filter banks
            EisPosting eisPosting = await _Context.EisPostings.FirstOrDefaultAsync(obj => obj.PostId == 13);
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.Where(obj => obj.MainCode == eisPosting.MainCode).ToListAsync();

            depositDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);
            return depositDTO;
        }

        public async Task AddDepositDtllAsync(int id, DepositEditAddDTO depositDTO)
        {
            Deposit deposit = _mapper.Map<Deposit>(depositDTO);
            deposit.MasterId = id;
            PatAdmission patAdmission =await _Context.PatAdmissions.FirstOrDefaultAsync(obj=>obj.MasterId==id);
            deposit.PatId = patAdmission.PatId;
            deposit.ModId = 11;

            await _Context.AddAsync(deposit);
            await _Context.SaveChangesAsync();

            ///////////////////////////////////////////////////////////////////////////////////////////////
            
            SystemTable systemTable = await _Context.SystemTables.FindAsync(35);
            var userCode = depositDTO.UserCode;

            if (systemTable.SysValue == 1)
            {
                UserCashNo userCashNo = await _Context.userCashNos.FirstOrDefaultAsync(obj=>obj.UserCode == userCode);
                UserJournalType userJournalType = await _Context.UserJournalTypes.FirstOrDefaultAsync(obj=>obj.UserCode == userCode);
                AccSafeCash accSafeCash = await _Context.AccSafeCashes
                 .FirstOrDefaultAsync(obj=>obj.BranchId == deposit.BranchId && obj.FYear == deposit.FYear && obj.MasterId==deposit.MasterId && obj.DocType=="SFCIN" && obj.DocNo == deposit.SafeDocNo &&obj.Flag ==2 && obj.SafeCode == deposit.CashNo);

                int newDocNo;
                if (accSafeCash is not null)
                {
                     _Context.Remove(accSafeCash);
                    await _Context.SaveChangesAsync();
                }
                else if(accSafeCash is null)
                {
                    if (deposit.SafeDocNo == null)
                    {
                        List<AccSafeCash> accSafeCashs = await _Context.AccSafeCashes
                            .Where(obj => obj.BranchId == deposit.BranchId && obj.FYear == deposit.FYear && obj.DocType == "SFCIN" && obj.SafeCode == deposit.CashNo).ToListAsync();
                        if (accSafeCashs.Count != 0)
                        {
                            newDocNo = (int)(accSafeCashs.Max(obj => obj.DocNo)) + 1;
                        }
                        else { newDocNo = 1; }
                    }
                    else { newDocNo = (int)deposit.SafeDocNo; }
                }
                int jorKiedNo;

                if (deposit.JorKiedNo == null)
                {
                    List<AccTransMaster> accTransMasters = await _Context.AccTransMasters
                            .Where(obj => obj.CoCode == deposit.BranchId && obj.FYear == deposit.FYear &&  obj.TransType == userJournalType.JournalCode.ToString()).ToListAsync();
                    if (accTransMasters.Count != 0)
                    {
                        jorKiedNo = (int)(accTransMasters.Max(obj => obj.TransNo)) + 1;
                    }
                    else { jorKiedNo = 1; }
                }
                else { jorKiedNo = (int)deposit.JorKiedNo; }

                EisPosting eisPosting = await _Context.EisPostings.FirstOrDefaultAsync(obj=>obj.PostId == 15 && obj.BranchId ==deposit.BranchId);
                var mainCodeEisPosting = eisPosting.MainCode;//الرئيسي
                var subCodeEisPosting = eisPosting.SubCode;//الفرعي

                string commentt;
                if (deposit.DepositDesc !=null)
                {
                    commentt = "مدفوعات مقدمة:" +" "+ deposit.DepositDesc;
                }
                else { commentt = "مدفوعات مقدمة:"; }

                var patName = (await _Context.Pats.FirstOrDefaultAsync(obj=>obj.PatId ==deposit.PatId)).PatName;
                var companyName = (await _Context.Companies.FindAsync(patAdmission.CompId)).CompName;
                if (patAdmission.MainInvNo == null)
                {
                    commentt = "المريض:" + " " + patName + " "+ "الجهة:" + companyName;
                }
                else { commentt = "المريض:" + " " + patName + " " + "الجهة: " + companyName+ " "+ "الرقم: " + patAdmission.MainInvNo; }

            }
        }


        public async Task EditDepositAsync(int id, DepositEditAddDTO depositDTO)
        {
            Deposit deposit = await _Context.Deposits.FirstOrDefaultAsync(obj => obj.DpsSer == id);
            deposit.ModId = 11;
            _mapper.Map(depositDTO, deposit);
            _Context.Update(deposit);
            await _Context.SaveChangesAsync();
        }
    }
}
