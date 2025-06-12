using AutoMapper;
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
    public class PatientController : Controller
    {
        private readonly IPatientRepo _patRepo;
        private readonly IMapper _mapper;
        public PatientController(IPatientRepo patRepo, IMapper mapper)
        {
            _patRepo = patRepo;
            _mapper = mapper;
        }
        //Get all
        public async Task<IActionResult> Patients()
        {
            List<PatViewDTO> patients = await _patRepo.GetAllPatsAsync();
            return View(patients);
        }

        public async Task<IActionResult> GetPatients()
        {
            List<Pat> patients = await _patRepo.GetAllPatientsAsync();
            return Json(patients);
        }
        // Get Add
        public async Task<IActionResult> Add_Patient(string redirect, string controll)
        {
            ViewBag.redirect = redirect;
            ViewBag.controll = controll;
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Patient(string redirect,string controll, PatEditAddDTO patDTO)
        {
            if (ModelState.IsValid)
            {
                await _patRepo.AddPatientAsync(patDTO);
                return RedirectToAction(redirect, controll);
            }
            return View(patDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddBatch_Patient(string redirect, string controll, List<PatEditAddDTO> patDTOs)
        {
            if (ModelState.IsValid)
            {
                foreach (var patDTO in patDTOs)
                {
                    // Optional: add individual validation here
                    await _patRepo.AddPatientAsync(patDTO);
                }
                return RedirectToAction(redirect, controll);
            }

            // Optional: Return a view that can handle multiple entries if invalid
            return View(patDTOs);
        }
        //Get Edit
        public async Task<IActionResult> Edit_Patient(int id)
        {
            PatEditAddDTO patEditAddDTO = await _patRepo.GetPatientByIdAsync(id);
            ViewBag.Master = id;
            return View(patEditAddDTO);
        }
        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Patient(int id, PatEditAddDTO patientDTO)
        {
            if (ModelState.IsValid)
            {
                await _patRepo.EditPatientAsync(id, patientDTO);
                return RedirectToAction("Patients", "Patient");
            }
            return View(patientDTO);
        }

        //Delete ClinicTrans 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Patient(int id)
        {
            await _patRepo.DeletePatientAsync(id);
            return RedirectToAction("Patients", "Patient");
        }


        [HttpPost]
        public async Task<IActionResult> SaveRows([FromBody] List<PatEditAddDTO> insertedData)
        {
            try
            {

                // Validate the model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the raw body is null
                if (insertedData == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Request body is null or invalid JSON format."
                    });
                }

           
                await _patRepo.AddBatchPatientsAsync(insertedData);



                return StatusCode(200, new
                {
                    success = true,
                    message = "Data Added",
                    data = insertedData
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while saving the row data",
                    innerEx = ex.InnerException
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRows([FromBody] List<PatEditAddDTO> modifiedData)
        {
            try
            {

                // Validate the model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the raw body is null
                if (modifiedData == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Request body is null or invalid JSON format."
                    });
                }

                List<PatEditAddDTO> patAdmissionDTOs = new();

                await _patRepo.EditBatchPatientsAsync(modifiedData);



                return StatusCode(200, new
                {
                    success = true,
                    message = "Data Updated",
                    data = modifiedData
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while saving the row data",
                    innerEx = ex.InnerException
                });
            }
        }

    }
}
