using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class CompanyGroupController : Controller
    {
        private readonly ICompanyGroupRepo _companyGroupRepo;
        private readonly IMapper _mapper;

        public CompanyGroupController(ICompanyGroupRepo companyGroupRepo, IMapper mapper)
        {
            _companyGroupRepo = companyGroupRepo;
            _mapper = mapper;

        }
        public async Task<IActionResult> Index()
        {
            List<CompanyGroup> companyGroups = await _companyGroupRepo.GetAllAsync();
            List<CompanyGroupDTO> companyGroupsDTO = _mapper.Map<List<CompanyGroupDTO>>(companyGroups);
            return View(companyGroupsDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_CompanyGroup()
        {
            ViewBag.GroupId = await _companyGroupRepo.GetNewIdAsync();
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_CompanyGroup(CompanyGroupDTO companyGroupDTO)
        {
            if (ModelState.IsValid)
            {
                CompanyGroup companyGroup = _mapper.Map<CompanyGroup>(companyGroupDTO);

                await _companyGroupRepo.AddAsync(companyGroup);
                await _companyGroupRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyGroupDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_CompanyGroup(int id)
        {
            CompanyGroup companyGroup = await _companyGroupRepo.GetByIdAsync(id);
            CompanyGroupDTO companyGroupDTO = _mapper.Map<CompanyGroupDTO>(companyGroup);
            return View(companyGroupDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_CompanyGroup(int id, CompanyGroupDTO companyGroupDTO)
        {
            if (ModelState.IsValid)
            {
                CompanyGroup companyGroup = await _companyGroupRepo.GetByIdAsync(id);
                _mapper.Map(companyGroupDTO, companyGroup);

                await _companyGroupRepo.UpdateAsync(companyGroup);
                await _companyGroupRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyGroupDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_CompanyGroup(int id)
        {
            CompanyGroup companyGroup = await _companyGroupRepo.GetByIdAsync(id);

            await _companyGroupRepo.DeleteAsync(companyGroup);
            await _companyGroupRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
