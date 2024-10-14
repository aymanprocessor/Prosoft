using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.UI.Global;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Area("Medical")]
    [Authorize]
    public class DoctorApologyController : Controller
    {
        private readonly IDoctorRepo _doctorRepo;

        public DoctorApologyController(IDoctorRepo doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Doctors = (await _doctorRepo.GetAllDoctor()).ToSelectListItem<DoctorViewDTO>(d => d.DrDesc, d => d.DrId.ToString());

            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Index(DoctorApologyRequestDTO model)
        {
            ViewBag.Doctors = (await _doctorRepo.GetAllDoctor()).ToSelectListItem<DoctorViewDTO>(d => d.DrDesc, d => d.DrId.ToString());

            if (!ModelState.IsValid) {
                return View(model); }
            return View();
        }

        public async Task<IActionResult> GetPatients(int id)
        {

        }
    }
}
