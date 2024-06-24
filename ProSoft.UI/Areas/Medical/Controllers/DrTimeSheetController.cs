using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class DrTimeSheetController : Controller
    {
        private readonly IDrTimeSheetRepo _drTimeSheetRepo;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IMapper _mapper;

        public DrTimeSheetController(IDrTimeSheetRepo drTimeSheetRepo, IDoctorRepo doctorRepo, IMapper mapper)
        {
            _drTimeSheetRepo = drTimeSheetRepo;
            _doctorRepo = doctorRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<DoctorViewDTO> doctorDTOs = await _doctorRepo.GetAllDoctor();
            return View(doctorDTOs);
        }

        //Ajax In Index
        public async Task<IActionResult> GetAppointmentsForDr(int id)
        {
            List<DrTimeSheetDTO> drTimeSheetDTOs = await _drTimeSheetRepo
                .GetAppointmentsForDrAsync(id);
            return Json(drTimeSheetDTOs);
        }
    }
}
 