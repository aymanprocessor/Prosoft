using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class AccTransMasterController : Controller
    {
        private readonly IAccTransMasterRepo _accTransMasterRepo;
        public AccTransMasterController(IAccTransMasterRepo accTransMasterRepo, IMapper mapper)
        {
            _accTransMasterRepo = accTransMasterRepo;
        }

        public async Task<IActionResult> Index(int journalCode)
        {
            List<AccTransMasterViewDTO> accTransMasterViewDTOs = await _accTransMasterRepo.GetAccTransMasterAsync(journalCode);
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

            ViewBag.journalCode = journalCode;
            ViewBag.fYear = fYear;
            ViewBag.branchId = branchId;
            return View(accTransMasterViewDTOs);
        }


        //Get add
        public async Task<IActionResult> Add_AccTransMaster(int journalCode, int fYear ,int branchId)
        {
            ViewBag.TransNo = await _accTransMasterRepo.GetNewtranNoAsync(journalCode, fYear, branchId);
            AccTransMasterEditAddDTO accTransMasterDTO = await _accTransMasterRepo.GetEmptyAccTransMasterAsync(journalCode);
            ViewBag.branchId = branchId;
            ViewBag.journalCode = journalCode;
            ViewBag.frear = fYear;
            ViewBag.userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

            return View(accTransMasterDTO);
        }

        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_AccTransMaster(AccTransMasterEditAddDTO accTransMasterDTO)
        {
            if (ModelState.IsValid)
            {
                await _accTransMasterRepo.AddAccTransMasterAsync(accTransMasterDTO);
                return RedirectToAction("Index", "AccTransMaster", new { journalCode = accTransMasterDTO.TransType});

            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_AccTransMaster(int id)
        {
            AccTransMasterEditAddDTO accTransMasterDTO = await _accTransMasterRepo.GetAccTransMasterByIdAsync(id);
            ViewBag.userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

            return View(accTransMasterDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_AccTransMaster(int id, AccTransMasterEditAddDTO accTransMasterDTO)
        {
            if (ModelState.IsValid)
            {
                await _accTransMasterRepo.EditAccTransMasterAsync(id, accTransMasterDTO);
                return RedirectToAction("Index", "AccTransMaster", new { journalCode = accTransMasterDTO.TransType });
            }
            return View(accTransMasterDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_AccTransMaster(int id)
        {
            AccTransMaster accTransMaster = await _accTransMasterRepo.GetByIdAsync(id);

            await _accTransMasterRepo.DeleteAsync(accTransMaster);
            await _accTransMasterRepo.SaveChangesAsync();
            return RedirectToAction("Index", "AccTransMaster", new { journalCode = accTransMaster.TransType });
        }
    }
}
