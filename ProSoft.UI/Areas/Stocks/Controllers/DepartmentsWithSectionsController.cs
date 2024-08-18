using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class DepartmentsWithSectionsController : Controller
    {
        private readonly IDepartmentsWithSectionsRepo _departmentsWithSectionsRepo;
        private readonly IClassificationCustRepo _classificationCustRepo;
        private readonly IMapper _mapper;
        public DepartmentsWithSectionsController(IDepartmentsWithSectionsRepo departmentsWithSectionsRepo, IClassificationCustRepo classificationCustRepo, IMapper mapper)
        {
            _departmentsWithSectionsRepo = departmentsWithSectionsRepo;
            _classificationCustRepo = classificationCustRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int? redirect)
        {
            List<ClassificationCust> classificationView = await _classificationCustRepo.GetAllAsync();
            List<ClassificationViewDTO> classificationViewDTOs = _mapper.Map<List<ClassificationViewDTO>>(classificationView);
            ViewBag.redirect = redirect;
            return View(classificationViewDTOs);
        }
        public async Task<IActionResult> GetDepartmentsWithSections(int id)
        {
            List<RegionsViewDTO> regionsViewDTOs = await _departmentsWithSectionsRepo.GetDepartmentsWithSectionsAll(id);
            return Json(regionsViewDTOs);
        }
        // Get Add
        public async Task<IActionResult> Add_DepartmentsWithSections(int id)
        {
            ViewBag.userName = (await _classificationCustRepo.GetByIdAsync(id)).ClassificationDesc;
            RegionsEditAddDTO regionsEditAddDTO = await _departmentsWithSectionsRepo.GetEmptyDepartmentsWithSections(id);
            return View(regionsEditAddDTO);
        }

        //// Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_DepartmentsWithSections(int id, RegionsEditAddDTO regionsEditAddDTO)
        {
            if (ModelState.IsValid)
            {
                await _departmentsWithSectionsRepo.AddDepartmentsWithSectionsAsync(id,regionsEditAddDTO);
                return RedirectToAction(nameof(Index), new { redirect = id });
            }
            return View(regionsEditAddDTO);
        }
        // Get Edit
        public async Task<IActionResult> Edit_DepartmentsWithSections(int id)
        {
            RegionsEditAddDTO regionsEditAddDTO = await _departmentsWithSectionsRepo.GetdepartmentsWithSectionsByIdAsync(id);

            return View(regionsEditAddDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_DepartmentsWithSections(int id, RegionsEditAddDTO regionsEditAddDTO)
        {
            if (ModelState.IsValid)
            {
                // userCashNoDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

                await _departmentsWithSectionsRepo.EditDepartmentsWithSectionsAsync(id, regionsEditAddDTO);
                return RedirectToAction(nameof(Index), new { redirect = regionsEditAddDTO.ClassificationCust });
            }
            return View(regionsEditAddDTO);
        }
        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_DepartmentsWithSections(int id)
        {
            Region region = await _departmentsWithSectionsRepo.GetByIdAsync(id);

            await _departmentsWithSectionsRepo.DeleteAsync(region);
            await _departmentsWithSectionsRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
