using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.UI.Global;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProSoft.UI.Areas.Medical.Controllers
{  
    [Authorize]
    [Area("Medical")]
  
    public class DoctorApologyController : Controller
    {
        private readonly IDoctorRepo _doctorRepo;
        private readonly IPatientRepo _patientRepo;
       
        public DoctorApologyController(IDoctorRepo doctorRepo, IPatientRepo patientRepo)
        {
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Doctors = (await _doctorRepo.GetAllDoctor()).ToSelectListItem<DoctorViewDTO>(d => d.DrDesc, d => d.DrId.ToString());

            return View();
        }
        //[HttpPost]
        //public async  Task<IActionResult> Index(int id, DateTime date,DoctorApologyRequestDTO model)
        //{
        //    ViewBag.Doctors = (await _doctorRepo.GetAllDoctor()).ToSelectListItem<DoctorViewDTO>(d => d.DrDesc, d => d.DrId.ToString());

        //    if (!ModelState.IsValid) {
        //        return View(model);
        //    }

        //    var patients = await _patientRepo.GetPatientsByDoctorIdAndDateAsync(id, date);
        //    var result = new
        //    {
        //        draw = 1,
        //        recordsTotal = patients.Count(),       // Total number of records
        //        recordsFiltered = patients.Count(),    // Total filtered records (for paging)
        //        data = patients                        // Actual data to display
        //    };
        //    model.data = result;

        //    return View(model);

        //}

        [HttpPost]
        public async Task<IActionResult> GetPatients(int id,string date)
        {
            var patients = await _patientRepo.GetPatientsByDoctorIdAndDateAsync(id,DateTime.Parse( date));
            var result = new
            {
                draw = 1,
                recordsTotal = patients.Count(),       // Total number of records
                recordsFiltered = patients.Count(),    // Total filtered records (for paging)
                data = patients                        // Actual data to display
            };

            return Json(result);
        }
    }
}
