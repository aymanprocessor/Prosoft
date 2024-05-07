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
    public class CustomerController : Controller
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerRepo customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<CustCode> customers = await _customerRepo.GetAllAsync();
            var customersDTO = _mapper.Map<List<CustCodeViewDTO>>(customers);
            return View(customersDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_Customer()
        {
            CustCodeEditAddDTO customerDTO = await _customerRepo.GetEmptyCustomerAsync();
            return View(customerDTO);
        }

        //// Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Customer(CustCodeEditAddDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                CustCode customer = _mapper.Map<CustCode>(customerDTO);
                customer.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                customer.Flag = 2;

                await _customerRepo.AddAsync(customer);
                await _customerRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_Customer(int id)
        {
            CustCodeEditAddDTO customerDTO = await _customerRepo.GetCustomerByIdAsync(id);
            return View(customerDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Customer(int id, CustCodeEditAddDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                CustCode customer = await _customerRepo.GetByIdAsync(id);
                _mapper.Map(customerDTO, customer);
                customer.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                customer.Flag = 2;

                await _customerRepo.UpdateAsync(customer);
                await _customerRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Customer(int id)
        {
            CustCode customer = await _customerRepo.GetByIdAsync(id);

            await _customerRepo.DeleteAsync(customer);
            await _customerRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
