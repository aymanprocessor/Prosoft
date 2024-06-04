using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class PermissionFormController : Controller
    {
        private readonly IUserTransRepo _userTransRepo;
        private readonly ITransMasterRepo _transMasterRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public PermissionFormController(IUserTransRepo userTransRepo,
            ITransMasterRepo transMasterRepo, IUserRepo userRepo, IMapper mapper)
        {
            _userTransRepo = userTransRepo;
            _transMasterRepo = transMasterRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<StockViewDTO> stocksDTO = await _transMasterRepo.GetActiveStocksForUserAsync(userCode);
            ViewBag.Stocks = stocksDTO;

            TransMaster permissionForm = await _transMasterRepo.GetByIdAsync(Convert.ToInt32(id));
            TransMasterViewDTO permissionsFormDTO = await _transMasterRepo.GetForViewAsync(permissionForm);
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

        // Add Permission Form
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

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_PermissionForm(int id)
        {
            TransMaster permissionForm = await _transMasterRepo.GetByIdAsync(id);

            await _transMasterRepo.DeleteAsync(permissionForm);
            await _transMasterRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //////////////////////////////////////////////////
        // Add Disburse Form => اذن صرف
        // Get Add
        public async Task<IActionResult> Add_DisburseForm(int id, int transType)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetNewDisburseFormAsync(id, userCode, transType, branchId);
            permissionFormDTO.UserName = (await _userRepo.GetUserByIdAsync(userCode)).UserName;

            return View(permissionFormDTO);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_DisburseForm(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                TransMaster permissionForm = await _transMasterRepo.AddDisburseFormAsync(permissionFormDTO);
                return RedirectToAction(nameof(Index), new { id = permissionForm.TransMAsterID });
            }
            return View(permissionFormDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_DisburseForm(int id)
        {
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetDisburseFormByIdAsync(id);
            return View(permissionFormDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_DisburseForm(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                await _transMasterRepo.UpdateDisburseFormAsync(id, permissionFormDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(permissionFormDTO);
        }

        // Delete
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete_DisburseForm(int id)
        //{
        //    TransMaster permissionForm = await _transMasterRepo.GetByIdAsync(id);

        //    await _transMasterRepo.DeleteAsync(permissionForm);
        //    await _transMasterRepo.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //////////////////////////////////////////////////
        // Add Sales Invoice => فاتورة مبيعات
        // Get Add
        public async Task<IActionResult> Add_SalesInvoice(int id, int transType)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo
                .GetNewDisburseFormAsync(id, userCode, transType, branchId);
            permissionFormDTO.UserName = (await _userRepo.GetUserByIdAsync(userCode)).UserName;

            return View(permissionFormDTO);
        }

        //Post Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add_SalesInvoice(int id, TransMasterEditAddDTO permissionFormDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        //        permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
        //        TransMaster permissionForm = await _transMasterRepo.AddDisburseFormAsync(permissionFormDTO);
        //        return RedirectToAction(nameof(Index), new { id = permissionForm.TransMAsterID });
        //    }
        //    return View(permissionFormDTO);
        //}

        // Get Edit
        //public async Task<IActionResult> Edit_SalesInvoice(int id)
        //{
        //    TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetDisburseFormByIdAsync(id);
        //    return View(permissionFormDTO);
        //}

        // Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_SalesInvoice(int id, TransMasterEditAddDTO permissionFormDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _transMasterRepo.UpdateDisburseFormAsync(id, permissionFormDTO);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(permissionFormDTO);
        //}

        // Delete
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete_SalesInvoice(int id)
        //{
        //    TransMaster permissionForm = await _transMasterRepo.GetByIdAsync(id);

        //    await _transMasterRepo.DeleteAsync(permissionForm);
        //    await _transMasterRepo.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
