using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class DoctorSpecialtiesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDoctorRepo _doctorRepo;
        public DoctorSpecialtiesController(IMapper mapper, IDoctorRepo doctorRepo)
        {
            _mapper = mapper;
            _doctorRepo = doctorRepo;
        }
        public async Task<IActionResult> GetDocSubDtl()
        {
            return View();
        }
    }
}
