using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Shared;

namespace ProSoft.UI.Areas.Shared.Controllers
{
    [Authorize]
    [Area("Shared")]
    public class SectionController : Controller
    {
        private readonly ISectionRepo _sectionRepo;
        private readonly IMapper _mapper;
        public SectionController(ISectionRepo sectionRepo, IMapper mapper)
        {
            _sectionRepo = sectionRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<Sections2> sections = await _sectionRepo.GetAllAsync();
            List<SectionViewDTO> sectionsDTO = _mapper.Map<List<SectionViewDTO>>(sections);
            return View(sectionsDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_Section()
        {
            ViewBag.sectionId = await _sectionRepo.GetNewIdAsync();
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Section(SectionEditAddDTO sectionDTO)
        {
            if (ModelState.IsValid)
            {
                Sections2 section = _mapper.Map<Sections2>(sectionDTO);
                int branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                section.BranchId = branchId;

                await _sectionRepo.AddAsync(section);
                await _sectionRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sectionDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_Section(int id)
        {
            Sections2 section = await _sectionRepo.GetByIdAsync(id);
            SectionEditAddDTO sectionDTO = _mapper.Map<SectionEditAddDTO>(section);
            return View(sectionDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Section(int id, SectionEditAddDTO sectionDTO)
        {
            if (ModelState.IsValid)
            {
                Sections2 section = await _sectionRepo.GetByIdAsync(id);
                int branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                sectionDTO.BranchId = branchId;
                _mapper.Map(sectionDTO, section);

                await _sectionRepo.UpdateAsync(section);
                await _sectionRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sectionDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Section(int id)
        {
            Sections2 section = await _sectionRepo.GetByIdAsync(id);

            await _sectionRepo.DeleteAsync(section);
            await _sectionRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
