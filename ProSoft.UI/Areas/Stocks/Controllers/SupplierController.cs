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
            var suppliersDTO = _mapper.Map<List<SupCodeEditAddDTO>>(suppliers);
            return View(suppliersDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_Supplier()
        {
            SupCodeEditAddDTO supplierDTO = await _supplierRepo.GetEmptySupplierAsync();
            return View(supplierDTO);
        }

        // Post Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add_Supplier(EisPostingEditAddDTO eisPostingDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        EisPosting eisPosting = _mapper.Map<EisPosting>(eisPostingDTO);
        //        eisPosting.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

        //        await _eisPostingRepo.AddAsync(eisPosting);
        //        await _eisPostingRepo.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(eisPostingDTO);
        //}

        // Get Edit
        //public async Task<IActionResult> Edit_Supplier(int id)
        //{
        //    EisPostingEditAddDTO eisPostingDTO = await _eisPostingRepo.GetEisPostingByIdAsync(id);
        //    return View(eisPostingDTO);
        //}

        // Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_Supplier(int id, EisPostingEditAddDTO eisPostingDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        EisPosting eisPosting = await _eisPostingRepo.GetByIdAsync(id);
        //        _mapper.Map(eisPostingDTO, eisPosting);
        //        eisPosting.PostId = id;

        //        await _eisPostingRepo.UpdateAsync(eisPosting);
        //        await _eisPostingRepo.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(eisPostingDTO);
        //}

        // Delete
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete_Supplier(int id)
        //{
        //    EisPosting eisPosting = await _eisPostingRepo.GetByIdAsync(id);

        //    await _eisPostingRepo.DeleteAsync(eisPosting);
        //    await _eisPostingRepo.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
