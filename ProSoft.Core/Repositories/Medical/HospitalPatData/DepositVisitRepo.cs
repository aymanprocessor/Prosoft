using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
            UserCashNo userCashNo = await _Context.userCashNos.FirstOrDefaultAsync(obj => obj.UserCode == deposit.UserCode);
            deposit.CashNo = userCashNo.SafeCode;

            await _Context.AddAsync(deposit);
            await _Context.SaveChangesAsync();

          var accSafeCashMessage =  await PostingToCash(deposit, patAdmission);

          PostingAccMasterMINIDTO accTransMaster = await PostingToAcctransMaster(deposit, patAdmission);        
          var accTransMasterMessage = accTransMaster.Message;        
          var transNo = accTransMaster.TransNo;        
          var transId = accTransMaster.TransId;        
          var accTransDetailMessage = await PostingToAcctransDetail(deposit, patAdmission, transNo, transId);        
        }
        ///get name of maincode + subcode ////
        private async Task<string> GetAccountName(string mainCode, string subCode)
        {
            AccMainCode myMainCode = await _Context.AccMainCodes
                .FirstOrDefaultAsync(obj => obj.MainCode == mainCode);
            string mainName = myMainCode.MainName;

            AccSubCode mySubCode = await _Context.AccSubCodes
                .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == mainCode);
            string subName = mySubCode! != null ? mySubCode.SubName : string.Empty;

            return subName != "" ? $"{mainName} / {subName}" : mainName;
        }

        public async Task EditDepositAsync(int id, DepositEditAddDTO depositDTO)
        {
            Deposit deposit = await _Context.Deposits.FirstOrDefaultAsync(obj => obj.DpsSer == id);
            deposit.ModId = 11;
            _mapper.Map(depositDTO, deposit);
            _Context.Update(deposit);
            await _Context.SaveChangesAsync();
            PatAdmission patAdmission = await _Context.PatAdmissions.FirstOrDefaultAsync(obj => obj.MasterId == deposit.MasterId);

            var accSafeCashMessage = await PostingToCash(deposit, patAdmission);
            var accTransMasterMessage = await PostingToAcctransMaster(deposit, patAdmission);
        }

        public async Task<string> PostingToCash(Deposit deposit, PatAdmission patAdmission)
        {
            SystemTable systemTable = await _Context.SystemTables.FindAsync(35);
            var userCode = deposit.UserCode;
            var depositId = deposit.DpsSer;


            if (systemTable.SysValue == 1)
            {
                UserCashNo userCashNo = await _Context.userCashNos.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
                UserJournalType userJournalType = await _Context.UserJournalTypes.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
                AccSafeCash accSafeCash = await _Context.AccSafeCashes
                 .FirstOrDefaultAsync(obj => obj.BranchId == deposit.BranchId && obj.FYear == deposit.FYear && obj.MasterId == deposit.MasterId && obj.DocType == "SFCIN" && obj.DocNo == deposit.SafeDocNo && obj.Flag == 2 && obj.SafeCode == deposit.CashNo);

                int newDocNo = 0;
                if (accSafeCash is not null)
                {
                    _Context.Remove(accSafeCash);
                    await _Context.SaveChangesAsync();
                    newDocNo = (int)deposit.SafeDocNo;
                }
                else if (accSafeCash is null)
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
                            .Where(obj => obj.CoCode == deposit.BranchId && obj.FYear == deposit.FYear && obj.TransType == userJournalType.JournalCode.ToString()).ToListAsync();
                    if (accTransMasters.Count != 0)
                    {
                        jorKiedNo = (int)(accTransMasters.Max(obj => obj.TransNo)) + 1;
                    }
                    else { jorKiedNo = 1; }
                }
                else { jorKiedNo = (int)deposit.JorKiedNo; }

                EisPosting eisPosting = await _Context.EisPostings.FirstOrDefaultAsync(obj => obj.PostId == 15 && obj.BranchId == deposit.BranchId);
                var mainCodeEisPosting = eisPosting.MainCode;//الرئيسي
                var subCodeEisPosting = eisPosting.SubCode;//الفرعي
                string nameOfMainAndSub = await GetAccountName(mainCodeEisPosting, subCodeEisPosting);

                ///////////////////////////////////////////////////
                ///
                string commentt;
                if (deposit.DepositDesc != null)
                {
                    commentt = "مدفوعات مقدمة:" + " " + deposit.DepositDesc;
                }
                else { commentt = "مدفوعات مقدمة:"; }

                var patName = (await _Context.Pats.FirstOrDefaultAsync(obj => obj.PatId == deposit.PatId)).PatName;
                var companyName = (await _Context.Companies.FindAsync(patAdmission.CompId)).CompName;
                string description;
                if (patAdmission.MainInvNo == null)
                {
                    description = "المريض:" + " " + patName + " " + "الجهة:" + companyName;
                }
                else { description = "المريض:" + " " + patName + " " + "الجهة: " + companyName + " " + "الرقم: " + patAdmission.MainInvNo; }

                if (deposit.DpsType == 1)
                {
                    //inserting///

                    AccSafeCash newAccSafeCash = new AccSafeCash();
                    newAccSafeCash.BranchId = deposit.BranchId;
                    newAccSafeCash.FYear = deposit.FYear;
                    newAccSafeCash.DocType = "SFCIN";
                    newAccSafeCash.DocNo = newDocNo;
                    newAccSafeCash.DocDate = deposit.DpsDate;
                    newAccSafeCash.CurCode = 1;
                    newAccSafeCash.PersonName = description;
                    newAccSafeCash.ValuePay = deposit.DpsVal;
                    newAccSafeCash.Commentt = commentt;
                    newAccSafeCash.CurSer = 1;
                    newAccSafeCash.Flag = 2;
                    newAccSafeCash.Rate1 = 1;
                    DateTime dpsDate = (DateTime)deposit.DpsDate;
                    int monthNumber = dpsDate.Month;
                    newAccSafeCash.FMonth = monthNumber;
                    newAccSafeCash.SafeCode = deposit.CashNo;
                    newAccSafeCash.MasterId = deposit.MasterId;
                    newAccSafeCash.MainCode = mainCodeEisPosting;
                    newAccSafeCash.SubCode = subCodeEisPosting;
                    newAccSafeCash.AccName = nameOfMainAndSub;
                    newAccSafeCash.AccTransType = userJournalType.JournalCode;
                    newAccSafeCash.DiscountVal = 0;
                    newAccSafeCash.AprovedFlag = "APR";
                    newAccSafeCash.UserCode = deposit.UserCode;
                    newAccSafeCash.EntryType = 1;
                    newAccSafeCash.AccTransNo = jorKiedNo;
                    newAccSafeCash.PostRecipt = deposit.PostRecipt;
                    await _Context.AddAsync(newAccSafeCash);
                    //await _Context.SaveChangesAsync();
                    int result = await _Context.SaveChangesAsync(); //for return message after enhance method
                    if (result == 0)
                    {
                        return "Failed to add the new AccSafeCash record.";
                    }
                    else
                    {
                        Deposit depositUpdate = await _Context.Deposits.FindAsync(depositId);
                        depositUpdate.SafeDocNo = newDocNo;
                        await _Context.SaveChangesAsync();
                        return "AccSafeCash record added successfully.";
                    }
                }
                else
                {
                    Deposit depositUpdate = await _Context.Deposits.FindAsync(depositId);
                    depositUpdate.SafeDocNo =0;
                    await _Context.SaveChangesAsync();                 
                }

            }

            return "";
        }


        public async Task<PostingAccMasterMINIDTO> PostingToAcctransMaster(Deposit deposit, PatAdmission patAdmission)
        {
            SystemTable systemTable = await _Context.SystemTables.FindAsync(35);
            var userCode = deposit.UserCode;
            var depositId = deposit.DpsSer;
            PostingAccMasterMINIDTO postingAccMasterMINIDTO = new PostingAccMasterMINIDTO();


            if (systemTable.SysValue == 1)
            {
                UserCashNo userCashNo = await _Context.userCashNos.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
                UserJournalType userJournalType = await _Context.UserJournalTypes.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
                AccSafeCash accSafeCash = await _Context.AccSafeCashes
                 .FirstOrDefaultAsync(obj => obj.BranchId == deposit.BranchId && obj.FYear == deposit.FYear && obj.MasterId == deposit.MasterId && obj.DocType == "SFCIN" && obj.DocNo == deposit.SafeDocNo && obj.Flag == 2 && obj.SafeCode == deposit.CashNo);

                int newDocNo = 0;
                int jorKiedNo;

                if (deposit.JorKiedNo == null)
                {
                    List<AccTransMaster> accTransMasters = await _Context.AccTransMasters
                            .Where(obj => obj.CoCode == deposit.BranchId && obj.FYear == deposit.FYear && obj.TransType == userJournalType.JournalCode.ToString()).ToListAsync();
                    if (accTransMasters.Count != 0)
                    {
                        jorKiedNo = (int)(accTransMasters.Max(obj => obj.TransNo)) + 1;
                    }
                    else { jorKiedNo = 1; }
                }
                else { jorKiedNo = (int)deposit.JorKiedNo; }


                ///////////////////////////////////////////////////
                
                string commentt;
                if (deposit.DepositDesc != null)
                {
                    commentt = "مدفوعات مقدمة:" + " " + deposit.DepositDesc;
                }
                else { commentt = "مدفوعات مقدمة:"; }

                /////////////////////////Account Master//////////////////////////////////

                AccTransMaster accTransMaster = await _Context.AccTransMasters
                    .FirstOrDefaultAsync(obj => obj.CoCode == deposit.BranchId && obj.FYear == deposit.FYear && obj.MasterId == deposit.MasterId && obj.TransNo == jorKiedNo && obj.TransType == userJournalType.JournalCode.ToString());
              
                if (accTransMaster != null)
                {
                     List<AccTransDetail> accTransDetails = await _Context.AccTransDetails.Where(obj => obj.TransId == accTransMaster.TransId).ToListAsync();
                    if (accTransDetails != null)
                    {
                        _Context.RemoveRange(accTransDetails);
                        await _Context.SaveChangesAsync();
                    }
                }
                int newTransNo = 0;
                if (accTransMaster is not null)
                {
                    newTransNo = (int)accTransMaster.TransNo;
                    _Context.Remove(accTransMaster);
                    await _Context.SaveChangesAsync();
                }
                else if (accTransMaster is null)
                {
                    if (deposit.JorKiedNo == null)
                    {
                        List<AccTransMaster> accTransMasters = await _Context.AccTransMasters
                            .Where(obj => obj.CoCode == deposit.BranchId && obj.FYear == deposit.FYear && obj.TransType == userJournalType.JournalCode.ToString()).ToListAsync();
                        if (accTransMasters.Count != 0)
                        {
                            newTransNo = (int)(accTransMasters.Max(obj => obj.TransNo)) + 1;
                        }
                        else { newTransNo = 1; }
                    }
                    else { newDocNo = (int)deposit.SafeDocNo; }
                }
                var yearTransNo = (deposit.FYear + "_" + newTransNo).ToString();

                //inserting///

                AccTransMaster newAccTransMaster = new AccTransMaster();
                newAccTransMaster.CoCode = deposit.BranchId;
                newAccTransMaster.FYear = deposit.FYear;
                newAccTransMaster.TransType = userJournalType.JournalCode.ToString();
                newAccTransMaster.TransNo = newTransNo;
                newAccTransMaster.TransDate = deposit.DpsDate;
                newAccTransMaster.TransDesc = commentt;
                newAccTransMaster.TotalTrans = deposit.DpsVal;
                newAccTransMaster.OkPost = "0";
                newAccTransMaster.CurCode = "1";
                newAccTransMaster.CurRate = 1;
                newAccTransMaster.YearTransNo = yearTransNo;
                DateTime dpsDate = (DateTime)deposit.DpsDate;
                int monthNumber = dpsDate.Month;
                newAccTransMaster.FMonth = monthNumber;
                newAccTransMaster.MasterId = deposit.MasterId;
                newAccTransMaster.MCode = deposit.ModId;
                newAccTransMaster.PostRecipt = deposit.PostRecipt;
                newAccTransMaster.EntryDate = DateTime.Now;
                await _Context.AddAsync(newAccTransMaster);
                //await _Context.SaveChangesAsync();
                int result = await _Context.SaveChangesAsync(); //for return message after enhance method
                if (result== 0)
                {
                    postingAccMasterMINIDTO.Message = "AccTransMaster record added successfully No";
                    postingAccMasterMINIDTO.TransNo = 0;
                    return postingAccMasterMINIDTO;

                }
                else
                {
                    Deposit depositUpdate = await _Context.Deposits.FindAsync(depositId);
                    depositUpdate.JorKiedNo = newTransNo;
                    await _Context.SaveChangesAsync();
                    postingAccMasterMINIDTO.Message = "AccTransMaster record added successfully No";
                    postingAccMasterMINIDTO.TransNo = newTransNo;
                    postingAccMasterMINIDTO.TransId = newAccTransMaster.TransId;
                    return postingAccMasterMINIDTO;
                }

            }


            return postingAccMasterMINIDTO;
        }











        public async Task<string> PostingToAcctransDetail(Deposit deposit, PatAdmission patAdmission, int transNo,int transId)
        {
            SystemTable systemTable = await _Context.SystemTables.FindAsync(35);
            var userCode = deposit.UserCode;
            var depositId = deposit.DpsSer;


            if (systemTable.SysValue == 1)
            {
                UserCashNo userCashNo = await _Context.userCashNos.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
                UserJournalType userJournalType = await _Context.UserJournalTypes.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
                AccSafeCash accSafeCash = await _Context.AccSafeCashes
                 .FirstOrDefaultAsync(obj => obj.BranchId == deposit.BranchId && obj.FYear == deposit.FYear && obj.MasterId == deposit.MasterId && obj.DocType == "SFCIN" && obj.DocNo == deposit.SafeDocNo && obj.Flag == 2 && obj.SafeCode == deposit.CashNo);

                EisPosting eisPosting = await _Context.EisPostings.FirstOrDefaultAsync(obj => obj.PostId == 15 && obj.BranchId == deposit.BranchId);

                var mainCodeEisPosting ="";
                var subCodeEisPosting="";
                string nameOfMainAndSub = "";
                if (eisPosting ==null)
                {
                    return "please enter cash code in accounts then try again";
                }
                else
                {
                     mainCodeEisPosting = eisPosting.MainCode;//الرئيسي
                    subCodeEisPosting = eisPosting.SubCode;//الفرعي
                    if (deposit.DpsType == 1)//نقدا
                    {
                        mainCodeEisPosting = userCashNo.MainCode;//الرئيسي
                        subCodeEisPosting = userCashNo.SubCode;//الفرعي
                    }
                    else if (deposit.DpsType == 2)//شيك
                    {
                        EisPosting eisPostingCheck = await _Context.EisPostings.FirstOrDefaultAsync(obj => obj.PostId == 7 && obj.BranchId == deposit.BranchId);
                        mainCodeEisPosting = eisPostingCheck.MainCode;//الرئيسي
                        if (deposit.BankId == null)
                        {
                            subCodeEisPosting = eisPostingCheck.SubCode;//الفرعي
                        }
                        else
                        {
                            EisPosting eisPostingCheckIFBANK = await _Context.EisPostings.FirstOrDefaultAsync(obj => obj.PostId == 13 && obj.BranchId == deposit.BranchId);
                            mainCodeEisPosting = eisPostingCheckIFBANK.MainCode;//الرئيسي
                            subCodeEisPosting = deposit.BankId;//الفرعي
                        }
                    }
                    else if (deposit.DpsType == 3)//فيزا
                    {
                        EisPosting eisPostingCheck = await _Context.EisPostings.FirstOrDefaultAsync(obj => obj.PostId == 13 && obj.BranchId == deposit.BranchId);
                        mainCodeEisPosting = eisPostingCheck.MainCode;//الرئيسي
                        if (deposit.BankId == null)
                        {
                            subCodeEisPosting = deposit.BankId; ;//الفرعي
                        }
                        else
                        {
                            subCodeEisPosting = deposit.BankId;//الفرعي
                        }
                    }

                  nameOfMainAndSub = await GetAccountName(mainCodeEisPosting, subCodeEisPosting);
                }

                ///////////////////////////////////////////////////

                string commentt;
                if (deposit.DepositDesc != null)
                {
                    commentt = "مدفوعات مقدمة:" + " " + deposit.DepositDesc;
                }
                else { commentt = "مدفوعات مقدمة:"; }

                var patName = (await _Context.Pats.FirstOrDefaultAsync(obj => obj.PatId == deposit.PatId)).PatName;
                var companyName = (await _Context.Companies.FindAsync(patAdmission.CompId)).CompName;
                string description;
                if (patAdmission.MainInvNo == null)
                {
                    description = "المريض:" + " " + patName + " " + "الجهة:" + companyName;
                }
                else { description = "المريض:" + " " + patName + " " + "الجهة: " + companyName + " " + "الرقم: " + patAdmission.MainInvNo; }

                /////////////////////////Accounts Details//////////////////////////////////

                if (transNo != null)
                {
                    List<AccTransDetail> accTransDetails = await _Context.AccTransDetails.Where(obj => obj.TransId == transNo).ToListAsync();
                    if (accTransDetails != null)
                    {
                        _Context.RemoveRange(accTransDetails);
                        await _Context.SaveChangesAsync();
                    }
                }
                var yearTransNo = (deposit.FYear + "_" + transNo).ToString();
                var balanceFlag = (await _Context.AccMainCodes.FirstOrDefaultAsync(obj=>obj.MainCode == mainCodeEisPosting)).BalanceFlag;
                if (balanceFlag == "P") 
                {
                    balanceFlag = "102";
                }

                //inserting///

                AccTransDetail newAccTransDetail = new AccTransDetail();
                newAccTransDetail.CoCode = deposit.BranchId;
                newAccTransDetail.FYear = deposit.FYear;
                newAccTransDetail.TransType = userJournalType.JournalCode.ToString();
                newAccTransDetail.TransNo = transNo;
                newAccTransDetail.TransDate = deposit.DpsDate;
                newAccTransDetail.MainCode = mainCodeEisPosting;
                newAccTransDetail.SubCode = subCodeEisPosting;
                newAccTransDetail.ValDep = deposit.DpsVal;
                newAccTransDetail.ValCredit = 0;
                newAccTransDetail.ValDepCur = deposit.DpsVal;
                newAccTransDetail.ValCreditCur = 0;
                newAccTransDetail.DocNo = transNo.ToString();
                newAccTransDetail.DocStatus = null;
                newAccTransDetail.DocDate = deposit.DpsDate;
                newAccTransDetail.CostCenterCode = balanceFlag;
                newAccTransDetail.AccName = nameOfMainAndSub;
                newAccTransDetail.LineDesc = commentt;
                newAccTransDetail.OkPost = "0";
                newAccTransDetail.CurCode = "1";
                newAccTransDetail.DocCode = null;
                newAccTransDetail.YearTransNo = yearTransNo;
                DateTime dpsDate = (DateTime)deposit.DpsDate;
                int monthNumber = dpsDate.Month;
                newAccTransDetail.FMonth = monthNumber;
                newAccTransDetail.UserCode = deposit.UserCode;
                newAccTransDetail.EntryType = 1;
                newAccTransDetail.TransSerial = 1;
                newAccTransDetail.PostRecipt = deposit.PostRecipt;
                newAccTransDetail.TransId = transId != 0 ? transId : 0;

                await _Context.AddAsync(newAccTransDetail);
                //await _Context.SaveChangesAsync();
                int result = await _Context.SaveChangesAsync(); //for return message after enhance method
                if (result == 0)
                {
                    return "Failed to add the new AccTransDetail record.";
                }
                else
                {
                    await _Context.SaveChangesAsync();
                    return "AccTransDetail record added successfully.";
                }
            }
            return "";
        }
    }
}
