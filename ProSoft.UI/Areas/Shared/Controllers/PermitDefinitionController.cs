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
            List<PermissionDefViewDTO> permissionsDTO = await _permissionRepo.GetAllPermissionsAsync("4");
            return View(permissionsDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_PermissionDef()
        {
            PermissionDefEditAddDTO permissionDTO = await _permissionRepo.GetEmptyPermissionAsync();
            return View(permissionDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_PermissionDef(PermissionDefEditAddDTO permissionDTO)
        {
            if (ModelState.IsValid)
            {
                GeneralCode permission = _mapper.Map<GeneralCode>(permissionDTO);
                permission.GType = "4";

                await _permissionRepo.AddAsync(permission);
                await _permissionRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permissionDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_PermissionDef(int id)
        {
            PermissionDefEditAddDTO permissionDTO = await _permissionRepo.GetPermissionByIdAsync(id);
            return View(permissionDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_PermissionDef(int id, PermissionDefEditAddDTO permissionDTO)
        {
            if (ModelState.IsValid)
            {
                GeneralCode permission = await _permissionRepo.GetByIdAsync(id);
                _mapper.Map(permissionDTO, permission);
                //permission.GType = "4";

                await _permissionRepo.UpdateAsync(permission);
                await _permissionRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permissionDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_PermissionDef(int id)
        {
            GeneralCode permission = await _permissionRepo.GetByIdAsync(id);

            await _permissionRepo.DeleteAsync(permission);
            await _permissionRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
