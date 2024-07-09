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
        //private readonly IUserTransRepo _userTransRepo;
        private readonly ITransMasterRepo _transMasterRepo;
        private readonly IUserRepo _userRepo;
        private readonly ITransDtlRepo _transDtlRepo;
        //private readonly IMapper _mapper;

        public PermissionFormController(/*IUserTransRepo userTransRepo,*/
            ITransMasterRepo transMasterRepo, IUserRepo userRepo, ITransDtlRepo transDtlRepo/*, IMapper mapper*/)
        {
            //_userTransRepo = userTransRepo;
            _transMasterRepo = transMasterRepo;
            _userRepo = userRepo;
            _transDtlRepo = transDtlRepo;
            //_mapper = mapper;
        }

        public async Task<IActionResult> Index(int? id/*, int? transDtlId*/)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<StockViewDTO> stocksDTO = await _transMasterRepo.GetActiveStocksForUserAsync(userCode);
            ViewBag.Stocks = stocksDTO;

            TransMaster permissionForm = await _transMasterRepo.GetByIdAsync(Convert.ToInt32(id));
            TransMasterViewDTO permissionsFormDTO = await _transMasterRepo.GetForViewAsync(permissionForm);
            ViewBag.docNo = permissionsFormDTO?.DocNo;
            return View(permissionsFormDTO);
        }

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
            bool ifHasDetails = await _transMasterRepo.CheckForDetailsAsync(id);
            if (ifHasDetails)
                await _transMasterRepo.ApprovePermissionAsync(id);

            return Json(ifHasDetails);
        }

        //////////////////////////////////////////////////
        // Permission Form
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
                return RedirectToAction(nameof(Index));
            }
            return View(permissionFormDTO);
        }
        
        public async Task<IActionResult> IfPossibleToDelete(int id)
        {
            // flag == 1 => لا يجوز الحذف بسبب الترحيل للحسابات
            // flag == 2 => لا يجوز الحذف بسبب عدم الاعتماد
            // flag == 3 => يتم الحذف
            int flag = await _transMasterRepo.IfPossibleToDeleteAsync(id);
            return Json(flag);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_PermissionForm(int id)
        {
            await _transMasterRepo.DeletePermissionFormAsync(id);
            return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
            }
            return View(permissionFormDTO);
        }
    }
}
