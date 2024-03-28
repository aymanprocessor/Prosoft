using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.Analysis;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Medical.Analysis;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Medical.Analysis;
using ProSoft.EF.Models.Shared;

namespace ProSoft.UI.Areas.Shared.Controllers
{
    [Authorize]
    [Area("Shared")]
    public class NationalityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INationalityRepo _nationalityRepo;

        public NationalityController(IMapper mapper, INationalityRepo nationalityRepo)
        {
            _mapper = mapper;
            _nationalityRepo = nationalityRepo;
        }
        public IActionResult index()
        {
            ViewBag.ahmed = "ahmed alaa";
            return View();
        }
        public async Task<IActionResult> Nationalities()
        {
           List<NationalityEi> nationalities =await _nationalityRepo.GetAllAsync();
            List<NationalityDTO> nationalitiesDTO = _mapper.Map<List<NationalityDTO>>(nationalities);
            return View(nationalitiesDTO);
        }
        // Get Add
        public async Task<IActionResult> Add_Nationality()
        {
            ViewBag.NationalID = await _nationalityRepo.GetNewIdAsync();

            return View();
        }
        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Nationality(NationalityDTO nationalityDTO)
        {
            if (ModelState.IsValid) 
            {
                NationalityEi nationality = _mapper.Map<NationalityEi>(nationalityDTO);
                await _nationalityRepo.AddAsync(nationality);
                await _nationalityRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Nationalities));
            }
            return View(nationalityDTO);
        }
        // Get Edit
        public async Task<IActionResult> Edit_Nationality(int id)
        {
            NationalityEi nationality = await _nationalityRepo.GetByIdAsync(id);
            NationalityDTO nationalityDTO = _mapper.Map<NationalityDTO>(nationality);

            return View(nationalityDTO);
        }
        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Nationality(int id,NationalityDTO nationalityDTO)
        {
            if (ModelState.IsValid)
            {
                NationalityEi nationality = await _nationalityRepo.GetByIdAsync(id);
                _mapper.Map(nationalityDTO, nationality);

                await _nationalityRepo.UpdateAsync(nationality);
                await _nationalityRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Nationalities));
            }
            return View(nationalityDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Nationality(int id)
        {
            NationalityEi nationality = await _nationalityRepo.GetByIdAsync(id);

            await _nationalityRepo.DeleteAsync(nationality);
            await _nationalityRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Nationalities));
        }
    }
}
