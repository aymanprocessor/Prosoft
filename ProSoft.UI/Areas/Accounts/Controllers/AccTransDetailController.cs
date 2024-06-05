using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

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
            //ViewBag.TransNo = await _accTransMasterRepo.GetNewtranNoAsync(journalCode, fYear, branchId);
             AccTransDetailEditAddDTO accTransDetailDTO = await _accTransDetailRepo.GetEmptyAccTransDetailAsync(id,journalCode);
            //ViewBag.branchId = branchId;
            //ViewBag.journalCode = journalCode;
            //ViewBag.frear = fYear;
            //ViewBag.userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

            return View(accTransDetailDTO);
        }

        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_AccTransDetail(AccTransMasterEditAddDTO accTransMasterDTO)
        {
            if (ModelState.IsValid)
            {
              //  await _accTransMasterRepo.AddAccTransMasterAsync(accTransMasterDTO);
                return RedirectToAction("Index", "AccTransMaster", new { journalCode = accTransMasterDTO.TransType });

            }
            return View();
        }
    }
}
