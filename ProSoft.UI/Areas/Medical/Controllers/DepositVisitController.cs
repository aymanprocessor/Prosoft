using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class DepositVisitController : Controller
    {
        private readonly IDepositVisitRepo _depositRepo;
        private readonly IMapper _mapper;
        public DepositVisitController(IDepositVisitRepo depositRepo , IMapper mapper)
        {
            _depositRepo = depositRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int id)
        {
            List<DepositViewDTO> depositDTOs = await _depositRepo.GetAllDepositesAsync(id);
            PatAdmissionViewDTO patAdmissionDTO = await _depositRepo.GetPatData(id);
            ViewBag.VisitID = patAdmissionDTO.MasterId;
            ViewBag.PatName = patAdmissionDTO.PatName;
            return View(depositDTOs);
        }

        //Get add
        public async Task<IActionResult> Add_Deposit(int id)
        {
            ViewBag.DepositId = await _depositRepo.GetNewIdAsync();
            ViewBag.MasterId = id;
            DepositEditAddDTO DepositEditAddDTO = await _depositRepo.GetEmptyDepositAsync(id);
            return View(DepositEditAddDTO);
        }

        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Deposit(int id, DepositEditAddDTO depositDTO)
        {
            if (ModelState.IsValid)
            {
                depositDTO.FYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
                depositDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                depositDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

                await _depositRepo.AddDepositDtllAsync(id, depositDTO);
                return RedirectToAction("Index", "DepositVisit" , new {id});
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_Deposit(int id)
        {
            DepositEditAddDTO depositDTO = await _depositRepo.GetDepositByIdAsync(id);
            return View(depositDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Deposit(int id, DepositEditAddDTO depositDTO)
        {
            if (ModelState.IsValid)
            {
                depositDTO.UserModify = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _depositRepo.EditDepositAsync(id, depositDTO);
                return RedirectToAction("Index", "DepositVisit", new { id =depositDTO.MasterId });
            }
            return View(depositDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Deposit(int id,int visitID)
        {
            Deposit deposit = await _depositRepo.GetByIdAsync(id);

            await _depositRepo.DeleteAsync(deposit);
            await _depositRepo.SaveChangesAsync();
            return RedirectToAction("Index", "DepositVisit", new { id = visitID});
        }
    }
}
