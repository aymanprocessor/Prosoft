using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Migrations;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        //Posting to Account
        public async Task<string> PostingToAcctrans(AccSafeCash accSafeCash)
        {
            var userCode = accSafeCash.UserCode;
            var postingAccMasterMINIDTO = new PostingAccMasterMINIDTO();
            if (accSafeCash.Flag ==1 && accSafeCash.AprovedFlag =="APR")
            {
                UserJournalType userJournalType = await _Context.UserJournalTypes.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
                UserCashNo userCashNo = await _Context.userCashNos.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
                if (userJournalType is not null && userCashNo is not null)
                {
                    accSafeCash.AccTransType = userJournalType.JournalCode; //هحط اليومية في ال column acctranstype
                    var lsCommentt = accSafeCash.Commentt + "ايصال توريد نقدي رقم :" + accSafeCash.DocNo;
                    var lsMainSubSafe = userCashNo.MainCode +"*"+userCashNo.SubCode;
                    var lsAccNameDept = await GetAccountName(userCashNo.MainCode, userCashNo.SubCode);
                    var lsAccNameCredit = await GetAccountName(accSafeCash.MainCode, accSafeCash.SubCode);
                    var moduleId = 4;//code الخزينة والبنوك
                    var ldValueEgy = (accSafeCash.ValuePay * accSafeCash.Rate1);
                    var lsYearTransNo = accSafeCash.FYear + "_" + accSafeCash.AccTransNo;
                    var lsCostCenterCode = "";
                    if ((userCashNo.MainCode).Substring(0,1) == "1" )
                    {
                        lsCostCenterCode = null;
                    }
                    else
                    {
                        lsCostCenterCode = accSafeCash.CostCenterCode.ToString();
                    }
                    AccTransMaster accTransMaster = await _Context.AccTransMasters.FirstOrDefaultAsync(obj => obj.CoCode == accSafeCash.BranchId && obj.FYear == accSafeCash.FYear && obj.TransType == userJournalType.JournalCode.ToString() && obj.TransNo == accSafeCash.AccTransNo);
                    if (accTransMaster is not null)
                    {
                        if (accTransMaster.OkPost =="1")
                        {
                            return "لم يتم الترحيل للحسابات بسبب اقفال الحسابات";
                         // return postingAccMasterMINIDTO;
                        }
                        //delete master And Detail
                        _Context.Remove(accTransMaster); 
                        List<AccTransDetail> accTransDetails = await _Context.AccTransDetails.Where(obj => obj.TransId == accTransMaster.TransId).ToListAsync();
                        if (accTransDetails != null)
                        {
                            _Context.RemoveRange(accTransDetails);
                        }
                        await _Context.SaveChangesAsync();

                        //inserting///
                        AccTransMaster newAccTransMaster = new AccTransMaster();
                        newAccTransMaster.CoCode = accSafeCash.BranchId;
                        newAccTransMaster.FYear = accSafeCash.FYear;
                        newAccTransMaster.TransType = userJournalType.JournalCode.ToString();
                        newAccTransMaster.TransNo = (int)accSafeCash.AccTransNo;
                        newAccTransMaster.TransDate =accSafeCash.DocDate;
                        newAccTransMaster.TransDesc = lsCommentt;
                        newAccTransMaster.TotalTrans = ldValueEgy;
                        newAccTransMaster.OkPost = "0";
                        newAccTransMaster.CurCode = accSafeCash.CurCode.ToString();
                        newAccTransMaster.CurRate = accSafeCash.Rate1;
                        newAccTransMaster.YearTransNo = lsYearTransNo;
                        DateTime dpsDate = (DateTime)accSafeCash.DocDate;
                        int monthNumber = dpsDate.Month;
                        newAccTransMaster.FMonth = monthNumber;
                        newAccTransMaster.MCode = moduleId;
                        newAccTransMaster.MCodeDtl = accSafeCash.MCodeDtl;
                        newAccTransMaster.DocNo = accSafeCash.DocNo.ToString();
                        newAccTransMaster.EntryDate = DateTime.Now;
                        await _Context.AddAsync(newAccTransMaster);

                        ///////////////Inserting to Detail

                        AccTransDetail newAccTransDetail = new AccTransDetail();
                        newAccTransDetail.CoCode = accSafeCash.BranchId;
                        newAccTransDetail.FYear = accSafeCash.FYear;
                        newAccTransDetail.TransType = userJournalType.JournalCode.ToString();
                        newAccTransDetail.TransNo = (int)accSafeCash.AccTransNo;
                        newAccTransDetail.TransDate = accSafeCash.DocDate;
                        newAccTransDetail.TransSerial = 1;
                        newAccTransDetail.MainCode = accSafeCash.MainCode;
                        newAccTransDetail.SubCode = accSafeCash.SubCode;
                        newAccTransDetail.ValDep = ldValueEgy;
                        newAccTransDetail.ValCredit = 0;
                        newAccTransDetail.ValDepCur = accSafeCash.ValuePay;
                        newAccTransDetail.ValCreditCur = 0;
                        newAccTransDetail.DocNo = accSafeCash.DocNo.ToString();
                        newAccTransDetail.DocStatus = null;
                        newAccTransDetail.DocDate = accSafeCash.DocDate;
                        newAccTransDetail.CostCenterCode = lsCostCenterCode;
                        newAccTransDetail.AccName = lsAccNameDept;
                        newAccTransDetail.LineDesc = lsCommentt;
                        newAccTransDetail.OkPost = "0";
                        newAccTransDetail.CurCode = accSafeCash.CurCode.ToString();
                        newAccTransDetail.DocCode = null;
                        newAccTransDetail.YearTransNo = lsYearTransNo;
                        DateTime dpsDate2 = (DateTime)accSafeCash.DocDate;
                        int monthNumber2 = dpsDate.Month;
                        newAccTransDetail.FMonth = monthNumber2;
                        newAccTransDetail.UserCode = accSafeCash.UserCode;
                        newAccTransDetail.EntryType = accSafeCash.EntryType;
                        newAccTransDetail.EntryDate = DateTime.Now;
                        newAccTransDetail.MCodeDtl = accSafeCash.MCodeDtl; ;
                        newAccTransDetail.TransId = newAccTransMaster.TransId != 0 ? newAccTransMaster.TransId : 0;

                        await _Context.AddAsync(newAccTransDetail);
                        int result = await _Context.SaveChangesAsync(); //for return message after enhance method
                        if (result == 0)
                        {
                            return "Failed to post in Account.";
                        }
                        else
                        {
                            return "Posting to Account Done."; 

                            ///اللي عليه الدور ترحيل الخصومات وهسال هل لو الخصومات 
                        }     
                    }
                }
            }
            return "Failed to post in Account.";
        }
    }
}
