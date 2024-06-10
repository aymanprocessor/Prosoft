using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class AccTransDetailController : Controller
    {
        private readonly IAccTransDetailRepo _accTransDetailRepo;
        public AccTransDetailController(IAccTransDetailRepo accTransDetailRepo, IMapper mapper)
        {
            _accTransDetailRepo = accTransDetailRepo;
        }
        public async Task<IActionResult> GetAccTransDetail(int id,int journalCode)
        {
            List<AccTransDetailViewDTO> accTransDetailViewDTOs = await _accTransDetailRepo.GetAccTransDetailAsync(id,journalCode);
            return Json(accTransDetailViewDTOs);
        }
        //Get add
        public async Task<IActionResult> Add_AccTransDetail(int id, int journalCode)
        {
             AccTransDetailEditAddDTO accTransDetailDTO = await _accTransDetailRepo.GetEmptyAccTransDetailAsync(id,journalCode);
            ViewBag.userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            ViewBag.valDepSum = await _accTransDetailRepo.GetValDep(id);
            ViewBag.valCreditSum = await _accTransDetailRepo.GetValCredit(id);

            return View(accTransDetailDTO);
        }

        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_AccTransDetail(AccTransDetailEditAddDTO accTransDetailDTO)
        {
            if (ModelState.IsValid)
            {
                 await _accTransDetailRepo.AddAccTransDetailAsync(accTransDetailDTO);
                return RedirectToAction("Add_AccTransDetail", "AccTransDetail", new {id = accTransDetailDTO.TransId, journalCode = accTransDetailDTO.TransType });

            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_AccTransDetail(int id)
        {
            AccTransDetailEditAddDTO accTransDetailDTO = await _accTransDetailRepo.GetAccTransDetailByIdAsync(id);
            ViewBag.userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

            return View(accTransDetailDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_AccTransDetail(int id, AccTransDetailEditAddDTO accTransDetailDTO)
        {
            if (ModelState.IsValid)
            {
                await _accTransDetailRepo.EditAccTransDetailAsync(id, accTransDetailDTO);
                return RedirectToAction("Index", "AccTransMaster", new { journalCode = accTransDetailDTO.TransType });
            }
            return View(accTransDetailDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_AccTransDetail(int id)
        {
            AccTransDetail accTransDetail = await _accTransDetailRepo.GetByIdAsync(id);

            await _accTransDetailRepo.DeleteAsync(accTransDetail);
            await _accTransDetailRepo.SaveChangesAsync();
            return RedirectToAction("Index", "AccTransMaster", new { journalCode = accTransDetail.TransType });
        }

        public async Task<IActionResult> DeletedAllDetail(int id,int journalCode)
        {
            await _accTransDetailRepo.DeletedAllDetailAsync(id);
            return RedirectToAction("Index", "AccTransMaster", new { journalCode = journalCode });
        }
    }
}
