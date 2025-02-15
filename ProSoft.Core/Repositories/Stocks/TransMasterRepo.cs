using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;
using System.Globalization;

namespace ProSoft.Core.Repositories.Stocks
{
    public class TransMasterRepo : Repository<TransMaster, int>, ITransMasterRepo
    {
        private readonly IMapper _mapper;
        public TransMasterRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<TransMasterEditAddDTO> GetPermissionFormByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            permissionFormDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);

            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;
            return permissionFormDTO;
        }

        public async Task<List<StockViewDTO>> GetActiveStocksForUserAsync(int userCode)
        {
            List<StockEmp> stockTransList = await _Context.StockEmps
                .Where(obj => obj.UserId == userCode && obj.StockDef == 1).ToListAsync();
            List<Stock> stocksList = await _Context.Stocks.ToListAsync();

            var stocksDTO = new List<StockViewDTO>();
            var isExisted = false;
            foreach (var stock in stocksList)
            {
                foreach (var stockTrans in stockTransList)
                {
                    if (stock.Stkcod == stockTrans.Stkcod)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (isExisted)
                {
                    var stockDTO = new StockViewDTO();
                    stockDTO.Stkcod = stock.Stkcod;
                    stockDTO.Stknam = (await _Context.Stocks
                        .FindAsync(stock.Stkcod)).Stknam;
                    stocksDTO.Add(stockDTO);
                }
            }
            return stocksDTO;
        }

        public async Task<List<PermissionDefViewDTO>> GetUserPermissionsForStockAsync(int userCode, int stockID)
        {
            List<StockEmp> stockTrans = await _Context.StockEmps
                .Where(obj => obj.UserId == userCode && obj.Stkcod == stockID && obj.StockDef == 1)
                .ToListAsync();

            var permissionsDTO = new List<PermissionDefViewDTO>();
            foreach (var item in stockTrans)
            {
                GeneralCode permission = await _Context.GeneralCodes
                    .FirstOrDefaultAsync(obj => obj.UniqueType == item.TransType);
                var permissionDTO = _mapper.Map<PermissionDefViewDTO>(permission);
                permissionsDTO.Add(permissionDTO);
            }
            return permissionsDTO;
        }

        public async Task<bool> CheckForDetailsAsync(int transMAsterID)
        {
            List<TransDtl> transDetails = await _Context.TransDtls
                .Where(obj => obj.TransMAsterID == transMAsterID).ToListAsync();
            return transDetails.Count != 0;
        }

        public async Task<int> IfPossibleToDeleteAsync(int transMAsterID)
        {
            TransMaster transMaster = await GetByIdAsync(transMAsterID);
            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == transMaster.UserCode &&
                obj.BranchId == transMaster.BranchId);

            AccTransMaster accTransMaster = await _Context.AccTransMasters.FirstOrDefaultAsync(obj =>
                obj.CoCode == transMaster.BranchId && obj.FYear == transMaster.FYear &&
                obj.TransType == userJournal.JournalCode.ToString() && obj.TransNo == transMaster.AccTransNo);

            return accTransMaster != null ? 1 : transMaster.Flag3 == "2" ? 2 : 3;
        }

        private async Task<string> GetAccountName(string mainCode, string subCode)
        {
            AccMainCode myMainCode = await _Context.AccMainCodes
                .FirstOrDefaultAsync(obj => obj.MainCode == mainCode);
            string mainName = myMainCode.MainName;

            AccSubCode mySubCode = await _Context.AccSubCodes
                .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == mainCode);
            string subName = mySubCode! != null ? mySubCode.SubName : string.Empty;

            return subName != "" ? $"{mainName} - {subName}" : mainName;
        }
        /// /////////////////////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////////////////////






        public async Task<int> CancelApprovePermissionAsync(int transMAsterID)
        {
            try
            {
                TransMaster transMaster = await GetByIdAsync(transMAsterID);
                transMaster.Flag3 = "1";
                await SaveChangesAsync();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

       public async Task<int> ApprovePermissionAsync(int transMAsterID, int userCode)
        {
            TransMaster transMaster = await GetByIdAsync(transMAsterID);
            transMaster.Flag3 = "2";

            if (transMaster.TransType == 2 && transMaster.StockCode2 > 0)
            {
                // لم يتم الترحيل للحسابات
                return 1;
            }

            var ls_flag3 = transMaster.Flag3;
            var sys_value = (await _Context.SystemTables.FirstOrDefaultAsync(obj =>
                obj.SysId == 5)).SysValue;

            var ll_stock_emp = 0;
            var ll_trans_type = transMaster.TransType;
            var ls_jornal = (await _Context.UserJournalTypes.FirstOrDefaultAsync(obj => obj.UserCode == userCode))?
                .JournalCode.ToString();
            var ll_acc_trans_no = transMaster.AccTransNo;
            var ll_acc_trans_no2 = transMaster.AccTransNo2;

            var ls_year_trans_no = $"{transMaster.FYear}_{ll_acc_trans_no}";
            var tot_trans_val = transMaster.TotTransVal; // اجمالي الاجمالي الخاص بال details
            var ls_comment = "";
            var sub_code_acc = "";
            var ll_doc_No = transMaster.DocNo;

            if (sys_value == 1 || ls_flag3 == "1")
            {
                // لم يتم الترحيل للحسابات
                return 1;
            }
            else
            {
                var ll_doc_no = transMaster.DocNo;
                // ترحيل تاريخ فاتورة المورد في قيد الاضافة
                var sys_value2 = (await _Context.SystemTables.FirstOrDefaultAsync(obj =>
                    obj.SysId == 22)).SysValue;
                if (sys_value2 == 1 && transMaster.TransType == 2)
                {
                    if (transMaster.SupInvDate != null)
                    {
                    }
                    else
                    {
                        transMaster.Flag3 = "1";
                        await SaveChangesAsync();
                        // من فضلك ادخل تاريخ فاتورة المورد
                        return 2;
                    }
                }
                var ll_year = transMaster.FYear;
                var ll_month = transMaster.FMonth;
                var ll_branch = transMaster.BranchId;
                //var ll_trans_type = transMaster.TransType;
                var ls_des = ll_trans_type == 2 ? "اذن اضافة رقم" :
                    ll_trans_type == 13 ? "اذن تحويل رقم" :
                    ll_trans_type == 12 ? "فاتورة العميل رقم" :
                    ll_trans_type == 23 ? "اذن استلام رقم" : "";
                //var tot_trans_val = transMaster.TotTransVal; // اجمالي الاجمالي الخاص بال details
                if (tot_trans_val is (null or 0))
                {
                    // تاكد من قيمة الاذن واعد المحاولة
                    return 3;
                }
                var ll_stock = 0;
                //var ll_stock_emp = 0;
                var ll_to_stock = 0;
                if (ll_trans_type == 13)
                {
                    ll_stock = (int)transMaster.StockCode2;
                    ll_stock_emp = ll_stock;
                }
                else if (ll_trans_type == 23)
                {
                    ll_stock = (int)transMaster.StockCode;
                    ll_to_stock = (int)transMaster.StockCode2;
                    ll_stock_emp = ll_to_stock;
                }
                else
                {
                    ll_stock = (int)transMaster.StockCode;
                    ll_stock_emp = ll_stock;
                }
                var ls_stock_name = (await _Context.Stocks.FirstOrDefaultAsync(obj =>
                    obj.Stkcod == ll_stock && obj.BranchId == ll_branch)).Stknam;
                ls_comment = ls_des + ll_doc_no + ls_stock_name;
                var ll_sup_no = transMaster.SupNo;
                if ((ll_sup_no == null || ll_sup_no == "0") && ll_trans_type == 2)
                {
                    transMaster.Flag3 = "1";
                    // خطأ لم يتم الترحيل للحسابات
                    // قم باختيار المورد وأعد المحاولة
                    return 4;
                }
                var ll_cust_no = transMaster.CustNo;
                if ((ll_cust_no == null || ll_cust_no == "0") && ll_trans_type == 12)
                {
                    transMaster.Flag3 = "1";
                    // خطأ لم يتم الترحيل للحسابات
                    // قم باختيار العميل وأعد المحاولة
                    return 5;
                }
                //var ls_jornal = (await _Context.UserJournalTypes.FirstOrDefaultAsync(obj => obj.UserCode == userCode))?
                //    .JournalCode.ToString();
                if (ls_jornal is (null or ""))
                {
                    // لم يتم الترحيل للحسابات لعدم وجود يومية للمستخدم 
                    return 6;
                }
                //var ll_acc_trans_no = transMaster.AccTransNo;
                if (ll_acc_trans_no == null)
                {
                    ll_acc_trans_no = 0;
                }
                // سجل مرحل من قبل
                if (ll_acc_trans_no > 0)
                {
                    AccTransMaster accTransMaster = await _Context.AccTransMasters.FirstOrDefaultAsync(obj =>
                        obj.CoCode == transMaster.BranchId && obj.FYear == transMaster.FYear && obj.TransType == ls_jornal &&
                        obj.TransNo == ll_acc_trans_no);
                    var ls_post = accTransMaster.OkPost;
                    if (ls_post == "1") // مغلق
                    {
                        //messagebox('تنبيه', 'غير مسموح بالترحيل لاقفال القيد بالحسابات')
                        return 7;
                    }
                    else
                    {
                        AccTransDetail accTransDetail = await _Context.AccTransDetails.FirstOrDefaultAsync(obj =>
                            obj.CoCode == transMaster.BranchId && obj.FYear == transMaster.FYear && obj.TransType == ls_jornal &&
                            obj.TransNo == ll_acc_trans_no);
                        _Context.Remove(accTransDetail);
                        _Context.Remove(accTransMaster);
                        await _Context.SaveChangesAsync();
                    }
                }
                // سجل مرحل من قبل
                if (ll_acc_trans_no2 > 0)
                {
                    AccTransMaster accTransMaster2 = await _Context.AccTransMasters.FirstOrDefaultAsync(obj =>
                        obj.CoCode == transMaster.BranchId && obj.FYear == transMaster.FYear && obj.TransType == ls_jornal &&
                        obj.TransNo == ll_acc_trans_no2);
                    AccSafeCash accSafeCash = await _Context.AccSafeCashes.FirstOrDefaultAsync(obj=>obj.BranchId == transMaster.BranchId && obj.FYear == transMaster.FYear &&obj.DocType == "SFCOT" && obj.AccTransNo == ll_acc_trans_no2);                    
                    var ls_post = accTransMaster2.OkPost;
                    if (ls_post == "1") // مغلق
                    {
                        //messagebox('تنبيه', 'غير مسموح بالترحيل لاقفال القيد بالحسابات')
                        return 7;
                    }
                    else
                    {
                        AccTransDetail accTransDetail2 = await _Context.AccTransDetails.FirstOrDefaultAsync(obj =>
                            obj.CoCode == transMaster.BranchId && obj.FYear == transMaster.FYear && obj.TransType == ls_jornal &&
                            obj.TransNo == ll_acc_trans_no2);
                        _Context.Remove(accTransDetail2);
                        _Context.Remove(accTransMaster2);
                        _Context.Remove(accSafeCash);
                        await _Context.SaveChangesAsync();
                    }
                }
                else if (ll_acc_trans_no == 0) // سجل جديد
                {
                    var ll_sys_table = (await _Context.SystemTables.FirstOrDefaultAsync(obj => obj.SysId == 2)).SysValue;
                    if (ll_sys_table == 1)
                    {
                        var max = await _Context.AccTransMasters
                            .Where(obj => obj.CoCode == transMaster.BranchId && obj.TransType == ls_jornal && obj.FYear == transMaster.FYear
                            && obj.FMonth == ll_month)
                            .MaxAsync(obj => obj.TransNo);
                        ll_acc_trans_no = (max == null || max == 0) ? 1 : max + 1;
                        ls_year_trans_no = $"{transMaster.FYear}_{ll_acc_trans_no}";
                    }
                    else if (ll_sys_table is (2 or 3))
                    {
                        var max = await _Context.AccTransMasters
                            .Where(obj => obj.CoCode == transMaster.BranchId && obj.TransType == ls_jornal && obj.FYear == transMaster.FYear)
                            .MaxAsync(obj => obj.TransNo);
                        ll_acc_trans_no = (max == null || max == 0) ? 1 : max + 1;
                        ls_year_trans_no = $"{transMaster.FYear}_{ll_acc_trans_no}";
                    }
                    else
                    {
                        //messagebox("تنبيـه", "برجاء تحديد مسلسل الحركة شهرى أم سنوى أولا ثم أعد المحاولة")
                        return 8;
                    }
                }
                //var ls_year_trans_no = $"{transMaster.FYear}_{ll_acc_trans_no}";
                StockEmp stockEmp = await _Context.StockEmps.FirstOrDefaultAsync(obj => obj.Stkcod == ll_stock_emp
                    && obj.UserId == userCode && obj.TransType == ll_trans_type);
                var main_code_stk = stockEmp.MainCodeStk;
                if (main_code_stk.Length <= 0 || main_code_stk.Length == null)
                {
                    transMaster.AccTransNo = null;
                    transMaster.AccTransType = null;
                    ////		messagebox('تنبيه','برجاء ادخال الحساب الرئيسى المدين  فى شاشة ريط المستخدم بالمخازن والحسابات')
                    return 0;
                }
                if (ll_trans_type == 30) // اذن ارتجاع لمورد
                {
                    SupCode supCode = await _Context.SupCodes.FirstOrDefaultAsync(obj =>
                        obj.Sup.ToString() == ll_sup_no && obj.BranchId == transMaster.BranchId);
                    var sub_code_stk = supCode.SubCode;
                }
                else if (ll_trans_type == 12) // فاتورة عميل
                {
                    CustCode custCode = await _Context.CustCodes.FirstOrDefaultAsync(obj =>
                        obj.Cust.ToString() == ll_cust_no && obj.BranchId == transMaster.BranchId);
                    var sub_code_stk = custCode.SubCode;
                }
                else
                {
                    if (stockEmp.SubCodeAcc == null)
                    {
                        SupCode? supCode = await _Context.SupCodes.FirstOrDefaultAsync(obj =>
                            obj.Sup.ToString() == ll_sup_no && obj.BranchId == transMaster.BranchId);
                        sub_code_acc = supCode != null? supCode.SubCode:null;
                        if (ll_sup_no != null && int.Parse(ll_sup_no) > 0)
                        {
                            if (sub_code_acc == null && ll_trans_type == 2)
                            {
                                transMaster.Flag3 = "1";
                                // messagebox('خطأ فى الترحيل للحسابات','هذا المورد غير مرتبط بالحسابات')
                                return 9;
                            }
                        }
                        /////////////////////////////////////
                        if (ll_cust_no != null && int.Parse(ll_cust_no) > 0)
                        {
                            CustCode custCode = await _Context.CustCodes.FirstOrDefaultAsync(obj =>
                                obj.Cust.ToString() == ll_cust_no && obj.BranchId == transMaster.BranchId);
                            var sub_code_stk = custCode.SubCode;
                            if (sub_code_acc == null && ll_trans_type == 12)
                            {
                                transMaster.Flag3 = "1";
                                // messagebox('خطأ فى الترحيل للحسابات','هذا العميل غير مرتبط بالحسابات')
                                return 10;
                            }
                        }
                    }
                }
            }
            ///////////////////////////////////////
            // Insert
            StockEmp stockEmp2 = await _Context.StockEmps.FirstOrDefaultAsync(obj => obj.Stkcod == ll_stock_emp
                && obj.UserId == userCode && obj.TransType == ll_trans_type);
            var main_name_stk = stockEmp2.MainCodeStk;
            var sub_name_stk = stockEmp2.SubCodeStk;
            var main_name_acc = stockEmp2.MainCodeAcc;
            var sub_name_acc = stockEmp2.SubCodeAcc;
            var ls_acc_name_depit = await GetAccountName(stockEmp2.MainCodeStk, stockEmp2.SubCodeStk); //$"{main_code_stk} - {sub_code_stk}";	//  الطرف المدين
            var ls_acc_name_credit = await GetAccountName(stockEmp2.MainCodeAcc, stockEmp2.SubCodeAcc); // $"{main_code_acc} - {sub_code_acc}";	//  الطرف الدائن

            var li_m_code_dtl = ll_trans_type; // نوع اذن المخازن

            ///////////////////
            //AccTransMaster
            var module_id = 2; // f_ret_module_code('المخازن ومراقبة المخزون')	// m_code

            var newAccTransMaster = new AccTransMaster
            {
                CoCode = transMaster.BranchId,
                FYear = transMaster.FYear,
                YearTransNo = ls_year_trans_no,
                FMonth = transMaster.FMonth,
                TransType = ls_jornal,
                TransNo = (int)ll_acc_trans_no,
                TransDate = transMaster.DocDate,
                TransDesc = ls_comment,
                TotalTrans = tot_trans_val,
                OkPost = "0",
                CurCode = "1",
                MCode = module_id,
                MCodeDtl = li_m_code_dtl,
                DocDate = transMaster.DocDate,
                EntryDate = DateTime.Now
            };
            await _Context.AddAsync(newAccTransMaster);
            await SaveChangesAsync();

            AccTransMaster addedTransMaster = await _Context.AccTransMasters.FirstOrDefaultAsync(obj => obj.TransType.ToString() == ls_jornal &&
                obj.YearTransNo == ls_year_trans_no && obj.FMonth == transMaster.FMonth);

            // من حساب
            var newAccTransDetail_1 = new AccTransDetail
            {
                CoCode = transMaster.BranchId,
                FYear = transMaster.FYear,
                FMonth = transMaster.FMonth,
                TransType = ls_jornal,
                TransNo = (int)ll_acc_trans_no,
                YearTransNo = ls_year_trans_no,
                TransDate = transMaster.DocDate,
                TransSerial = 1,
                MainCode = stockEmp2.MainCodeStk,
                SubCode = stockEmp2.SubCodeStk,
                ValDep = tot_trans_val,
                ValCredit = 0,
                ValDepCur = 0,
                ValCreditCur = 0,
                DocNo = ll_doc_No.ToString(),
                DocStatus = null,
                DocDate = transMaster.DocDate,
                CostCenterCode = null,
                AccName = ls_acc_name_depit,
                LineDesc = ls_comment,
                OkPost = "0",
                CurCode = "1",
                DocCode = null,
                MCodeDtl = li_m_code_dtl,
                TransId = addedTransMaster.TransId,
                UserCode = userCode,
                EntryDate = DateTime.Now
            };

            // الي حساب
            var newAccTransDetail_2 = new AccTransDetail
            {
                CoCode = transMaster.BranchId,
                FYear = transMaster.FYear,
                FMonth = transMaster.FMonth,
                TransType = ls_jornal,
                TransNo = (int)ll_acc_trans_no,
                YearTransNo = ls_year_trans_no,
                TransDate = transMaster.DocDate,
                TransSerial = 2,
                MainCode = stockEmp2.MainCodeAcc,
                SubCode = stockEmp2.SubCodeAcc,
                ValDep = 0,
                ValCredit = tot_trans_val,
                ValDepCur = 0,
                ValCreditCur = 0,
                DocNo = ll_doc_No.ToString(),
                DocStatus = null,
                DocDate = transMaster.DocDate,
                CostCenterCode = null,
                AccName = ls_acc_name_credit,
                LineDesc = ls_comment,
                OkPost = "0",
                CurCode = "1",
                DocCode = null,
                MCodeDtl = li_m_code_dtl,
                TransId = addedTransMaster.TransId,
                UserCode = userCode,
                EntryDate = DateTime.Now
            };
            await _Context.AddAsync(newAccTransDetail_1);
            await _Context.AddAsync(newAccTransDetail_2);








            /////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////
            // في حالة نقدي
            if (transMaster.Pay =="c") 
            {
                ///////////////////////////////////////
                UserCashNo userCashNo = await _Context.userCashNos.FirstOrDefaultAsync(obj=>obj.UserCode ==userCode &&obj.BranchId == transMaster.BranchId);
                if(userCashNo ==null)
                {
                    // لم يتم الترحيل للحسابات
                    return 1;
                }
                // Insert
                StockEmp stockEmp3 = await _Context.StockEmps.FirstOrDefaultAsync(obj => obj.Stkcod == ll_stock_emp
                    && obj.UserId == userCode && obj.TransType == ll_trans_type);
                var main_name_stk_1 = stockEmp3.MainCodeStk;
                var sub_name_stk_1 = stockEmp3.SubCodeStk;
                var main_name_acc_1 = stockEmp3.MainCodeAcc;
                var sub_name_acc_1 = stockEmp3.SubCodeAcc;
                var ls_acc_name_depit_1 = await GetAccountName(stockEmp3.MainCodeAcc, stockEmp3.SubCodeAcc); // $"{main_code_acc} - {sub_code_acc}";	//  الطرف الدائن
                var ls_acc_name_credit_1 = await GetAccountName(userCashNo.MainCode, userCashNo.SubCode); //$"{main_code_stk} - {sub_code_stk}";	//  الطرف المدين
                ll_acc_trans_no2 = ll_acc_trans_no + 1;
                ls_year_trans_no = $"{transMaster.FYear}_{ll_acc_trans_no2}";
                var li_safe_code = userCashNo.SafeCode;
                var ls_acc_name = await GetAccountName(stockEmp3.MainCodeAcc, stockEmp3.SubCodeAcc);
                var entry_type = "";

               var maxDoNumber = await _Context.AccSafeCashes
                    .Where(obj => obj.BranchId == transMaster.BranchId
                               && obj.FYear == transMaster.FYear
                               && obj.DocType == "SFCOT"
                               && obj.SafeCode == li_safe_code)
                    .MaxAsync(obj =>obj.DocNo);   
                var ll_doc_No_safe = maxDoNumber + 1;

                ////Insert in accsafecash اذن صرف في الخزنة
                AccSafeCash newAccSafeCash = new AccSafeCash();
                newAccSafeCash.CoCode = 1;
                newAccSafeCash.FYear = transMaster.FYear;
                newAccSafeCash.DocType = "SFCOT";
                newAccSafeCash.DocNo = ll_doc_No_safe;
                newAccSafeCash.DocDate = transMaster.DocDate;
                newAccSafeCash.CurCode = 1;
                newAccSafeCash.PersonName = "";
                newAccSafeCash.ValuePay = transMaster.TotTransVal;
                newAccSafeCash.Commentt = ls_acc_name;
                newAccSafeCash.CurSer = 1;
                newAccSafeCash.Flag = 1;
                newAccSafeCash.Rate1 = 1;
                newAccSafeCash.FMonth = transMaster.FMonth;
                newAccSafeCash.SafeCode = li_safe_code;
                newAccSafeCash.BranchId = transMaster.BranchId;
                newAccSafeCash.MainCode = stockEmp3.MainCodeAcc;
                newAccSafeCash.SubCode = stockEmp3.SubCodeAcc;
                newAccSafeCash.AccName = ls_acc_name;
                newAccSafeCash.AccTransType = int.Parse(ls_jornal);
                newAccSafeCash.DiscountVal = 0;
                newAccSafeCash.AprovedFlag = "APR";
                newAccSafeCash.UserCode = userCode;
                newAccSafeCash.EntryType = null;
                newAccSafeCash.AccTransNo = ll_acc_trans_no2;
                newAccSafeCash.EntryDate = DateTime.Now;
                newAccSafeCash.MCodeDtl = li_m_code_dtl;
                await _Context.AddAsync(newAccSafeCash);
                //////////////////////////////////
                //var li_m_code_dtl = ll_trans_type; // نوع اذن المخازن

                ///////////////////
                //AccTransMaster
                //var module_id = 2; // f_ret_module_code('المخازن ومراقبة المخزون')	// m_code

                var newAccTransMaster_2 = new AccTransMaster
                {
                    CoCode = transMaster.BranchId,
                    FYear = transMaster.FYear,
                    YearTransNo = ls_year_trans_no,
                    FMonth = transMaster.FMonth,
                    TransType = ls_jornal,
                    TransNo = (int)ll_acc_trans_no2,
                    TransDate = transMaster.DocDate,
                    TransDesc = ls_comment,
                    TotalTrans = tot_trans_val,
                    OkPost = "0",
                    CurCode = "1",
                    MCode = module_id,
                    MCodeDtl = li_m_code_dtl,
                    DocDate = transMaster.DocDate,
                    EntryDate = DateTime.Now
                };
                await _Context.AddAsync(newAccTransMaster_2);
                await SaveChangesAsync();

                AccTransMaster addedTransMaster_2 = await _Context.AccTransMasters.FirstOrDefaultAsync(obj => obj.TransType.ToString() == ls_jornal &&
                    obj.YearTransNo == ls_year_trans_no && obj.FMonth == transMaster.FMonth);

                // من حساب
                var newAccTransDetail_1_2 = new AccTransDetail
                {
                    CoCode = transMaster.BranchId,
                    FYear = transMaster.FYear,
                    FMonth = transMaster.FMonth,
                    TransType = ls_jornal,
                    TransNo = (int)ll_acc_trans_no,
                    YearTransNo = ls_year_trans_no,
                    TransDate = transMaster.DocDate,
                    TransSerial = 1,
                    MainCode = stockEmp3.MainCodeStk,
                    SubCode = stockEmp3.SubCodeStk,
                    ValDep = tot_trans_val,
                    ValCredit = 0,
                    ValDepCur = 0,
                    ValCreditCur = 0,
                    DocNo = ll_doc_No.ToString(),
                    DocStatus = null,
                    DocDate = transMaster.DocDate,
                    CostCenterCode = null,
                    AccName = ls_acc_name_depit_1,
                    LineDesc = ls_comment,
                    OkPost = "0",
                    CurCode = "1",
                    DocCode = null,
                    MCodeDtl = li_m_code_dtl,
                    TransId = addedTransMaster_2.TransId,
                    UserCode = userCode,
                    EntryDate = DateTime.Now
                };

                // الي حساب
                var newAccTransDetail_2_2 = new AccTransDetail
                {
                    CoCode = transMaster.BranchId,
                    FYear = transMaster.FYear,
                    FMonth = transMaster.FMonth,
                    TransType = ls_jornal,
                    TransNo = (int)ll_acc_trans_no2,
                    YearTransNo = ls_year_trans_no,
                    TransDate = transMaster.DocDate,
                    TransSerial = 2,
                    MainCode = userCashNo.MainCode,
                    SubCode = userCashNo.SubCode,
                    ValDep = 0,
                    ValCredit = tot_trans_val,
                    ValDepCur = 0,
                    ValCreditCur = 0,
                    DocNo = ll_doc_No.ToString(),
                    DocStatus = null,
                    DocDate = transMaster.DocDate,
                    CostCenterCode = null,
                    AccName = ls_acc_name_credit_1,
                    LineDesc = ls_comment,
                    OkPost = "0",
                    CurCode = "1",
                    DocCode = null,
                    MCodeDtl = li_m_code_dtl,
                    TransId = addedTransMaster.TransId,
                    UserCode = userCode,
                    EntryDate = DateTime.Now
                };
                await _Context.AddAsync(newAccTransDetail_1_2);
                await _Context.AddAsync(newAccTransDetail_2_2);

            }

            transMaster.AccTransNo = ll_acc_trans_no2;
            transMaster.AccTransType = ls_jornal;
            transMaster.Flag3 = "2";

            await SaveChangesAsync();

            // تم الترحيل للحسابات
            return 11;
        }

        public async Task DeletePermissionFormAsync(int transMAsterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMAsterID);
            List<TransDtl> transDtls = await _Context.TransDtls.Where(obj => obj.TransMAsterID == transMAsterID)
                .ToListAsync();

            if (permissionForm.TransType == 13)
            {
                TransMaster receiveTransMaster = await _DbSet.FirstOrDefaultAsync(obj =>
                    obj.TransType == 23 && obj.DocNoFr == permissionForm.DocNo &&
                    obj.StockCode == permissionForm.StockCode2);
                List<TransDtl> receiveTransDtls = await _Context.TransDtls.Where(obj =>
                    obj.TransMAsterID == receiveTransMaster.TransMAsterID)
                    .ToListAsync();

                foreach (var item in receiveTransDtls)
                    _Context.Remove(item);

                await DeleteAsync(receiveTransMaster);
            }

            foreach (var item in transDtls)
                _Context.Remove(item);

            await DeleteAsync(permissionForm);
            await SaveChangesAsync();
        }

        public async Task<TransMasterViewDTO> GetTransMasterByAsync(int transType, int stockCode, int deletedId)
        {
            var transMaster = await _DbSet.OrderBy(obj=>obj.TransMAsterID).LastOrDefaultAsync(obj => obj.TransType == transType && obj.StockCode == stockCode
                && obj.TransMAsterID != deletedId);
            var transMasterDTO = _mapper.Map<TransMasterViewDTO>(transMaster);
            return transMasterDTO;
        }

        public async Task<TransMasterViewDTO> GetForViewAsync(TransMaster permissionForm)
        {
            if (permissionForm != null)
            {
                var permFormDTO = _mapper.Map<TransMasterViewDTO>(permissionForm);

                permFormDTO.PermissionName = (await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionForm.TransType)).GDesc;
                permFormDTO.StockName = (await _Context.Stocks
                    .FirstOrDefaultAsync(obj => obj.Stkcod == permissionForm.StockCode)).Stknam;
                if (permissionForm.StockCode2 is not (null or 0))
                {
                    permFormDTO.StockName2 = (await _Context.Stocks
                    .FirstOrDefaultAsync(obj => obj.Stkcod == permissionForm.StockCode2)).Stknam;
                }
                permFormDTO.UserName = (await _Context.Users
                    .FirstOrDefaultAsync(obj => obj.UserCode == permissionForm.UserCode)).UserName;
                if (permissionForm.SupNo is not (null or "0"))
                {
                    permFormDTO.SupplierName = (await _Context.SupCodes
                        .FirstOrDefaultAsync(obj => obj.SupCode1 == permissionForm.SupNo)).SupName;
                }
                StockEmp stockTrans = await _Context.StockEmps.FirstOrDefaultAsync(obj => 
                    obj.Stkcod == permissionForm.StockCode && obj.TransType == permissionForm.TransType &&
                    obj.UserId == permFormDTO.UserCode);
                permFormDTO.ShowTransPrice = (int)stockTrans.ShowPrice;
                return permFormDTO;
            }
            return null;
        }

        public async Task<List<TransMasterViewDTO>> GetPermissionsFormsAsync(int stockID, int transType)
        {
            List<TransMaster> permissionsForms = await _DbSet.Where(obj => obj.StockCode == stockID
                && obj.TransType == transType && obj.Flag3 == "1").OrderByDescending(obj => obj.DocNo).ToListAsync();
            var permissionsFormsDTO = new List<TransMasterViewDTO>();
            foreach (var item in permissionsForms)
            {
                TransMasterViewDTO permFormDTO = await GetForViewAsync(item);
                StockEmp stockTrans = await _Context.StockEmps.FirstOrDefaultAsync(obj => obj.Stkcod == stockID &&
                    obj.TransType == transType && obj.UserId == permFormDTO.UserCode);
                if(stockTrans != null)
                    permFormDTO.ShowTransPrice = (int)stockTrans.ShowPrice;
                
                permissionsFormsDTO.Add(permFormDTO);
            }
            return permissionsFormsDTO;
        }
        /// <summary>

        public async Task<List<TransMasterViewDTO>> GetAllPermissionsFormsAsync(int stockID, int transType, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod, string supCode)
        {
            List<TransMaster> permissionsForms = await _DbSet.Where(obj => obj.StockCode == stockID
                && obj.TransType == transType &&
                      (fromReceipt == null || fromReceipt == 0 || obj.DocNo >= fromReceipt) &&
                      (toReceipt == null || toReceipt == 0 || obj.DocNo <= toReceipt) &&
                      (fromPeriod == null || obj.DocDate >= fromPeriod) &&
                      (toPeriod == null || obj.DocDate <= toPeriod) && (supCode ==null || obj.SupNo == supCode)).OrderByDescending(obj => obj.DocNo).ToListAsync();
            var permissionsFormsDTO = new List<TransMasterViewDTO>();
            foreach (var item in permissionsForms)
            {
                TransMasterViewDTO permFormDTO = await GetForViewAsync(item);
                StockEmp stockTrans = await _Context.StockEmps.FirstOrDefaultAsync(obj => obj.Stkcod == stockID &&
                    obj.TransType == transType && obj.UserId == permFormDTO.UserCode);
                if (stockTrans != null)
                    permFormDTO.ShowTransPrice = (int)stockTrans.ShowPrice;

                permissionsFormsDTO.Add(permFormDTO);
            }
            return permissionsFormsDTO;
        }
        public async Task<List<SupCodeViewDTO>> GetAllSuPCodesAsync()
        {
            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            List<SupCodeViewDTO> supCodeViewDTOs = _mapper.Map<List<SupCodeViewDTO>>(suppliers);
            return supCodeViewDTOs;
        }

        // Add Permission Form
        public async Task<TransMasterEditAddDTO> GetNewTransMasterAsync(int stockID, int userCode, int uniqueType)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            permissionFormDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            return permissionFormDTO;
        }

        public async Task<TransMaster> AddPermissionFormAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 0;
            permissionFormDTO.InvType = "0";
            permissionFormDTO.TransConfirm = 0;
            permissionFormDTO.ShowRow = 3;

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateTransMasterAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 0;
            permissionFormDTO.InvType = "0";
            permissionFormDTO.TransConfirm = 0;
            permissionFormDTO.ShowRow = 3;

            TransMaster permissionForm = await GetByIdAsync(id);
            permissionFormDTO.DocNo = permissionForm.DocNo;
            permissionFormDTO.SerSys = permissionForm.SerSys;

            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Disburse or Convert Permission => اذن صرف او تحويل

        public async Task<TransMasterEditAddDTO> GetDisburseOrConvertFormByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<Stock> stocks = await _Context.Stocks.Where(obj => obj.Stkcod != permissionFormDTO.StockCode).ToListAsync();
            permissionFormDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);

            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
                obj.BranchId == permissionFormDTO.BranchId);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMasterEditAddDTO> GetNewDisburseOrConvertFormAsync(int stockID, int userCode,
            int uniqueType, int branchID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<Stock> stocks = await _Context.Stocks.Where(obj => obj.Stkcod != stockID).ToListAsync();
            permissionFormDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == uniqueType
                && obj.StockCode == stockID && obj.BranchId == branchID
                && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ?
                filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;

            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == userCode && obj.BranchId == branchID);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMaster> AddDisburseOrConvertFormAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.Flag = "1";
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvNo = 0;
            permissionFormDTO.Flag3 = "1";
            permissionFormDTO.InvType = "0";
            permissionFormDTO.ShowRow = 3;

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            //if It Is a Convert Permission
            if (permissionForm.TransType == 13)
            {
                var ReceivePermissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
                ReceivePermissionForm.TransType = 23;
                ReceivePermissionForm.AddPers = 10;
                ReceivePermissionForm.CustDisc5 = 0;
                ReceivePermissionForm.CashAmount = 0;
                ReceivePermissionForm.DocNoFr = permissionForm.DocNo;
                var temp = ReceivePermissionForm.StockCode2;
                ReceivePermissionForm.StockCode2 = ReceivePermissionForm.StockCode;
                ReceivePermissionForm.StockCode = temp;

                var filteredPermissions2 = await _DbSet.Where(obj => obj.TransType == ReceivePermissionForm.TransType
                    && obj.StockCode == ReceivePermissionForm.StockCode &&
                    obj.BranchId == ReceivePermissionForm.BranchId && obj.FYear == ReceivePermissionForm.FYear)
                    .ToListAsync();
                ReceivePermissionForm.DocNo = filteredPermissions2.Count != 0 ?
                    filteredPermissions2.Max(obj => obj.DocNo) + 1 : 1;
                ReceivePermissionForm.SerSys = filteredPermissions2.Count != 0 ?
                    filteredPermissions2.Max(obj => obj.SerSys) + 1 : 1;

                await AddAsync(ReceivePermissionForm);
            }

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateDisburseOrConvertFormAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.Flag = "1";
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvNo = 0;
            permissionFormDTO.Flag3 = "1";
            permissionFormDTO.InvType = "0";
            permissionFormDTO.ShowRow = 3;

            TransMaster permissionForm = await GetByIdAsync(id);

            TransMaster receiveTransMaster = null;
            if (permissionForm.TransType == 13)
            {
                receiveTransMaster = await _DbSet.FirstOrDefaultAsync(obj => obj.TransType == 23 &&
                    obj.DocNoFr == permissionForm.DocNo && obj.StockCode == permissionForm.StockCode2);
                receiveTransMaster.StockCode = permissionFormDTO.StockCode2;
                receiveTransMaster.ModifyDate = DateTime.Now;
                await UpdateAsync(receiveTransMaster);

                List<TransDtl> receiveTransDtls = await _Context.TransDtls.Where(obj =>
                    obj.TransMAsterID == receiveTransMaster.TransMAsterID).ToListAsync();
                foreach (var item in receiveTransDtls)
                {
                    item.StockCode = permissionFormDTO.StockCode2;
                    item.ModifyDate = receiveTransMaster.ModifyDate;
                    _Context.Update(item);
                }
            }
            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Stock Receive Permission => اذن استلام من مخزن

        //public async Task<TransMasterEditAddDTO> GetStockReceiveFormByIdAsync(int transMasterID)
        //{
        //    TransMaster permissionForm = await GetByIdAsync(transMasterID);
        //    var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

        //    List<TransMaster> permissionForms = await _DbSet
        //        .Where(obj => obj.StockCode == permissionFormDTO.StockCode
        //        && obj.TransType == permissionFormDTO.TransType
        //        && obj.Flag3 == "1").ToListAsync();

        //    EisUserObject userObj = await _Context.EisUserObjects
        //        .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
        //        obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

        //    permissionFormDTO.EnableModify = userObj != null;

        //    List<Stock> stocks = await _Context.Stocks.Where(obj => obj.Stkcod != permissionFormDTO.StockCode).ToListAsync();
        //    permissionFormDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);

        //    Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
        //    GeneralCode permission = await _Context.GeneralCodes
        //        .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
        //    AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
        //        permissionFormDTO.UserCode);

        //    permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
        //        DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
        //    permissionFormDTO.PermissionsCount = permissionForms.Count();
        //    permissionFormDTO.StockName = stock?.Stknam;
        //    permissionFormDTO.PermissionName = permission?.GDesc;
        //    permissionFormDTO.UserName = user?.UserName;

        //    UserJournalType userJournal = await _Context.UserJournalTypes
        //        .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
        //        obj.BranchId == permissionFormDTO.BranchId);
        //    permissionFormDTO.JournalName = (await _Context.JournalTypes
        //        .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
        //        .JournalName;

        //    return permissionFormDTO;
        //}

        //public async Task UpdateStockReceiveFormAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        //{
        //    permissionFormDTO.RefDocNo = "0";
        //    permissionFormDTO.TotTransVal = 0;
        //    permissionFormDTO.Descount = 0;
        //    permissionFormDTO.CustDisc1 = 0;
        //    permissionFormDTO.StatusBal = "R";
        //    permissionFormDTO.DiscPers = 0;
        //    permissionFormDTO.Flag = "1";
        //    permissionFormDTO.SaleStatus = "N";
        //    permissionFormDTO.Flag2 = "0";
        //    permissionFormDTO.AmountVisa = 0;
        //    permissionFormDTO.CustDisc2 = 0;
        //    permissionFormDTO.CustDisc4 = 0;
        //    permissionFormDTO.TaxPrc = 0;
        //    permissionFormDTO.TaxValue = 0;
        //    permissionFormDTO.DueValue = 0;
        //    permissionFormDTO.InvNo = 0;
        //    permissionFormDTO.Flag3 = "1";
        //    permissionFormDTO.InvType = "0";
        //    permissionFormDTO.ShowRow = 3;
        //    permissionFormDTO.CustDisc5 = 0;
        //    permissionFormDTO.AddPers = 10;
        //    permissionFormDTO.CashAmount = 0;

        //    TransMaster permissionForm = await GetByIdAsync(id);
        //    _mapper.Map(permissionFormDTO, permissionForm);
        //    permissionForm.ModifyDate = DateTime.Now;

        //    await UpdateAsync(permissionForm);
        //    await SaveChangesAsync();
        //}

        //////////////////////////////////////////////////
        // Sales Invoice => فاتورة مبيعات

        public async Task<TransMasterEditAddDTO> GetSalesInvoiceByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<SafeName> receivers = await _Context.SafeNames.ToListAsync();
            permissionFormDTO.Receivers = _mapper.Map<List<TreasuryNameViewDTO>>(receivers);

            List<CustCode> customers = await _Context.CustCodes.ToListAsync();
            permissionFormDTO.Customers = _mapper.Map<List<CustCodeViewDTO>>(customers);

            List<SalesmenDatum> salesMen = await _Context.SalesmenData.ToListAsync();
            permissionFormDTO.SalesMen = _mapper.Map<List<SalesmenDataViewDTO>>(salesMen);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);

            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
                obj.BranchId == permissionFormDTO.BranchId);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMasterEditAddDTO> GetNewSalesInvoiceAsync(int stockID, int userCode,
            int uniqueType, int branchID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<SafeName> receivers = await _Context.SafeNames.ToListAsync();
            permissionFormDTO.Receivers = _mapper.Map<List<TreasuryNameViewDTO>>(receivers);

            List<CustCode> customers = await _Context.CustCodes.ToListAsync();
            permissionFormDTO.Customers = _mapper.Map<List<CustCodeViewDTO>>(customers);

            List<SalesmenDatum> salesMen = await _Context.SalesmenData.ToListAsync();
            permissionFormDTO.SalesMen = _mapper.Map<List<SalesmenDataViewDTO>>(salesMen);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == uniqueType
                && obj.StockCode == stockID && obj.BranchId == branchID
                && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;

            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == userCode && obj.BranchId == branchID);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMaster> AddSalesInvoiceAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.CustDisc5 = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvType = permissionFormDTO.InvType == "1" ? permissionFormDTO.InvType : "0";
            permissionFormDTO.PriceIncTax = permissionFormDTO.PriceIncTax == 1 ? permissionFormDTO.PriceIncTax : 0;
            permissionFormDTO.ShowRow = 3;

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateSalesInvoiceAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.CustDisc5 = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvType = permissionFormDTO.InvType == "1" ? permissionFormDTO.InvType : "0";
            permissionFormDTO.PriceIncTax = permissionFormDTO.PriceIncTax == 1 ? permissionFormDTO.PriceIncTax : 0;
            permissionFormDTO.ShowRow = 3;

            TransMaster permissionForm = await GetByIdAsync(id);
            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Debit or Credit Settlement => تسوية مدينةاو دائنة

        public async Task<TransMasterEditAddDTO> GetSettlementByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<AppUser> users = await _Context.Users.ToListAsync();
            permissionFormDTO.Users = _mapper.Map<List<UserDTO>>(users);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);

            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
                obj.BranchId == permissionFormDTO.BranchId);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMasterEditAddDTO> GetNewSettlementAsync(int stockID, int userCode,
            int uniqueType, int branchID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<AppUser> users = await _Context.Users.ToListAsync();
            permissionFormDTO.Users = _mapper.Map<List<UserDTO>>(users);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == uniqueType
                && obj.StockCode == stockID && obj.BranchId == branchID
                && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;

            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == userCode && obj.BranchId == branchID);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMaster> AddSettlementAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.RefDocNo = permissionFormDTO.RefDocNo is not (null or "") ?
                permissionFormDTO.RefDocNo : "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "S";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Pay = "Y";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = permissionFormDTO.CashAmount is not (null or 0) ?
                permissionFormDTO.CashAmount : 0;

            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.ShowRow = 3;

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateSettlementAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.RefDocNo = permissionFormDTO.RefDocNo is not (null or "") ?
                permissionFormDTO.RefDocNo : "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "S";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Pay = "Y";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = permissionFormDTO.CashAmount is not (null or 0) ?
                permissionFormDTO.CashAmount : 0;

            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.ShowRow = 3;

            TransMaster permissionForm = await GetByIdAsync(id);
            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Requirements Disburse Form => اذن صرف مستلزمات

        public async Task<TransMasterEditAddDTO> GetReqDisburseByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            permissionFormDTO.Stocks = await GetActiveStocksForUserAsync((int)permissionFormDTO.UserCode);

            List<CustCode> customers = await _Context.CustCodes.ToListAsync();
            permissionFormDTO.Customers = _mapper.Map<List<CustCodeViewDTO>>(customers);

            List<Pat> patients = await _Context.Pats.ToListAsync();
            permissionFormDTO.Patients = _mapper.Map<List<PatViewDTO>>(patients);

            List<SafeName> receivers = await _Context.SafeNames.ToListAsync();
            permissionFormDTO.Receivers = _mapper.Map<List<TreasuryNameViewDTO>>(receivers);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);
            
            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
                obj.BranchId == permissionFormDTO.BranchId);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMasterEditAddDTO> GetNewReqDisburseAsync(int stockID, int userCode,
            int uniqueType, int branchID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            permissionFormDTO.Stocks = await GetActiveStocksForUserAsync(userCode);

            List<CustCode> customers = await _Context.CustCodes.ToListAsync();
            permissionFormDTO.Customers = _mapper.Map<List<CustCodeViewDTO>>(customers);

            List<Pat> patients = await _Context.Pats.ToListAsync();
            permissionFormDTO.Patients = _mapper.Map<List<PatViewDTO>>(patients);

            List<SafeName> receivers = await _Context.SafeNames.ToListAsync();
            permissionFormDTO.Receivers = _mapper.Map<List<TreasuryNameViewDTO>>(receivers);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == uniqueType
                && obj.StockCode == stockID && obj.BranchId == branchID
                && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;

            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == userCode && obj.BranchId == branchID);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMaster> AddReqDisburseAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "S";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Pay = "Y";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.Flag3 = "1";

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateReqDisburseAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "S";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Pay = "Y";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.Flag3 = "1";

            TransMaster permissionForm = await GetByIdAsync(id);
            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Return Permission Form => اذن ارتجاع لمورد

        public async Task<TransMasterEditAddDTO> GetReturnPermissionByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            permissionFormDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);

            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
                obj.BranchId == permissionFormDTO.BranchId);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMasterEditAddDTO> GetNewReturnPermissionAsync(int stockID, int userCode,
            int uniqueType, int branchID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            permissionFormDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == uniqueType
                && obj.StockCode == stockID && obj.BranchId == branchID
                && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;

            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == userCode && obj.BranchId == branchID);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMaster> AddReturnPermissionAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.CustDisc5 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvNo = 0;
            permissionFormDTO.InvType = "0";
            permissionFormDTO.ShowRow = 3;

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateReturnPermissionAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.CustDisc5 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvNo = 0;
            permissionFormDTO.InvType = "0";
            permissionFormDTO.ShowRow = 3;

            TransMaster permissionForm = await GetByIdAsync(id);
            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }
    }
}
