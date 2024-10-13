using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class PermissionFormDetailsController : Controller
    {
        private readonly ITransDtlRepo _transDtlRepo;
        private readonly ITransMasterRepo _transMasterRepo;
        private readonly IMapper _mapper;
        public PermissionFormDetailsController(ITransDtlRepo transDtlRepo, IMapper mapper, ITransMasterRepo transMasterRepo)
        {
            _transDtlRepo = transDtlRepo;
            _mapper = mapper;
            _transMasterRepo = transMasterRepo;
        }

        public async Task<IActionResult> GetPermissionDetails(int id)
        {
            TransMaster transMaster = await _transMasterRepo.GetByIdAsync(id);

            List<TransDtlWithPriceDTO> transDtlListDTO = await _transDtlRepo.GetPermissionDetailsAsync(id);

            return Json(new {data = transDtlListDTO,totalValue = transMaster.TotTransVal });

        }

        public async Task<IActionResult> GetItem(string itemBarcode)
        {
            var itemCode = await _transDtlRepo.GetItemAsync(itemBarcode);
            return Json(itemCode);
        }

        public async Task<IActionResult> GetOldBarCode(string itemCode)
        {
            var barCode = await _transDtlRepo.GetOldBarCodeAsync(itemCode);
            return Json(barCode);
        }

        public async Task<IActionResult> GetBarCode(int transMAsterID, int serial, string itemMaster)
        {
            var barCode = await _transDtlRepo.GetBarCodeAsync(transMAsterID, serial, itemMaster);
            return Json(barCode);
        }

        // For Showing Trans Price
        // Get Add
        public async Task<IActionResult> Add_TransDetailWithPrice(int id)
        {
            TransDtlWithPriceDTO transDtlDTO = await _transDtlRepo.GetNewTransDtlWithPriceAsync(id);
            return View(transDtlDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_TransDetailWithPrice(int id, TransDtlWithPriceDTO transDtlDTO)
        {
            if (transDtlDTO.ItemMaster == null && transDtlDTO.ShowItemMaster == null)
            {
                ModelState.AddModelError("", "The Item is required");
                TransDtlWithPriceDTO newTransDtlDTO = await _transDtlRepo.GetNewTransDtlWithPriceAsync(id);
                _mapper.Map(newTransDtlDTO, transDtlDTO);
            }
            else if (ModelState.IsValid)
            {
                transDtlDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                transDtlDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                transDtlDTO.TransMasterID = id;

                await _transDtlRepo.AddTransDtlWithPriceAsync(transDtlDTO);
                return RedirectToAction(nameof(Add_TransDetailWithPrice), new { id });
            }
            return View(transDtlDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_TransDetailWithPrice(int id)
        {
            TransDtlWithPriceDTO transDtlDTO = await _transDtlRepo.GetTransDtlWithPriceByIdAsync(id);
            ViewBag.transMasterID = transDtlDTO.TransMasterID;
            return View(transDtlDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_TransDetailWithPrice(int id, TransDtlWithPriceDTO transDtlDTO)
        {
            TransDtlWithPriceDTO newTransDtlDTO = await _transDtlRepo.GetTransDtlWithPriceByIdAsync(id);
            if (transDtlDTO.ItemMaster == null && transDtlDTO.ShowItemMaster == null)
            {
                ModelState.AddModelError("", "The Item is required");
                _mapper.Map(newTransDtlDTO, transDtlDTO);
            }
            else if (ModelState.IsValid)
            {
                transDtlDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                transDtlDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _transDtlRepo.UpdateTransDtlWithPriceAsync(id, transDtlDTO);
                return RedirectToAction("Index", "PermissionForm", new { id = newTransDtlDTO.TransMasterID });
            }
            return View(transDtlDTO);
        }

        ////////////////////////////////////////////////////////////////////////////

        // For Not Showing Trans Price
        // Get Add
        public async Task<IActionResult> Add_TransDetail(int id)
        {
            TransDtlDTO transDtlDTO = await _transDtlRepo.GetNewTransDtlAsync(id);
            return View(transDtlDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_TransDetail(int id, TransDtlDTO transDtlDTO)
        {
            if (transDtlDTO.ItemMaster == null && transDtlDTO.ShowItemMaster == null)
            {
                ModelState.AddModelError("", "The Item is required");
                TransDtlDTO newTransDtlDTO = await _transDtlRepo.GetNewTransDtlAsync(id);
                _mapper.Map(newTransDtlDTO, transDtlDTO);
            }
            else if (ModelState.IsValid)
            {
                transDtlDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                transDtlDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                transDtlDTO.TransMasterID = id;

                await _transDtlRepo.AddTransDtlAsync(transDtlDTO);
                return RedirectToAction(nameof(Add_TransDetail), new { id });
            }

            return View(transDtlDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_TransDetail(int id)
        {
            TransDtlDTO transDtlDTO = await _transDtlRepo.GetTransDtlByIdAsync(id);
            return View(transDtlDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_TransDetail(int id, TransDtlDTO transDtlDTO)
        {
            TransDtlDTO newTransDtlDTO = await _transDtlRepo.GetTransDtlByIdAsync(id);
            if (transDtlDTO.ItemMaster == null && transDtlDTO.ShowItemMaster == null)
            {
                ModelState.AddModelError("", "The Item is required");
                _mapper.Map(newTransDtlDTO, transDtlDTO);
            }
            else if (ModelState.IsValid)
            {
                transDtlDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                transDtlDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _transDtlRepo.UpdateTransDtlAsync(id, transDtlDTO);
                return RedirectToAction("Index", "PermissionForm", new { id = newTransDtlDTO.TransMasterID });
            }
            return View(transDtlDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_TransDetail(int id)
        {
            await _transDtlRepo.DeleteTransDtlAsync(id);
            return RedirectToAction("Index", "PermissionForm");
        }
    }
}
