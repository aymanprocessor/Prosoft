using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Shared;

namespace ProSoft.UI.Areas.Shared.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Shared")]
    public class PermitDefinitionController : Controller
    {
        private readonly IGeneralTableRepo _permissionRepo;
        private readonly IMapper _mapper;
        public PermitDefinitionController(IGeneralTableRepo permissionRepo, IMapper mapper)
        {
            _permissionRepo = permissionRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<GeneralCode> permissions = (await _permissionRepo.GetAllAsync())
                .Where(obj => obj.GType == "4").ToList();
            List<PermissionDefDTO> permissionsDTO = _mapper.Map<List<PermissionDefDTO>>(permissions);
            foreach (var item in permissionsDTO)
            {
                if (item.TransType != 0)
                    item.PermissionDepended = (await _permissionRepo.GetByIdAsync(item.TransType)).GDesc;
                else
                    item.PermissionDepended = "";
            }
            return View(permissionsDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_PermissionDef()
        {
            //ViewBag.sectionId = await _permissionRepo.GetNewIdAsync();
            return View();
        }

        // Post Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add_PermissionDef(SectionEditAddDTO sectionDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Sections2 section = _mapper.Map<Sections2>(sectionDTO);
        //        int branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        //        section.BranchId = branchId;

        //        await _sectionRepo.AddAsync(section);
        //        await _sectionRepo.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(sectionDTO);
        //}

        // Get Edit
        //public async Task<IActionResult> Edit_PermissionDef(int id)
        //{
        //    Sections2 section = await _sectionRepo.GetByIdAsync(id);
        //    SectionEditAddDTO sectionDTO = _mapper.Map<SectionEditAddDTO>(section);
        //    return View(sectionDTO);
        //}

        // Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_PermissionDef(int id, SectionEditAddDTO sectionDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Sections2 section = await _sectionRepo.GetByIdAsync(id);
        //        int branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        //        sectionDTO.BranchId = branchId;
        //        _mapper.Map(sectionDTO, section);

        //        await _sectionRepo.UpdateAsync(section);
        //        await _sectionRepo.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(sectionDTO);
        //}

        // Delete
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete_PermissionDef(int id)
        //{
        //    Sections2 section = await _sectionRepo.GetByIdAsync(id);

        //    await _sectionRepo.DeleteAsync(section);
        //    await _sectionRepo.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
