using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class PermissionFormController : Controller
    {
        private readonly ITransMasterRepo _transMasterRepo;
        private readonly IUserRepo _userRepo;
        private readonly ITransDtlRepo _transDtlRepo;

        public PermissionFormController(ITransMasterRepo transMasterRepo,
            IUserRepo userRepo, ITransDtlRepo transDtlRepo)
        {
            _transMasterRepo = transMasterRepo;
            _userRepo = userRepo;
            _transDtlRepo = transDtlRepo;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<StockViewDTO> stocksDTO = await _transMasterRepo.GetActiveStocksForUserAsync(userCode);
            ViewBag.Stocks = stocksDTO;

            TransMaster permissionForm = await _transMasterRepo.GetByIdAsync(Convert.ToInt32(id));
            TransMasterViewDTO permissionsFormDTO = await _transMasterRepo.GetForViewAsync(permissionForm);
            if(permissionForm != null)
            {
                ViewBag.trans_Type = permissionForm.TransType;
                ViewBag.stockCode = permissionForm.StockCode;
            }
            ViewBag.docNo = permissionsFormDTO?.DocNo;
            return View(permissionsFormDTO);
        }
        //  كل الاذون معتمد وغير معتمد ولالغاء الاعتماد
        ////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> AllRermissionForm(int? id)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<StockViewDTO> stocksDTO = await _transMasterRepo.GetActiveStocksForUserAsync(userCode);
            ViewBag.Stocks = stocksDTO;
            ViewBag.SupCodes = await _transMasterRepo.GetAllSuPCodesAsync();
            TransMaster permissionForm = await _transMasterRepo.GetByIdAsync(Convert.ToInt32(id));
            TransMasterViewDTO permissionsFormDTO = await _transMasterRepo.GetForViewAsync(permissionForm);
            if (permissionForm != null)
            {
                ViewBag.trans_Type = permissionForm.TransType;
                ViewBag.stockCode = permissionForm.StockCode;
            }
            ViewBag.docNo = permissionsFormDTO?.DocNo;
            return View(permissionsFormDTO);
        }
        public async Task<IActionResult> GetAllPermissionsForms(int id, int transType, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod, string supCode)
        {
            List<TransMasterViewDTO> permissionsFormsDTO = await _transMasterRepo
                .GetAllPermissionsFormsAsync(id, transType, fromReceipt, toReceipt, fromPeriod, toPeriod, supCode);
            return Json(permissionsFormsDTO);
        }
        public async Task<IActionResult> CancelApprovePermission(int id)
        {
            int result = await _transMasterRepo.CancelApprovePermissionAsync(id);
            return Json(result);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> GetUserPermissionsForStock(int id)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<PermissionDefViewDTO> permissionsDTO = await _transMasterRepo
                .GetUserPermissionsForStockAsync(userCode, id);
            return Json(permissionsDTO);
        }
        public async Task<IActionResult> GetPermissionsForms(int id, int transType)
        {
            List<TransMasterViewDTO> permissionsFormsDTO = await _transMasterRepo
                .GetPermissionsFormsAsync(id, transType);
            return Json(permissionsFormsDTO);
        }

        public async Task<IActionResult> ApprovePermission(int id)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            bool ifHasDetails = await _transMasterRepo.CheckForDetailsAsync(id);
            int result;
            try
            {
                if (ifHasDetails)
                {
                    // result
                    // 1 => لم يتم الترحيل للحسابات
                    // 2 => من فضلك ادخل تاريخ فاتورة المورد
                    // 3 => تاكد من قيمة الاذن واعد المحاولة
                    // 4 => خطأ لم يتم الترحيل للحسابات , قم باختيار المورد وأعد المحاولة
                    // 5 => خطأ لم يتم الترحيل للحسابات , قم باختيار العميل وأعد المحاولة
                    // 6 => لم يتم الترحيل للحسابات لعدم وجود يومية للمستخدم
                    // 7 => غير مسموح بالترحيل لاقفال القيد بالحسابات
                    // 8 => برجاء تحديد مسلسل الحركة شهرى أم سنوى أولا ثم أعد المحاولة
                    // 9 => خطأ فى الترحيل للحسابات , هذا المورد غير مرتبط بالحسابات
                    // 10 => خطأ فى الترحيل للحسابات , هذا العميل غير مرتبط بالحسابات
                    // 11 => تم الترحيل للحسابات
                    result = await _transMasterRepo.ApprovePermissionAsync(id, userCode);
                    return Json(result);
                }
            }
            catch (Exception err)
            {
                return Json(err.Message);
            }

            return Json(ifHasDetails);
        }

        //////////////////////////////////////////////////
        // Permission Form => اذن اضافة
        // Get Add
        public async Task<IActionResult> Add_PermissionForm(int id, int transType)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetNewTransMasterAsync(id, userCode, transType);
            permissionFormDTO.UserName = (await _userRepo.GetUserByIdAsync(userCode)).UserName;

            return View(permissionFormDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_PermissionForm(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                TransMaster permissionForm = await _transMasterRepo.AddPermissionFormAsync(permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id = permissionForm.TransMAsterID });
            }
            return View(permissionFormDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_PermissionForm(int id)
        {
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetPermissionFormByIdAsync(id);
            ViewBag.transMasterID = id;
            return View(permissionFormDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_PermissionForm(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                await _transMasterRepo.UpdateTransMasterAsync(id, permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(permissionFormDTO);
        }
        
        public async Task<IActionResult> IfPossibleToDelete(int id)
        {
            // flag == 1 => لا يجوز الحذف بسبب الترحيل للحسابات
            // flag == 2 => لا يجوز الحذف بسبب الاعتماد
            // flag == 3 => يتم الحذف
            int flag = await _transMasterRepo.IfPossibleToDeleteAsync(id);
            return Json(flag);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_PermissionForm(int id)
        {
            var deleted = await _transMasterRepo.GetByIdAsync(id);
            var lastRecord = await _transMasterRepo.GetTransMasterByAsync((int)deleted.TransType, (int)deleted.StockCode,id);
            await _transMasterRepo.DeletePermissionFormAsync(id);
            return RedirectToAction(nameof(Index), new { id = lastRecord.TransMAsterID });
        }

        //////////////////////////////////////////////////
        // Disburse or Convert Permission => اذن صرف او تحويل
        // Get Add
        public async Task<IActionResult> Add_DisburseOrConvertForm(int id, int transType)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetNewDisburseOrConvertFormAsync(id, userCode, transType, branchId);
            permissionFormDTO.UserName = (await _userRepo.GetUserByIdAsync(userCode)).UserName;

            return View(permissionFormDTO);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_DisburseOrConvertForm(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                TransMaster permissionForm = await _transMasterRepo.AddDisburseOrConvertFormAsync(permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id = permissionForm.TransMAsterID });
            }
            return View(permissionFormDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_DisburseOrConvertForm(int id)
        {
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetDisburseOrConvertFormByIdAsync(id);
            ViewBag.transMasterID = id;
            return View(permissionFormDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_DisburseOrConvertForm(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                await _transMasterRepo.UpdateDisburseOrConvertFormAsync(id, permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(permissionFormDTO);
        }

        //////////////////////////////////////////////////
        // Stock Receive Permission => اذن استلام من مخزن
        // Get Add
        //public async Task<IActionResult> Add_StockReceivePermission(int id, int transType)
        //{
        //    var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
        //    var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        //    TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetNewDisburseOrConvertFormAsync(id, userCode, transType, branchId);
        //    permissionFormDTO.UserName = (await _userRepo.GetUserByIdAsync(userCode)).UserName;

        //    return View(permissionFormDTO);
        //}

        //Post Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add_StockReceivePermission(int id, TransMasterEditAddDTO permissionFormDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        //        permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
        //        TransMaster permissionForm = await _transMasterRepo.AddDisburseOrConvertFormAsync(permissionFormDTO);
        //        return RedirectToAction(nameof(Index), new { id = permissionForm.TransMAsterID });
        //    }
        //    return View(permissionFormDTO);
        //}

        // Get Edit
        //public async Task<IActionResult> Edit_StockReceivePermission(int id)
        //{
        //    TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetDisburseOrConvertFormByIdAsync(id);
        //    return View(permissionFormDTO);
        //}

        // Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_StockReceivePermission(int id, TransMasterEditAddDTO permissionFormDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        //        permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
        //        await _transMasterRepo.UpdateStockReceiveFormAsync(id, permissionFormDTO);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(permissionFormDTO);
        //}

        //////////////////////////////////////////////////
        // Sales Invoice => فاتورة مبيعات
        // Get Add
        public async Task<IActionResult> Add_SalesInvoice(int id, int transType)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo
                .GetNewSalesInvoiceAsync(id, userCode, transType, branchId);
            permissionFormDTO.UserName = (await _userRepo.GetUserByIdAsync(userCode)).UserName;

            return View(permissionFormDTO);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_SalesInvoice(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                TransMaster permissionForm = await _transMasterRepo.AddSalesInvoiceAsync(permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id = permissionForm.TransMAsterID });
            }
            return View(permissionFormDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_SalesInvoice(int id)
        {
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetSalesInvoiceByIdAsync(id);
            ViewBag.transMasterID = id;
            return View(permissionFormDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_SalesInvoice(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                await _transMasterRepo.UpdateSalesInvoiceAsync(id, permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(permissionFormDTO);
        }

        //////////////////////////////////////////////////
        // Debit or Credit Settlement => تسوية مدينةاو دائنة
        // Get Add
        public async Task<IActionResult> Add_Settlement(int id, int transType)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo
                .GetNewSettlementAsync(id, userCode, transType, branchId);
            permissionFormDTO.UserName = (await _userRepo.GetUserByIdAsync(userCode)).UserName;

            return View(permissionFormDTO);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Settlement(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                TransMaster permissionForm = await _transMasterRepo.AddSettlementAsync(permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id = permissionForm.TransMAsterID });
            }
            return View(permissionFormDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_Settlement(int id)
        {
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetSettlementByIdAsync(id);
            ViewBag.transMasterID = id;
            return View(permissionFormDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Settlement(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                await _transMasterRepo.UpdateSettlementAsync(id, permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(permissionFormDTO);
        }

        //////////////////////////////////////////////////
        // Requirements Disburse Form => اذن صرف مستلزمات
        // Get Add
        public async Task<IActionResult> Add_ReqDisburse(int id, int transType)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo
                .GetNewReqDisburseAsync(id, userCode, transType, branchId);
            permissionFormDTO.UserName = (await _userRepo.GetUserByIdAsync(userCode)).UserName;

            return View(permissionFormDTO);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_ReqDisburse(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                TransMaster permissionForm = await _transMasterRepo.AddReqDisburseAsync(permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id = permissionForm.TransMAsterID });
            }
            return View(permissionFormDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_ReqDisburse(int id)
        {
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetReqDisburseByIdAsync(id);
            ViewBag.transMasterID = id;
            return View(permissionFormDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_ReqDisburse(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                await _transMasterRepo.UpdateReqDisburseAsync(id, permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(permissionFormDTO);
        }

        //////////////////////////////////////////////////
        // Return Permission Form => اذن ارتجاع لمورد
        // Get Add
        public async Task<IActionResult> Add_ReturnPermission(int id, int transType)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo
                .GetNewReturnPermissionAsync(id, userCode, transType, branchId);
            permissionFormDTO.UserName = (await _userRepo.GetUserByIdAsync(userCode)).UserName;

            return View(permissionFormDTO);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_ReturnPermission(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                TransMaster permissionForm = await _transMasterRepo.AddReturnPermissionAsync(permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id = permissionForm.TransMAsterID });
            }
            return View(permissionFormDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_ReturnPermission(int id)
        {
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetReturnPermissionByIdAsync(id);
            ViewBag.transMasterID = id;
            return View(permissionFormDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_ReturnPermission(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                await _transMasterRepo.UpdateReturnPermissionAsync(id, permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(permissionFormDTO);
        }
    }
}
