using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.MedicalRecords.Controllers
{
    [Authorize]
    [Area(nameof(MedicalRecords))]
    public class PatRecordController : Controller
    {
        private readonly IPatientRepo _patientRepo;
        private readonly IMapper _mapper;

        public PatRecordController(IPatientRepo patientRepo, IMapper mapper)
        {
            _patientRepo = patientRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int id)
        {
            Pat patient = await _patientRepo.GetByIdAsync(id);
            var patientDTO = _mapper.Map<PatViewDTO>(patient);
            return View(patientDTO);
        }

        public async Task<IActionResult> AllPatients()
        {
            List<PatViewDTO> patients = await _patientRepo.GetAllPatsAsync();
            return View(patients);
        }
    }
}
