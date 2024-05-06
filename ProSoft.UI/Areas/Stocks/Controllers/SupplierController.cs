using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Stocks")]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepo _supplierRepo;
        private readonly IMapper _mapper;
        public SupplierController(ISupplierRepo supplierRepo, IMapper mapper)
        {
            _supplierRepo = supplierRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<SupCode> suppliers = await _supplierRepo.GetAllAsync();
            var suppliersDTO = _mapper.Map<List<SupCodeViewDTO>>(suppliers);
            return View(suppliersDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_Supplier()
        {
            SupCodeEditAddDTO supplierDTO = await _supplierRepo.GetEmptySupplierAsync();
            return View(supplierDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Supplier(SupCodeEditAddDTO supplierDTO)
        {
            if (ModelState.IsValid)
            {
                SupCode supplier = _mapper.Map<SupCode>(supplierDTO);
                supplier.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

                await _supplierRepo.AddAsync(supplier);
                await _supplierRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplierDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_Supplier(int id)
        {
            SupCodeEditAddDTO supplierDTO = await _supplierRepo.GetSupplierByIdAsync(id);
            return View(supplierDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Supplier(int id, SupCodeEditAddDTO supplierDTO)
        {
            if (ModelState.IsValid)
            {
                SupCode supplier = await _supplierRepo.GetByIdAsync(id);
                supplierDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                _mapper.Map(supplierDTO, supplier);
                supplier.Sup = id;

                await _supplierRepo.UpdateAsync(supplier);
                await _supplierRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplierDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Supplier(int id)
        {
            SupCode supplier = await _supplierRepo.GetByIdAsync(id);

            await _supplierRepo.DeleteAsync(supplier);
            await _supplierRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
