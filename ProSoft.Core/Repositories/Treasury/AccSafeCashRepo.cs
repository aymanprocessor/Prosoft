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
                        AccTransNo =obj.AccTransNo
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
                        AccTransNo = obj.AccTransNo
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
            List<SafeName> safeNames2 = await _Context.SafeNames.ToListAsync();//for transfer


            accSafeCashDTO.journalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            accSafeCashDTO.gTablels = _mapper.Map<List<GTablelDTO>>(gTables);
            accSafeCashDTO.costCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            accSafeCashDTO.treasuryNames = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames);
            accSafeCashDTO.treasuryNames2 = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames2);//for transfer
            accSafeCashDTO.accGlobalDefs = _mapper.Map<List<AccGlobalDefDTO>>(accGlobalDefs);
            accSafeCashDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);
            accSafeCashDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);

            
            return accSafeCashDTO;
        }

        public async Task<string> AddAccSafeCashAsync(AccSafeCashEditAddDTO accSafeCashDTO)
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
            if (accSafeCash.Rate1 == null)
            {
                accSafeCash.Rate1 = 1;
            }
            await _Context.AddAsync(accSafeCash);
            await _Context.SaveChangesAsync();

            if (accSafeCash.AprovedFlag == "APR" && accSafeCash.DocType == "SFCIN")
            {
                string message = await PostingToAcctrans_PaymentReceipt(accSafeCash);
                return message;    
            }
            else if (accSafeCash.AprovedFlag == "APP" && accSafeCash.DocType == "SFCOT")
            {
                string message = await PostingToAcctrans_PaymentReceipt(accSafeCash);
                return message;
            }
            else if (accSafeCash.AprovedFlag == "APR" && accSafeCash.DocType == "SFTIN")
            {
                string message = await PostingToAcctrans_PaymentReceipt(accSafeCash);
                return message;
            }
            else if (accSafeCash.AprovedFlag == "APP" && accSafeCash.DocType == "SFTOT")
            {
                string message = await PostingToAcctrans_PaymentReceipt(accSafeCash);
                return message;
            }

            return "";
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
            List<SafeName> safeNames2 = await _Context.SafeNames.ToListAsync();//for transfer

            accSafeCashDTO.journalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            accSafeCashDTO.gTablels = _mapper.Map<List<GTablelDTO>>(gTables);
            accSafeCashDTO.costCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            accSafeCashDTO.treasuryNames = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames);
            accSafeCashDTO.treasuryNames2 = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames2);//for transfer
            accSafeCashDTO.accGlobalDefs = _mapper.Map<List<AccGlobalDefDTO>>(accGlobalDefs);
            accSafeCashDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);
            accSafeCashDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);

            return accSafeCashDTO;
        }

        public async Task<string> EditAccSafeCashAsync(int id, AccSafeCashEditAddDTO accSafeCashDTO)
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
            if (accSafeCash.Rate1 ==null)
            {
                accSafeCash.Rate1 = 1;
            }
            accSafeCash.EntryDate = DateTime.Now;
            _Context.Update(accSafeCash);
            await _Context.SaveChangesAsync();

            if (accSafeCash.AprovedFlag == "APR" && accSafeCash.DocType == "SFCIN")
            {
                string message = await PostingToAcctrans_PaymentReceipt(accSafeCash);
                return message;
            }
            else if (accSafeCash.AprovedFlag == "APP" && accSafeCash.DocType == "SFCOT")
            {
                string message = await PostingToAcctrans_DisbursementPermission(accSafeCash);
                return message;
            }
            else if (accSafeCash.AprovedFlag == "APR" && accSafeCash.DocType == "SFTIN")
            {
                string message = await PostingToAcctrans_PaymentReceipt(accSafeCash);
                return message;
            }
            else if (accSafeCash.AprovedFlag == "APP" && accSafeCash.DocType == "SFTOT")
            {
                string message = await PostingToAcctrans_DisbursementPermission(accSafeCash);
                return message;
            }
            return "لم يتم الترحيل للحسابات";
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

        //////////////////////////////////////Posting////////////////////////////////////////////////////

        //Posting PaymentReceipt & ReciveReceipt to Account SFCIN & SFTIN
        public async Task<string> PostingToAcctrans_PaymentReceipt(AccSafeCash accSafeCash)
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
                    var lsCommentt = accSafeCash.Commentt +":"+ "ايصال توريد نقدي رقم :" + accSafeCash.DocNo;
                    var lsMainSubSafe = userCashNo.MainCode +"*"+userCashNo.SubCode;
                    var lsAccNameDept = await GetAccountName(userCashNo.MainCode, userCashNo.SubCode);
                    var lsAccNameCredit = await GetAccountName(accSafeCash.MainCode, accSafeCash.SubCode);
                    var moduleId = 4;//code الخزينة والبنوك
                    var ldValueEgy = (accSafeCash.ValuePay * accSafeCash.Rate1);
                    var lsYearTransNo = accSafeCash.FYear + "_" + accSafeCash.AccTransNo;
                    var lsCostCenterCode = "";
                    if ((userCashNo.MainCode).Substring(0,1) == "1" || (accSafeCash.MainCode).Substring(0, 1) == "2")
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
                        await _Context.SaveChangesAsync();

                        ///////////////Inserting to Detail

                        AccTransDetail newAccTransDetail = new AccTransDetail();
                        newAccTransDetail.CoCode = accSafeCash.BranchId;
                        newAccTransDetail.FYear = accSafeCash.FYear;
                        newAccTransDetail.TransType = userJournalType.JournalCode.ToString();
                        newAccTransDetail.TransNo = (int)accSafeCash.AccTransNo;
                        newAccTransDetail.TransDate = accSafeCash.DocDate;
                        newAccTransDetail.TransSerial = 1;
                        newAccTransDetail.MainCode = userCashNo.MainCode;
                        newAccTransDetail.SubCode = userCashNo.SubCode;
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
                        await _Context.SaveChangesAsync();

                        ///////////////Inserting Discount in accTransdetails
                        List<CustCollectionsDiscount> custCollectionsDiscounts =await _Context.custCollectionsDiscounts.Where(obj=>obj.SafeCashId == accSafeCash.SafeCashId).ToListAsync();
                            var totalDiscValue=0.0;
                            var llSerial = 1;
                            string lsCostCenterCodeC;
                            var ldDiscValueAll= 0.0;

                        if (custCollectionsDiscounts.Count !=0)
                        {
                            foreach (var item in custCollectionsDiscounts)
                            {
                                var lsMainCode = item.MainCode;
                                var lsSubCode = item.SubCode;
                                var ldDiscValue = item.DiscValue;
                                totalDiscValue += (double)ldDiscValue;
                                ldDiscValueAll =  totalDiscValue;
                                var lsAccNameDepitC = await GetAccountName(item.MainCode, item.SubCode);
                                llSerial ++;
                                if ((item.MainCode).Substring(0, 1) == "1" || (item.MainCode).Substring(0, 1) == "2")
                                {
                                    lsCostCenterCodeC = null;
                                }
                                else
                                {
                                    lsCostCenterCodeC = accSafeCash.CostCenterCode.ToString();
                                }

                                //Inserting
                                AccTransDetail newAccTransDetailC = new AccTransDetail();

                                newAccTransDetailC.CoCode = accSafeCash.BranchId;
                                newAccTransDetailC.FYear = accSafeCash.FYear;
                                newAccTransDetailC.TransType = userJournalType.JournalCode.ToString();
                                newAccTransDetailC.TransNo = (int)accSafeCash.AccTransNo;
                                newAccTransDetailC.TransDate = accSafeCash.DocDate;
                                newAccTransDetailC.TransSerial = llSerial;
                                newAccTransDetailC.MainCode = item.MainCode;
                                newAccTransDetailC.SubCode = item.SubCode;
                                newAccTransDetailC.ValDep = ldDiscValue;
                                newAccTransDetailC.ValCredit = 0;
                                newAccTransDetailC.ValDepCur = ldDiscValue;
                                newAccTransDetailC.ValCreditCur = 0;
                                newAccTransDetailC.DocNo = accSafeCash.DocNo.ToString();
                                newAccTransDetailC.DocStatus = null;
                                newAccTransDetailC.DocDate = accSafeCash.DocDate;
                                newAccTransDetailC.CostCenterCode = lsCostCenterCodeC;
                                newAccTransDetailC.AccName = lsAccNameDepitC;
                                newAccTransDetailC.LineDesc = lsCommentt;
                                newAccTransDetailC.OkPost = "0";
                                newAccTransDetailC.CurCode = accSafeCash.CurCode.ToString();
                                newAccTransDetailC.DocCode = null;
                                newAccTransDetailC.YearTransNo = lsYearTransNo;
                                DateTime dpsDate3 = (DateTime)accSafeCash.DocDate;
                                int monthNumber3 = dpsDate.Month;
                                newAccTransDetailC.FMonth = monthNumber3;
                                newAccTransDetailC.UserCode = accSafeCash.UserCode;
                                newAccTransDetailC.EntryType = accSafeCash.EntryType;
                                newAccTransDetailC.EntryDate = DateTime.Now;
                                newAccTransDetailC.MCodeDtl = accSafeCash.MCodeDtl; ;
                                newAccTransDetailC.TransId = newAccTransMaster.TransId != 0 ? newAccTransMaster.TransId : 0;
                                await _Context.AddAsync(newAccTransDetailC);
                                await _Context.SaveChangesAsync();
                            }
                        }

                        //Inserting
                        string lsCostCenterCodeC_d;
                        if ((accSafeCash.MainCode).Substring(0, 1) == "1" || (accSafeCash.MainCode).Substring(0, 1) == "2")
                        {
                            lsCostCenterCodeC_d = null;
                        }
                        else
                        {
                            lsCostCenterCodeC_d = accSafeCash.CostCenterCode.ToString();
                        }
                        AccTransDetail newAccTransDetailC_d = new AccTransDetail();

                        newAccTransDetailC_d.CoCode = accSafeCash.BranchId;
                        newAccTransDetailC_d.FYear = accSafeCash.FYear;
                        newAccTransDetailC_d.TransType = userJournalType.JournalCode.ToString();
                        newAccTransDetailC_d.TransNo = (int)accSafeCash.AccTransNo;
                        newAccTransDetailC_d.TransDate = accSafeCash.DocDate;
                        newAccTransDetailC_d.TransSerial = llSerial + 1;
                        newAccTransDetailC_d.MainCode = accSafeCash.MainCode;
                        newAccTransDetailC_d.SubCode = accSafeCash.SubCode;
                        newAccTransDetailC_d.ValDep = 0;
                        newAccTransDetailC_d.ValCredit = ldValueEgy + (decimal)ldDiscValueAll;
                        newAccTransDetailC_d.ValDepCur = 0;
                        newAccTransDetailC_d.ValCreditCur = accSafeCash.ValuePay;
                        newAccTransDetailC_d.DocNo = accSafeCash.DocNo.ToString();
                        newAccTransDetailC_d.DocStatus = null;
                        newAccTransDetailC_d.DocDate = accSafeCash.DocDate;
                        newAccTransDetailC_d.CostCenterCode = lsCostCenterCodeC_d;
                        newAccTransDetailC_d.AccName = lsAccNameCredit;
                        newAccTransDetailC_d.LineDesc = lsCommentt;
                        newAccTransDetailC_d.OkPost = "0";
                        newAccTransDetailC_d.CurCode = accSafeCash.CurCode.ToString();
                        newAccTransDetailC_d.DocCode = null;
                        newAccTransDetailC_d.YearTransNo = lsYearTransNo;
                        DateTime dpsDate4 = (DateTime)accSafeCash.DocDate;
                        int monthNumber4 = dpsDate.Month;
                        newAccTransDetailC_d.FMonth = monthNumber4;
                        newAccTransDetailC_d.UserCode = accSafeCash.UserCode;
                        newAccTransDetailC_d.EntryType = accSafeCash.EntryType;
                        newAccTransDetailC_d.EntryDate = DateTime.Now;
                        newAccTransDetailC_d.MCodeDtl = accSafeCash.MCodeDtl; ;
                        newAccTransDetailC_d.TransId = newAccTransMaster.TransId != 0 ? newAccTransMaster.TransId : 0;
                        await _Context.AddAsync(newAccTransDetailC_d);

                        int result = await _Context.SaveChangesAsync(); //for return message after enhance method
                        if (result == 0)
                        {
                            return "Failed to post in Account.";
                        }
                        else
                        {
                            return "Posting to Account Done."; 

                        }     
                    }
                   
                    //قيد اول مرة
                    else
                    {
                        EF.Models.Shared.SystemTable systemTable = await _Context.SystemTables.FindAsync(2);

                        int llDoc ;
                        if (accSafeCash.AccTransNo == null || accSafeCash.AccTransNo == 0)
                        {
                          if (systemTable.SysValue ==1)
                            {
                                 llDoc = (int)(await _Context.AccTransMasters.Where(obj => obj.CoCode == accSafeCash.BranchId && obj.FYear == accSafeCash.FYear && obj.TransType == userJournalType.JournalCode.ToString() && obj.FMonth == accSafeCash.FMonth).MaxAsync(obj=>obj.TransNo)) + 1;
                            }
                            else if (systemTable.SysValue == 2 || systemTable.SysValue == 3)
                            {
                                 llDoc = (int)(await _Context.AccTransMasters.Where(obj => obj.CoCode == accSafeCash.BranchId && obj.FYear == accSafeCash.FYear && obj.TransType == userJournalType.JournalCode.ToString()).MaxAsync(obj => obj.TransNo)) + 1;
                            }
                            else
                            {
                                return "برجاء تحديد مسلسل الحركة شهري ام ثانوي اولا ثم اعد المحاولة";
                            }
                            lsYearTransNo = accSafeCash.FYear + "_" + llDoc;
                        }
                        else
                        {
                            llDoc = (int)accSafeCash.AccTransNo;
                        }
                        //Istert AccTransMaster

                        AccTransMaster newAccTransMaster = new AccTransMaster();
                        newAccTransMaster.CoCode = accSafeCash.BranchId;
                        newAccTransMaster.FYear = accSafeCash.FYear;
                        newAccTransMaster.TransType = userJournalType.JournalCode.ToString();
                        newAccTransMaster.TransNo = llDoc;
                        newAccTransMaster.TransDate = accSafeCash.DocDate;
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
                        await _Context.SaveChangesAsync();
                        /////////////////////////////////////////////////////
                        if ((userCashNo.MainCode).Substring(0, 1) == "1" || (userCashNo.MainCode).Substring(0, 1) == "2")
                        {
                            lsCostCenterCode = null;
                        }
                        else
                        {
                            lsCostCenterCode = accSafeCash.CostCenterCode.ToString();
                        }
                        var llSerial = 1;

                        //Istert AccTransDetails

                        AccTransDetail newAccTransDetail = new AccTransDetail();
                        newAccTransDetail.CoCode = accSafeCash.BranchId;
                        newAccTransDetail.FYear = accSafeCash.FYear;
                        newAccTransDetail.TransType = userJournalType.JournalCode.ToString();
                        newAccTransDetail.TransNo = llDoc;
                        newAccTransDetail.TransDate = accSafeCash.DocDate;
                        newAccTransDetail.TransSerial = llSerial;
                        newAccTransDetail.MainCode = userCashNo.MainCode;
                        newAccTransDetail.SubCode = userCashNo.SubCode;
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

                        //Istert AccTransDetails Discount
                        List<CustCollectionsDiscount> custCollectionsDiscounts = await _Context.custCollectionsDiscounts.Where(obj => obj.SafeCashId == accSafeCash.SafeCashId).ToListAsync();
                        var totalDiscValue = 0.0;
                        var llSeriall = 1;
                        string lsCostCenterCodeC;
                        var ldDiscValueAll = 0.0;
                        if (custCollectionsDiscounts.Count != 0)
                        {
                            foreach (var item in custCollectionsDiscounts)
                            {
                                var lsMainCode = item.MainCode;
                                var lsSubCode = item.SubCode;
                                var ldDiscValue = item.DiscValue;
                                totalDiscValue += (double)ldDiscValue;
                                ldDiscValueAll = totalDiscValue;
                                var lsAccNameDepitC = await GetAccountName(item.MainCode, item.SubCode);
                                llSeriall++;
                                if ((item.MainCode).Substring(0, 1) == "1" || (item.MainCode).Substring(0, 1) == "2")
                                {
                                    lsCostCenterCodeC = null;
                                }
                                else
                                {
                                    lsCostCenterCodeC = accSafeCash.CostCenterCode.ToString();
                                }

                                //Inserting
                                AccTransDetail newAccTransDetailC = new AccTransDetail();

                                newAccTransDetailC.CoCode = accSafeCash.BranchId;
                                newAccTransDetailC.FYear = accSafeCash.FYear;
                                newAccTransDetailC.TransType = userJournalType.JournalCode.ToString();
                                newAccTransDetailC.TransNo = llDoc;
                                newAccTransDetailC.TransDate = accSafeCash.DocDate;
                                newAccTransDetailC.TransSerial = llSeriall;
                                newAccTransDetailC.MainCode = item.MainCode;
                                newAccTransDetailC.SubCode = item.SubCode;
                                newAccTransDetailC.ValDep = ldDiscValue;
                                newAccTransDetailC.ValCredit = 0;
                                newAccTransDetailC.ValDepCur = ldDiscValue;
                                newAccTransDetailC.ValCreditCur = 0;
                                newAccTransDetailC.DocNo = accSafeCash.DocNo.ToString();
                                newAccTransDetailC.DocStatus = null;
                                newAccTransDetailC.DocDate = accSafeCash.DocDate;
                                newAccTransDetailC.CostCenterCode = lsCostCenterCodeC;
                                newAccTransDetailC.AccName = lsAccNameDepitC;
                                newAccTransDetailC.LineDesc = lsCommentt;
                                newAccTransDetailC.OkPost = "0";
                                newAccTransDetailC.CurCode = accSafeCash.CurCode.ToString();
                                newAccTransDetailC.DocCode = null;
                                newAccTransDetailC.YearTransNo = lsYearTransNo;
                                DateTime dpsDate3 = (DateTime)accSafeCash.DocDate;
                                int monthNumber3 = dpsDate.Month;
                                newAccTransDetailC.FMonth = monthNumber3;
                                newAccTransDetailC.UserCode = accSafeCash.UserCode;
                                newAccTransDetailC.EntryType = accSafeCash.EntryType;
                                newAccTransDetailC.EntryDate = DateTime.Now;
                                newAccTransDetailC.MCodeDtl = accSafeCash.MCodeDtl; ;
                                newAccTransDetailC.TransId = newAccTransMaster.TransId != 0 ? newAccTransMaster.TransId : 0;
                                await _Context.AddAsync(newAccTransDetailC);
                                await _Context.SaveChangesAsync();

                            }
                        }
                        //Inserting
                        string lsCostCenterCodeC_d;
                        if ((accSafeCash.MainCode).Substring(0, 1) == "1" || (accSafeCash.MainCode).Substring(0, 1) == "2")
                        {
                            lsCostCenterCodeC_d = null;
                        }
                        else
                        {
                            lsCostCenterCodeC_d = accSafeCash.CostCenterCode.ToString();
                        }
                        AccTransDetail newAccTransDetailC_d = new AccTransDetail();

                        newAccTransDetailC_d.CoCode = accSafeCash.BranchId;
                        newAccTransDetailC_d.FYear = accSafeCash.FYear;
                        newAccTransDetailC_d.TransType = userJournalType.JournalCode.ToString();
                        newAccTransDetailC_d.TransNo = llDoc;
                        newAccTransDetailC_d.TransDate = accSafeCash.DocDate;
                        newAccTransDetailC_d.TransSerial = llSeriall + 1;
                        newAccTransDetailC_d.MainCode = accSafeCash.MainCode;
                        newAccTransDetailC_d.SubCode = accSafeCash.SubCode;
                        newAccTransDetailC_d.ValDep = 0;
                        newAccTransDetailC_d.ValCredit = ldValueEgy + (decimal)ldDiscValueAll;
                        newAccTransDetailC_d.ValDepCur = 0;
                        newAccTransDetailC_d.ValCreditCur = accSafeCash.ValuePay;
                        newAccTransDetailC_d.DocNo = accSafeCash.DocNo.ToString();
                        newAccTransDetailC_d.DocStatus = null;
                        newAccTransDetailC_d.DocDate = accSafeCash.DocDate;
                        newAccTransDetailC_d.CostCenterCode = lsCostCenterCodeC_d;
                        newAccTransDetailC_d.AccName = lsAccNameCredit;
                        newAccTransDetailC_d.LineDesc = lsCommentt;
                        newAccTransDetailC_d.OkPost = "0";
                        newAccTransDetailC_d.CurCode = accSafeCash.CurCode.ToString();
                        newAccTransDetailC_d.DocCode = null;
                        newAccTransDetailC_d.YearTransNo = lsYearTransNo;
                        DateTime dpsDate4 = (DateTime)accSafeCash.DocDate;
                        int monthNumber4 = dpsDate.Month;
                        newAccTransDetailC_d.FMonth = monthNumber4;
                        newAccTransDetailC_d.UserCode = accSafeCash.UserCode;
                        newAccTransDetailC_d.EntryType = accSafeCash.EntryType;
                        newAccTransDetailC_d.EntryDate = DateTime.Now;
                        newAccTransDetailC_d.MCodeDtl = accSafeCash.MCodeDtl; ;
                        newAccTransDetailC_d.TransId = newAccTransMaster.TransId != 0 ? newAccTransMaster.TransId : 0;
                        await _Context.AddAsync(newAccTransDetailC_d);

                        accSafeCash.AccTransNo = llDoc;
                         _Context.Update(accSafeCash);
                        int result = await _Context.SaveChangesAsync(); //for return message after enhance method
                        if (result == 0)
                        {
                            return "Failed to post in Account.";
                        }
                        else
                        {
                            return "Posting to Account Done.";

                        }
                    }
                }
            }
            return "Failed to post in Account.";
        }

                //////////////////////////////////////////////////////////////

        //Posting DisbursementPermission & transfer to Account SFCOT & SFTOT
        public async Task<string> PostingToAcctrans_DisbursementPermission(AccSafeCash accSafeCash)
        {
            var userCode = accSafeCash.UserCode;
            var postingAccMasterMINIDTO = new PostingAccMasterMINIDTO();
            if (accSafeCash.Flag == 1 && accSafeCash.AprovedFlag == "APP")
            {
                UserJournalType userJournalType = await _Context.UserJournalTypes.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
                UserCashNo userCashNo = await _Context.userCashNos.FirstOrDefaultAsync(obj => obj.UserCode == userCode);
                if (userJournalType is not null && userCashNo is not null)
                {
                    accSafeCash.AccTransType = userJournalType.JournalCode; //هحط اليومية في ال column acctranstype
                    var lsCommentt = accSafeCash.Commentt + ":" + "ايصال صرف نقدي رقم :" + accSafeCash.DocNo;
                    var lsMainSubSafe = userCashNo.MainCode + "*" + userCashNo.SubCode;
                    var lsAccNameDept = await GetAccountName(accSafeCash.MainCode, accSafeCash.SubCode);
                    var lsAccNameCredit = await GetAccountName(userCashNo.MainCode, userCashNo.SubCode);
                    var moduleId = 4;//code الخزينة والبنوك
                    var ldValueEgy = (accSafeCash.ValuePay * accSafeCash.Rate1);
                    var lsYearTransNo = accSafeCash.FYear + "_" + accSafeCash.AccTransNo;
                    var lsCostCenterCode = "";
                    if ((userCashNo.MainCode).Substring(0, 1) == "1" || (accSafeCash.MainCode).Substring(0, 1) == "2")
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
                        if (accTransMaster.OkPost == "1")
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
                        newAccTransMaster.TransDate = accSafeCash.DocDate;
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
                        await _Context.SaveChangesAsync();

                        ///////////////Inserting to Detail

                        AccTransDetail newAccTransDetail = new AccTransDetail();
                        newAccTransDetail.CoCode = accSafeCash.BranchId;
                        newAccTransDetail.FYear = accSafeCash.FYear;
                        newAccTransDetail.TransType = userJournalType.JournalCode.ToString();
                        newAccTransDetail.TransNo = (int)accSafeCash.AccTransNo;
                        newAccTransDetail.TransDate = accSafeCash.DocDate;
                        newAccTransDetail.TransSerial = 2;
                        newAccTransDetail.MainCode = userCashNo.MainCode;
                        newAccTransDetail.SubCode = userCashNo.SubCode;
                        newAccTransDetail.ValDep = 0;
                        newAccTransDetail.ValCredit = ldValueEgy;
                        newAccTransDetail.ValDepCur = 0;
                        newAccTransDetail.ValCreditCur = accSafeCash.ValuePay;
                        newAccTransDetail.DocNo = accSafeCash.DocNo.ToString();
                        newAccTransDetail.DocStatus = null;
                        newAccTransDetail.DocDate = accSafeCash.DocDate;
                        newAccTransDetail.CostCenterCode = lsCostCenterCode;
                        newAccTransDetail.AccName = lsAccNameCredit;
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
                        await _Context.SaveChangesAsync();

                        ///////////////Inserting Discount in accTransdetails
                        List<CustCollectionsDiscount> custCollectionsDiscounts = await _Context.custCollectionsDiscounts.Where(obj => obj.SafeCashId == accSafeCash.SafeCashId).ToListAsync();
                        var totalDiscValue = 0.0;
                        var llSerial = 2;
                        string lsCostCenterCodeC;
                        var ldDiscValueAll = 0.0;

                        if (custCollectionsDiscounts.Count != 0)
                        {
                            foreach (var item in custCollectionsDiscounts)
                            {
                                var lsMainCode = item.MainCode;
                                var lsSubCode = item.SubCode;
                                var ldDiscValue = item.DiscValue;
                                totalDiscValue += (double)ldDiscValue;
                                ldDiscValueAll = totalDiscValue;
                                var lsAccNameCreditC = await GetAccountName(item.MainCode, item.SubCode);
                                llSerial++;
                                if ((item.MainCode).Substring(0, 1) == "1" || (item.MainCode).Substring(0, 1) == "2")
                                {
                                    lsCostCenterCodeC = null;
                                }
                                else
                                {
                                    lsCostCenterCodeC = accSafeCash.CostCenterCode.ToString();
                                }

                                //Inserting
                                AccTransDetail newAccTransDetailC = new AccTransDetail();

                                newAccTransDetailC.CoCode = accSafeCash.BranchId;
                                newAccTransDetailC.FYear = accSafeCash.FYear;
                                newAccTransDetailC.TransType = userJournalType.JournalCode.ToString();
                                newAccTransDetailC.TransNo = (int)accSafeCash.AccTransNo;
                                newAccTransDetailC.TransDate = accSafeCash.DocDate;
                                newAccTransDetailC.TransSerial = llSerial;
                                newAccTransDetailC.MainCode = item.MainCode;
                                newAccTransDetailC.SubCode = item.SubCode;
                                newAccTransDetailC.ValDep = 0;
                                newAccTransDetailC.ValCredit = ldDiscValue;
                                newAccTransDetailC.ValDepCur = 0;
                                newAccTransDetailC.ValCreditCur = ldDiscValue;
                                newAccTransDetailC.DocNo = accSafeCash.DocNo.ToString();
                                newAccTransDetailC.DocStatus = null;
                                newAccTransDetailC.DocDate = accSafeCash.DocDate;
                                newAccTransDetailC.CostCenterCode = lsCostCenterCodeC;
                                newAccTransDetailC.AccName = lsAccNameCreditC;
                                newAccTransDetailC.LineDesc = lsCommentt;
                                newAccTransDetailC.OkPost = "0";
                                newAccTransDetailC.CurCode = accSafeCash.CurCode.ToString();
                                newAccTransDetailC.DocCode = null;
                                newAccTransDetailC.YearTransNo = lsYearTransNo;
                                DateTime dpsDate3 = (DateTime)accSafeCash.DocDate;
                                int monthNumber3 = dpsDate.Month;
                                newAccTransDetailC.FMonth = monthNumber3;
                                newAccTransDetailC.UserCode = accSafeCash.UserCode;
                                newAccTransDetailC.EntryType = accSafeCash.EntryType;
                                newAccTransDetailC.EntryDate = DateTime.Now;
                                newAccTransDetailC.MCodeDtl = accSafeCash.MCodeDtl; ;
                                newAccTransDetailC.TransId = newAccTransMaster.TransId != 0 ? newAccTransMaster.TransId : 0;
                                await _Context.AddAsync(newAccTransDetailC);
                                await _Context.SaveChangesAsync();
                            }
                        }

                        //Inserting
                        string lsCostCenterCodeC_d;
                        if ((accSafeCash.MainCode).Substring(0, 1) == "1" || (accSafeCash.MainCode).Substring(0, 1) == "2")
                        {
                            lsCostCenterCodeC_d = null;
                        }
                        else
                        {
                            lsCostCenterCodeC_d = accSafeCash.CostCenterCode.ToString();
                        }
                        AccTransDetail newAccTransDetailC_d = new AccTransDetail();

                        newAccTransDetailC_d.CoCode = accSafeCash.BranchId;
                        newAccTransDetailC_d.FYear = accSafeCash.FYear;
                        newAccTransDetailC_d.TransType = userJournalType.JournalCode.ToString();
                        newAccTransDetailC_d.TransNo = (int)accSafeCash.AccTransNo;
                        newAccTransDetailC_d.TransDate = accSafeCash.DocDate;
                        newAccTransDetailC_d.TransSerial = 1;
                        newAccTransDetailC_d.MainCode = accSafeCash.MainCode;
                        newAccTransDetailC_d.SubCode = accSafeCash.SubCode;
                        newAccTransDetailC_d.ValDep = ldValueEgy + (decimal)ldDiscValueAll;
                        newAccTransDetailC_d.ValCredit = 0;
                        newAccTransDetailC_d.ValDepCur = accSafeCash.ValuePay;
                        newAccTransDetailC_d.ValCreditCur = 0;
                        newAccTransDetailC_d.DocNo = accSafeCash.DocNo.ToString();
                        newAccTransDetailC_d.DocStatus = null;
                        newAccTransDetailC_d.DocDate = accSafeCash.DocDate;
                        newAccTransDetailC_d.CostCenterCode = lsCostCenterCodeC_d;
                        newAccTransDetailC_d.AccName = lsAccNameDept;
                        newAccTransDetailC_d.LineDesc = lsCommentt;
                        newAccTransDetailC_d.OkPost = "0";
                        newAccTransDetailC_d.CurCode = accSafeCash.CurCode.ToString();
                        newAccTransDetailC_d.DocCode = null;
                        newAccTransDetailC_d.YearTransNo = lsYearTransNo;
                        DateTime dpsDate4 = (DateTime)accSafeCash.DocDate;
                        int monthNumber4 = dpsDate.Month;
                        newAccTransDetailC_d.FMonth = monthNumber4;
                        newAccTransDetailC_d.UserCode = accSafeCash.UserCode;
                        newAccTransDetailC_d.EntryType = accSafeCash.EntryType;
                        newAccTransDetailC_d.EntryDate = DateTime.Now;
                        newAccTransDetailC_d.MCodeDtl = accSafeCash.MCodeDtl; ;
                        newAccTransDetailC_d.TransId = newAccTransMaster.TransId != 0 ? newAccTransMaster.TransId : 0;
                        await _Context.AddAsync(newAccTransDetailC_d);

                        int result = await _Context.SaveChangesAsync(); //for return message after enhance method
                        if (result == 0)
                        {
                            return "Failed to post in Account.";
                        }
                        else
                        {
                            return "Posting to Account Done.";

                        }
                    }

                    //قيد اول مرة
                    else
                    {
                        EF.Models.Shared.SystemTable systemTable = await _Context.SystemTables.FindAsync(2);

                        int llDoc;
                        if(accSafeCash.AccTransNo == null || accSafeCash.AccTransNo == 0)
                        {
                            if (systemTable.SysValue == 1)
                            {
                                llDoc = (int)(await _Context.AccTransMasters.Where(obj => obj.CoCode == accSafeCash.BranchId && obj.FYear == accSafeCash.FYear && obj.TransType == userJournalType.JournalCode.ToString() && obj.FMonth == accSafeCash.FMonth).MaxAsync(obj => obj.TransNo)) + 1;
                            }
                            else if (systemTable.SysValue == 2 || systemTable.SysValue == 3)
                            {
                                llDoc = (int)(await _Context.AccTransMasters.Where(obj => obj.CoCode == accSafeCash.BranchId && obj.FYear == accSafeCash.FYear && obj.TransType == userJournalType.JournalCode.ToString()).MaxAsync(obj => obj.TransNo)) + 1;
                            }
                            else
                            {
                                return "برجاء تحديد مسلسل الحركة شهري ام ثانوي اولا ثم اعد المحاولة";
                            }
                            lsYearTransNo = accSafeCash.FYear + "_" + llDoc;
                        }
                        else
                        {
                            llDoc = (int)accSafeCash.AccTransNo;
                        }

                        //Istert AccTransMaster

                        AccTransMaster newAccTransMaster = new AccTransMaster();
                        newAccTransMaster.CoCode = accSafeCash.BranchId;
                        newAccTransMaster.FYear = accSafeCash.FYear;
                        newAccTransMaster.TransType = userJournalType.JournalCode.ToString();
                        newAccTransMaster.TransNo = llDoc;
                        newAccTransMaster.TransDate = accSafeCash.DocDate;
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
                        await _Context.SaveChangesAsync();
                        /////////////////////////////////////////////////////
                        if ((userCashNo.MainCode).Substring(0, 1) == "1" || (userCashNo.MainCode).Substring(0, 1) == "2")
                        {
                            lsCostCenterCode = null;
                        }
                        else
                        {
                            lsCostCenterCode = accSafeCash.CostCenterCode.ToString();
                        }
                       var llSerial = 2;

                        //Istert AccTransDetails

                        AccTransDetail newAccTransDetail = new AccTransDetail();
                        newAccTransDetail.CoCode = accSafeCash.BranchId;
                        newAccTransDetail.FYear = accSafeCash.FYear;
                        newAccTransDetail.TransType = userJournalType.JournalCode.ToString();
                        newAccTransDetail.TransNo = llDoc;
                        newAccTransDetail.TransDate = accSafeCash.DocDate;
                        newAccTransDetail.TransSerial = llSerial;
                        newAccTransDetail.MainCode = userCashNo.MainCode;
                        newAccTransDetail.SubCode = userCashNo.SubCode;
                        newAccTransDetail.ValDep = 0;
                        newAccTransDetail.ValCredit = ldValueEgy;
                        newAccTransDetail.ValDepCur = 0;
                        newAccTransDetail.ValCreditCur = accSafeCash.ValuePay;
                        newAccTransDetail.DocNo = accSafeCash.DocNo.ToString();
                        newAccTransDetail.DocStatus = null;
                        newAccTransDetail.DocDate = accSafeCash.DocDate;
                        newAccTransDetail.CostCenterCode = lsCostCenterCode;
                        newAccTransDetail.AccName = lsAccNameCredit;
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

                        //Istert AccTransDetails Discount
                        List<CustCollectionsDiscount> custCollectionsDiscounts = await _Context.custCollectionsDiscounts.Where(obj => obj.SafeCashId == accSafeCash.SafeCashId).ToListAsync();
                        var totalDiscValue = 0.0;
                        var llSeriall = 2;
                        string lsCostCenterCodeC;
                        var ldDiscValueAll = 0.0;
                        if (custCollectionsDiscounts.Count != 0)
                        {
                            foreach (var item in custCollectionsDiscounts)
                            {
                                var lsMainCode = item.MainCode;
                                var lsSubCode = item.SubCode;
                                var ldDiscValue = item.DiscValue;
                                totalDiscValue += (double)ldDiscValue;
                                ldDiscValueAll = totalDiscValue;
                                var lsAccNameCreditC = await GetAccountName(item.MainCode, item.SubCode);
                                llSeriall++;
                                if ((item.MainCode).Substring(0, 1) == "1" || (item.MainCode).Substring(0, 1) == "2")
                                {
                                    lsCostCenterCodeC = null;
                                }
                                else
                                {
                                    lsCostCenterCodeC = accSafeCash.CostCenterCode.ToString();
                                }

                                //Inserting
                                AccTransDetail newAccTransDetailC = new AccTransDetail();

                                newAccTransDetailC.CoCode = accSafeCash.BranchId;
                                newAccTransDetailC.FYear = accSafeCash.FYear;
                                newAccTransDetailC.TransType = userJournalType.JournalCode.ToString();
                                newAccTransDetailC.TransNo = llDoc;
                                newAccTransDetailC.TransDate = accSafeCash.DocDate;
                                newAccTransDetailC.TransSerial = llSeriall;
                                newAccTransDetailC.MainCode = item.MainCode;
                                newAccTransDetailC.SubCode = item.SubCode;
                                newAccTransDetailC.ValDep = 0;
                                newAccTransDetailC.ValCredit = ldDiscValue;
                                newAccTransDetailC.ValDepCur = 0;
                                newAccTransDetailC.ValCreditCur = ldDiscValue;
                                newAccTransDetailC.DocNo = accSafeCash.DocNo.ToString();
                                newAccTransDetailC.DocStatus = null;
                                newAccTransDetailC.DocDate = accSafeCash.DocDate;
                                newAccTransDetailC.CostCenterCode = lsCostCenterCodeC;
                                newAccTransDetailC.AccName = lsAccNameCreditC;
                                newAccTransDetailC.LineDesc = lsCommentt;
                                newAccTransDetailC.OkPost = "0";
                                newAccTransDetailC.CurCode = accSafeCash.CurCode.ToString();
                                newAccTransDetailC.DocCode = null;
                                newAccTransDetailC.YearTransNo = lsYearTransNo;
                                DateTime dpsDate3 = (DateTime)accSafeCash.DocDate;
                                int monthNumber3 = dpsDate.Month;
                                newAccTransDetailC.FMonth = monthNumber3;
                                newAccTransDetailC.UserCode = accSafeCash.UserCode;
                                newAccTransDetailC.EntryType = accSafeCash.EntryType;
                                newAccTransDetailC.EntryDate = DateTime.Now;
                                newAccTransDetailC.MCodeDtl = accSafeCash.MCodeDtl; ;
                                newAccTransDetailC.TransId = newAccTransMaster.TransId != 0 ? newAccTransMaster.TransId : 0;
                                await _Context.AddAsync(newAccTransDetailC);
                                await _Context.SaveChangesAsync();

                            }
                        }
                        //Inserting
                        string lsCostCenterCodeC_d;
                        if ((accSafeCash.MainCode).Substring(0, 1) == "1" || (accSafeCash.MainCode).Substring(0, 1) == "2")
                        {
                            lsCostCenterCodeC_d = null;
                        }
                        else
                        {
                            lsCostCenterCodeC_d = accSafeCash.CostCenterCode.ToString();
                        }
                        AccTransDetail newAccTransDetailC_d = new AccTransDetail();

                        newAccTransDetailC_d.CoCode = accSafeCash.BranchId;
                        newAccTransDetailC_d.FYear = accSafeCash.FYear;
                        newAccTransDetailC_d.TransType = userJournalType.JournalCode.ToString();
                        newAccTransDetailC_d.TransNo = llDoc;
                        newAccTransDetailC_d.TransDate = accSafeCash.DocDate;
                        newAccTransDetailC_d.TransSerial = 1;
                        newAccTransDetailC_d.MainCode = accSafeCash.MainCode;
                        newAccTransDetailC_d.SubCode = accSafeCash.SubCode;
                        newAccTransDetailC_d.ValDep = ldValueEgy + (decimal)ldDiscValueAll;
                        newAccTransDetailC_d.ValCredit = 0;
                        newAccTransDetailC_d.ValDepCur = accSafeCash.ValuePay;
                        newAccTransDetailC_d.ValCreditCur = 0;
                        newAccTransDetailC_d.DocNo = accSafeCash.DocNo.ToString();
                        newAccTransDetailC_d.DocStatus = null;
                        newAccTransDetailC_d.DocDate = accSafeCash.DocDate;
                        newAccTransDetailC_d.CostCenterCode = lsCostCenterCodeC_d;
                        newAccTransDetailC_d.AccName = lsAccNameDept;
                        newAccTransDetailC_d.LineDesc = lsCommentt;
                        newAccTransDetailC_d.OkPost = "0";
                        newAccTransDetailC_d.CurCode = accSafeCash.CurCode.ToString();
                        newAccTransDetailC_d.DocCode = null;
                        newAccTransDetailC_d.YearTransNo = lsYearTransNo;
                        DateTime dpsDate4 = (DateTime)accSafeCash.DocDate;
                        int monthNumber4 = dpsDate.Month;
                        newAccTransDetailC_d.FMonth = monthNumber4;
                        newAccTransDetailC_d.UserCode = accSafeCash.UserCode;
                        newAccTransDetailC_d.EntryType = accSafeCash.EntryType;
                        newAccTransDetailC_d.EntryDate = DateTime.Now;
                        newAccTransDetailC_d.MCodeDtl = accSafeCash.MCodeDtl; ;
                        newAccTransDetailC_d.TransId = newAccTransMaster.TransId != 0 ? newAccTransMaster.TransId : 0;
                        await _Context.AddAsync(newAccTransDetailC_d);

                        accSafeCash.AccTransNo = llDoc;
                        _Context.Update(accSafeCash);
                        int result = await _Context.SaveChangesAsync(); //for return message after enhance method
                        if (result == 0)
                        {
                            return "Failed to post in Account.";
                        }
                        else
                        {
                            return "Posting to Account Done.";

                        }
                    }
                }
            }
            return "Failed to post in Account.";
        }
    }
}
