using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class ClinicTransController : Controller
    {
        private readonly IClinicTransRepo _clinicTransRepo;
        private readonly IPatAdmissionRepo _patAdmissionRepo;
        private readonly ICurrentUserService _currentUserService;
        public ClinicTransController(IClinicTransRepo clinicTransRepo, IPatAdmissionRepo patAdmissionRepo, ICurrentUserService currentUserService)
        {
            _clinicTransRepo = clinicTransRepo;
            _patAdmissionRepo = patAdmissionRepo;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> GetClinicTrans(int id, int flag)
        {
            List<ClinicTransViewDTO> clinicTrans = await _clinicTransRepo
                .GetClinicTransByAdmissionAsync(id, flag);
            return Json(clinicTrans);
        }
        /// Get GetSubComp
        public async Task<IActionResult> GetSubClinic(int id)
        {
            List<SubClinicViewDTO> subClinics = await _clinicTransRepo.GetSubClinic(id);
            return Json(subClinics);
        }

        /// Get GetSubItem
        public async Task<IActionResult> GetSubItem(int id)
        {
            List<SubItemViewDTO> subitems = await _clinicTransRepo.GetSubItem(id);
            return Json(subitems);
        }

        /// Get GetServe
        public async Task<IActionResult> GetServeClinic(int id)
        {
            List<ServiceClinicViewDTO> serveCinics = await _clinicTransRepo.GetServeClinic(id);
            return Json(serveCinics);
        }

        ///Get  GetPriceListDetails
        public async Task<IActionResult> GetPriceListDetails(int id, int clincID, int sClincID, int servID)
        {
            TermsPriceListViewDTO TermsPriceListDTO = await _clinicTransRepo.GetPricesDetails(id, clincID, sClincID, servID);
            return Json(TermsPriceListDTO);
        }

        ///Get  Get Doctor Data
        public async Task<IActionResult> GetDoctorData(int id,int sClincID, int servID)
        {
            DoctorPrecentViewDTO doctorPrecentDTO = await _clinicTransRepo.GetDoctorPrices(id, sClincID, servID);
            //To Get Percentage of doctor in case ==0 in doctor Precentage
            //ServiceClinicViewDTO serviceClinicDTO = await _clinicTransRepo.GetServiceClinicByIDs(id, sClincID, servID);
            //ViewBag.DoctorPercentSC = serviceClinicDTO.DrPerc;
            return Json(doctorPrecentDTO);
        }

        //////////////////////////////////////////////////////////////////////////////////////////
     
        //Get Add ClinicTrans
        public async Task<IActionResult> Add_ClinicTrans(int id ,string redirect, int flag)
        {
            ClinicTransEditAddDTO clinicTransEditAddDTO = await _clinicTransRepo.GetEmptyClinicTransAsync();
       
            ViewBag.MainClinics = clinicTransEditAddDTO.MainClinics;
            ViewBag.MainItems = clinicTransEditAddDTO.MainItems;
            ViewBag.Doctors = clinicTransEditAddDTO.Doctors;
            ViewBag.Master = id;
            ViewBag.flag = flag;
            //Set Invoice number
            ViewBag.InvoiceNo = clinicTransEditAddDTO.ExInvoiceNo;
            //To Know private or Contract
            PatAdmissionEditAddDTO patAdmissionDTO = await _clinicTransRepo.GetPatAdmissionByIdAsync(id);
            ViewBag.privateOrContract = patAdmissionDTO.Deal;
            //All Price Of All Services Belong to Visit of Patient
            decimal totalPriceService = await _clinicTransRepo.GetPricesOfServices(id, flag);
            ViewBag.AllServicesPrice = totalPriceService;
            //for redirction
            ViewBag.redirect=redirect;

            return View(clinicTransEditAddDTO);
        }

        // Post Add ClinicTrans
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_ClinicTrans(int id, string redirect, int flag, ClinicTransEditAddDTO clinicTranDTO)
        {
            ClinicTransEditAddDTO clinicTransEditAddDTO = await _clinicTransRepo.GetEmptyClinicTransAsync();
            ViewBag.MainClinics = clinicTransEditAddDTO.MainClinics;
            ViewBag.MainItems = clinicTransEditAddDTO.MainItems;
            ViewBag.Doctors = clinicTransEditAddDTO.Doctors;
            if (ModelState.IsValid) 
            {
            clinicTranDTO.ExYear = _currentUserService.Year;
            clinicTranDTO.BranchId = _currentUserService.BranchId;
                
                await _clinicTransRepo.AddClinicTransAsync(id, flag, clinicTranDTO);
                // return RedirectToAction(redirect, "HospitalPatData");
                return RedirectToAction("Add_ClinicTrans", new { id , redirect , flag });
            }
            return View(clinicTranDTO);
        }

        //Get Edit ClinicTrans
        public async Task<IActionResult> Edit_ClinicTrans(int id, string redirect)
        {
            ClinicTransEditAddDTO clinicTransEditAddDTO = await _clinicTransRepo.GetClinicTransByIdAsync(id);
            ViewBag.Master = clinicTransEditAddDTO.MasterId;
            PatAdmissionEditAddDTO patAdmissionDTO = await _clinicTransRepo.GetPatAdmissionByIdAsync(id);
            ViewBag.privateOrContract = patAdmissionDTO.Deal;
            decimal totalPriceService = await _clinicTransRepo.GetPricesOfServices((int)clinicTransEditAddDTO.MasterId, (int)clinicTransEditAddDTO.Flag);
            ViewBag.AllServicesPrice = totalPriceService;
            //for redirction
            ViewBag.redirect = redirect;

            return View(clinicTransEditAddDTO);
        }

        // Post Edit ClinicTrans
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_ClinicTrans(int id, string redirect, ClinicTransEditAddDTO clinicTranDTO)
        {
        
            if (ModelState.IsValid)
            {
                clinicTranDTO.ExYear = _currentUserService.Year;
                clinicTranDTO.BranchId = _currentUserService.BranchId;
                await _clinicTransRepo.EditClinicTransAsync(id, clinicTranDTO);
                return RedirectToAction(redirect, "HospitalPatData");
            }
            return View(clinicTranDTO);
        }

        //Delete ClinicTrans 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_ClinicTrans(int id, string redirect)
        {
            await _clinicTransRepo.DeleteClinicTransAsync(id);
            return RedirectToAction(redirect, "HospitalPatData");
        }

    }
}
