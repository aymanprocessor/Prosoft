using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class ClinicTransController : Controller
    {
        private readonly IClinicTransRepo _clinicTransRepo;
        public ClinicTransController(IClinicTransRepo clinicTransRepo)
        {
            _clinicTransRepo = clinicTransRepo;
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
            ClinicTransEditAddDTO clinicTransDTO = await _clinicTransRepo.GetPricesDetails(id, clincID, sClincID, servID);
            return Json(clinicTransDTO);
        }
        //Get Add ClinicTrans
        public async Task<IActionResult> Add_ClinicTrans(int id ,string redirect, int flag)
        {
            ClinicTransEditAddDTO clinicTransEditAddDTO = await _clinicTransRepo.GetEmptyClinicTransAsync();
            ViewBag.Master = id;
            ViewBag.flag = flag;
            //for redirction
            ViewBag.redirect=redirect;

            return View(clinicTransEditAddDTO);
        }

        // Post Add ClinicTrans
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_ClinicTrans(int id, string redirect, int flag, ClinicTransEditAddDTO clinicTranDTO)
        {
            if (ModelState.IsValid) 
            {
                await _clinicTransRepo.AddClinicTransAsync(id, flag, clinicTranDTO);
                return RedirectToAction(redirect, "HospitalPatData");
            }
            return View(clinicTranDTO);
        }

        //Get Edit ClinicTrans
        public async Task<IActionResult> Edit_ClinicTrans(int id, string redirect)
        {
            ClinicTransEditAddDTO clinicTransEditAddDTO = await _clinicTransRepo.GetClinicTransByIdAsync(id);
            ViewBag.Master = id;
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
