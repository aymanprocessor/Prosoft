using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Treasury
{
    public class AccSafeCashRepo : Repository<AccSafeCash, int>, IAccSafeCashRepo
    {

        private readonly IMapper _mapper;
        public AccSafeCashRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<AccSafeCashViewDTO>> GetAccSafeCashAsync(string docType, string flagType, int fYear, int safeCode)
        {
            List<AccSafeCashViewDTO> accSafeCashDTOs = new List<AccSafeCashViewDTO>();

            if (flagType == "oneANDtwo")
            {
                accSafeCashDTOs = await _Context.AccSafeCashes
                    .Where(obj => obj.DocType == docType && (obj.Flag == 1 || obj.Flag == 2) && obj.FYear==fYear && obj.SafeCode== safeCode)
                    .Select(obj => new AccSafeCashViewDTO()
                    {
                        SafeCashId = obj.SafeCashId,
                        DocNo = (int)obj.DocNo,
                        SafeName = obj.SafeName.SafeNames,
                        DocDate = obj.DocDate,
                        PersonName = obj.PersonName,
                        ValuePay = obj.ValuePay,
                        Commentt = obj.Commentt,
                    }).ToListAsync();
            }
            else if (flagType == "oneANDtwoAndthree")
            {
                accSafeCashDTOs = await _Context.AccSafeCashes
                    .Where(obj => obj.DocType == docType && (obj.Flag == 1 || obj.Flag == 2 || obj.Flag == 3))
                    .Select(obj => new AccSafeCashViewDTO()
                    {
                        SafeCashId = obj.SafeCashId,
                        DocNo = (int)obj.DocNo,
                        SafeName = obj.SafeName.SafeNames,
                        DocDate = obj.DocDate,
                        PersonName = obj.PersonName,
                        ValuePay = obj.ValuePay,
                        Commentt = obj.Commentt,
                    }).ToListAsync();
            }

            return accSafeCashDTOs;
        }

        public async Task<int> GetNewSerialAsync(string docType, int safeCode, int fYear)
        {
            int newSerial;
            var accSafeCash = await _Context.AccSafeCashes
                .Where(obj => obj.DocType == docType && obj.SafeCode == safeCode && obj.FYear == fYear).CountAsync();
           
            if (accSafeCash != 0)
            {
                //var lastID = await _DbSet.MaxAsync(obj => obj.DocNo);
                newSerial = (int)accSafeCash + 1;
            }
            else
                newSerial = 1;
            return newSerial;
        }
        public async Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode)
        {
            List<AccSubCode> subAccCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == mainAccCode).ToListAsync();
            var subAccCodesDTO = _mapper.Map<List<AccSubCodeDTO>>(subAccCodes);
            return subAccCodesDTO;
        }

        public async Task<AccSafeCashEditAddDTO> GetEmptyAccSafeCashAsync(int userCode)
        {
            AccSafeCashEditAddDTO accSafeCashDTO = new AccSafeCashEditAddDTO();
            
            List<JournalType> journalTypes = new List<JournalType>();
            List<UserJournalType> userJournalTypes = await _Context.UserJournalTypes.Where(obj => obj.UserCode == userCode).ToListAsync();
            List<JournalType> allJournalTypes = await _Context.JournalTypes.ToListAsync();
            foreach (var jou in allJournalTypes)
            {
                var isExisted = false;
                foreach (var user in userJournalTypes)
                {
                    if (jou.JournalCode == user.JournalCode)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (isExisted)
                {
                    journalTypes.Add(jou);
                }
            }
            List<SafeName> safeNames = new List<SafeName>();
            List<UserCashNo> userCashNos = await _Context.userCashNos.Where(obj => obj.UserCode == userCode).ToListAsync();
            List<SafeName> allsafeNames = await _Context.SafeNames.ToListAsync();
            foreach (var jou in allsafeNames)
            {
                var isExisted = false;
                foreach (var user in userCashNos)
                {
                    if (jou.SafeCode == user.SafeCode)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (isExisted)
                {
                    safeNames.Add(jou);
                }
            }
            List<GTable> gTables = await _Context.gTables.Where(obj => obj.Flag == 30).ToListAsync();
            List<CostCenter> costCenters = await _Context.CostCenters.ToListAsync();
            List<AccGlobalDef> accGlobalDefs = await _Context.accGlobalDefs.ToListAsync();
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes.ToListAsync();
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.ToListAsync();

            accSafeCashDTO.journalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            accSafeCashDTO.gTablels = _mapper.Map<List<GTablelDTO>>(gTables);
            accSafeCashDTO.costCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            accSafeCashDTO.treasuryNames = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames);
            accSafeCashDTO.accGlobalDefs = _mapper.Map<List<AccGlobalDefDTO>>(accGlobalDefs);
            accSafeCashDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);
            accSafeCashDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);
            
            return accSafeCashDTO;
        }

        public async Task AddAccSafeCashAsync(AccSafeCashEditAddDTO accSafeCashDTO)
        {
            AccSafeCash accSafeCash = _mapper.Map<AccSafeCash>(accSafeCashDTO);
            //accSafeCash.DocType = "SFCIN";
            if (accSafeCash.DocType == "SFCIN")
            {
                accSafeCash.MCodeDtl = 31;
            }
            else if (accSafeCash.DocType == "SFCOT")
            {
                accSafeCash.MCodeDtl = 32;
            }
            else if (accSafeCash.DocType == "SFTIN")
            {
                accSafeCash.MCodeDtl = 35;
            }
            else if (accSafeCash.DocType == "SFTOT")
            {
                accSafeCash.MCodeDtl = 36;
            }
            accSafeCash.SerId = 1;
            accSafeCash.EntryDate = DateTime.Now;

            await _Context.AddAsync(accSafeCash);
            await _Context.SaveChangesAsync();

            ///Posting/// 
        }

        public async Task<AccSafeCashEditAddDTO> GetAccSafeCashByIdAsync(int id,int userCode)
        {
            AccSafeCash accSafeCash = await _Context.AccSafeCashes.FirstOrDefaultAsync(obj => obj.SafeCashId == id);

            AccSafeCashEditAddDTO accSafeCashDTO = _mapper.Map<AccSafeCashEditAddDTO>(accSafeCash);

            List<JournalType> journalTypes = new List<JournalType>();
            List<UserJournalType> userJournalTypes = await _Context.UserJournalTypes.Where(obj => obj.UserCode == userCode).ToListAsync();
            List<JournalType> allJournalTypes = await _Context.JournalTypes.ToListAsync();
            foreach (var jou in allJournalTypes)
            {
                var isExisted = false;
                foreach (var user in userJournalTypes)
                {
                    if (jou.JournalCode == user.JournalCode)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (isExisted)
                {
                    journalTypes.Add(jou);
                }
            }
            List<SafeName> safeNames = new List<SafeName>();
            List<UserCashNo> userCashNos = await _Context.userCashNos.Where(obj => obj.UserCode == userCode).ToListAsync();
            List<SafeName> allsafeNames = await _Context.SafeNames.ToListAsync();
            foreach (var jou in allsafeNames)
            {
                var isExisted = false;
                foreach (var user in userCashNos)
                {
                    if (jou.SafeCode == user.SafeCode)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (isExisted)
                {
                    safeNames.Add(jou);
                }
            }
            List<GTable> gTables = await _Context.gTables.Where(obj => obj.Flag == 30).ToListAsync();
            List<CostCenter> costCenters = await _Context.CostCenters.ToListAsync();
            List<AccGlobalDef> accGlobalDefs = await _Context.accGlobalDefs.ToListAsync();
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes.ToListAsync();
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.Where(obj => obj.MainCode == accSafeCash.MainCode).ToListAsync();

            accSafeCashDTO.journalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            accSafeCashDTO.gTablels = _mapper.Map<List<GTablelDTO>>(gTables);
            accSafeCashDTO.costCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            accSafeCashDTO.treasuryNames = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames);
            accSafeCashDTO.accGlobalDefs = _mapper.Map<List<AccGlobalDefDTO>>(accGlobalDefs);
            accSafeCashDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);
            accSafeCashDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);

            return accSafeCashDTO;
        }

        public async Task EditAccSafeCashAsync(int id, AccSafeCashEditAddDTO accSafeCashDTO)
        {

            AccSafeCash accSafeCash = await _Context.AccSafeCashes.FirstOrDefaultAsync(obj => obj.SafeCashId == id);
            _mapper.Map(accSafeCashDTO, accSafeCash);
            //accSafeCash.DocType = "SFCIN";
            if (accSafeCash.DocType == "SFCIN")
            {
                accSafeCash.MCodeDtl = 31;
            }
            else if (accSafeCash.DocType == "SFCOT")
            {
                accSafeCash.MCodeDtl = 32;
            }
            else if (accSafeCash.DocType == "SFTIN")
            {
                accSafeCash.MCodeDtl = 35;
            }
            else if (accSafeCash.DocType == "SFTOT")
            {
                accSafeCash.MCodeDtl = 36;
            }
            accSafeCash.SerId = 1;
            accSafeCash.EntryDate = DateTime.Now;
            _Context.Update(accSafeCash);
            await _Context.SaveChangesAsync();
        }

        public async Task<bool> HasRelatedDataAsync(int id, string doctype)
        {
            // Check if there are related records in custCollectionsDiscounts
            var hasRelatedData = await _Context.custCollectionsDiscounts.AnyAsync(p => p.SafeCashId == id && p.DocType ==doctype);
            return hasRelatedData;
        }

        //public async Task<PostingAccMasterMINIDTO> PostingToAcctransMaster(AccSafeCash accSafeCash)
        //{
        //    SystemTable systemTable = await _Context.SystemTables.FindAsync(35);
        //    var userCode = accSafeCash.UserCode;
        //    var depositId = accSafeCash.DpsSer;
        //    PostingAccMasterMINIDTO postingAccMasterMINIDTO = new PostingAccMasterMINIDTO();


        //    if (systemTable.SysValue == 1)
        //    {
        //        UserCashNo userCashNo = await _Context.userCashNos.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
        //        UserJournalType userJournalType = await _Context.UserJournalTypes.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
        //        AccSafeCash accSafeCash = await _Context.AccSafeCashes
        //         .FirstOrDefaultAsync(obj => obj.BranchId == deposit.BranchId && obj.FYear == deposit.FYear && obj.MasterId == deposit.MasterId && obj.DocType == "SFCIN" && obj.DocNo == deposit.SafeDocNo && obj.Flag == 2 && obj.SafeCode == deposit.CashNo);

        //        int newDocNo = 0;
        //        int jorKiedNo;

        //        if (deposit.JorKiedNo == null)
        //        {
        //            List<AccTransMaster> accTransMasters = await _Context.AccTransMasters
        //                    .Where(obj => obj.CoCode == deposit.BranchId && obj.FYear == deposit.FYear && obj.TransType == userJournalType.JournalCode.ToString()).ToListAsync();
        //            if (accTransMasters.Count != 0)
        //            {
        //                jorKiedNo = (int)(accTransMasters.Max(obj => obj.TransNo)) + 1;
        //            }
        //            else { jorKiedNo = 1; }
        //        }
        //        else { jorKiedNo = (int)deposit.JorKiedNo; }


        //        ///////////////////////////////////////////////////

        //        string commentt;
        //        if (deposit.DepositDesc != null)
        //        {
        //            commentt = "مدفوعات مقدمة:" + " " + deposit.DepositDesc;
        //        }
        //        else { commentt = "مدفوعات مقدمة:"; }

        //        /////////////////////////Account Master//////////////////////////////////

        //        AccTransMaster accTransMaster = await _Context.AccTransMasters
        //            .FirstOrDefaultAsync(obj => obj.CoCode == deposit.BranchId && obj.FYear == deposit.FYear && obj.MasterId == deposit.MasterId && obj.TransNo == jorKiedNo && obj.TransType == userJournalType.JournalCode.ToString());

        //        if (accTransMaster != null)
        //        {
        //            List<AccTransDetail> accTransDetails = await _Context.AccTransDetails.Where(obj => obj.TransId == accTransMaster.TransId).ToListAsync();
        //            if (accTransDetails != null)
        //            {
        //                _Context.RemoveRange(accTransDetails);
        //                await _Context.SaveChangesAsync();
        //            }
        //        }
        //        int newTransNo = 0;
        //        if (accTransMaster is not null)
        //        {
        //            newTransNo = (int)accTransMaster.TransNo;
        //            _Context.Remove(accTransMaster);
        //            await _Context.SaveChangesAsync();
        //        }
        //        else if (accTransMaster is null)
        //        {
        //            if (deposit.JorKiedNo == null)
        //            {
        //                List<AccTransMaster> accTransMasters = await _Context.AccTransMasters
        //                    .Where(obj => obj.CoCode == deposit.BranchId && obj.FYear == deposit.FYear && obj.TransType == userJournalType.JournalCode.ToString()).ToListAsync();
        //                if (accTransMasters.Count != 0)
        //                {
        //                    newTransNo = (int)(accTransMasters.Max(obj => obj.TransNo)) + 1;
        //                }
        //                else { newTransNo = 1; }
        //            }
        //            else { newDocNo = (int)deposit.SafeDocNo; }
        //        }
        //        var yearTransNo = (deposit.FYear + "_" + newTransNo).ToString();

        //        //inserting///

        //        AccTransMaster newAccTransMaster = new AccTransMaster();
        //        newAccTransMaster.CoCode = deposit.BranchId;
        //        newAccTransMaster.FYear = deposit.FYear;
        //        newAccTransMaster.TransType = userJournalType.JournalCode.ToString();
        //        newAccTransMaster.TransNo = newTransNo;
        //        newAccTransMaster.TransDate = deposit.DpsDate;
        //        newAccTransMaster.TransDesc = commentt;
        //        newAccTransMaster.TotalTrans = deposit.DpsVal;
        //        newAccTransMaster.OkPost = "0";
        //        newAccTransMaster.CurCode = "1";
        //        newAccTransMaster.CurRate = 1;
        //        newAccTransMaster.YearTransNo = yearTransNo;
        //        DateTime dpsDate = (DateTime)deposit.DpsDate;
        //        int monthNumber = dpsDate.Month;
        //        newAccTransMaster.FMonth = monthNumber;
        //        newAccTransMaster.MasterId = deposit.MasterId;
        //        newAccTransMaster.MCode = deposit.ModId;
        //        newAccTransMaster.PostRecipt = deposit.PostRecipt;
        //        newAccTransMaster.EntryDate = DateTime.Now;
        //        await _Context.AddAsync(newAccTransMaster);
        //        //await _Context.SaveChangesAsync();
        //        int result = await _Context.SaveChangesAsync(); //for return message after enhance method
        //        if (result == 0)
        //        {
        //            postingAccMasterMINIDTO.Message = "Failed to post in Account.";
        //            postingAccMasterMINIDTO.TransNo = 0;
        //            return postingAccMasterMINIDTO;

        //        }
        //        else
        //        {
        //            Deposit depositUpdate = await _Context.Deposits.FindAsync(depositId);
        //            depositUpdate.JorKiedNo = newTransNo;
        //            await _Context.SaveChangesAsync();
        //            postingAccMasterMINIDTO.Message = "Posting to Account Done.";
        //            postingAccMasterMINIDTO.TransNo = newTransNo;
        //            postingAccMasterMINIDTO.TransId = newAccTransMaster.TransId;
        //            return postingAccMasterMINIDTO;
        //        }

        //    }


        //    return postingAccMasterMINIDTO;
        //}
    }
}
