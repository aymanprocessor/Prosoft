using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Stocks")]
    public class UserStockTransactionController : Controller
    {
        private readonly IStockEmpRepo _userStockRepo;
        private readonly IUserRepo _userRepo;
        //private readonly IGeneralTableRepo _generalCodeRepo;
        //private readonly ITransTypeRepo _transTypeRepo;
        private readonly IMapper _mapper;

        public UserStockTransactionController(IStockEmpRepo serStockRepo
            , IUserRepo userRepo, /*IGeneralTableRepo generalCodeRepo,*/
            /*ITransTypeRepo transTypeRepo,*/ IMapper mapper)
        {
            _userStockRepo = serStockRepo;
            _userRepo = userRepo;
            //_generalCodeRepo = generalCodeRepo;
            //_transTypeRepo = transTypeRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await _userRepo.GetAllUsersAsync();
            List<UserDTO> usersDTO = _mapper.Map<List<UserDTO>>(users);
            return View(usersDTO);
        }

        //public async Task<IActionResult> GetStockTransForUser(int id)
        //{
        //    List<UserTransViewDTO> permissionsDTO = await _userTransRepo
        //        .GetPermissionsForUserAsync(id, transType);
        //    return Json(usersDTO);
        //}

        // Get Add
        public async Task<IActionResult> Add_StockTrans(int id)
        {
            ViewBag.userName = (await _userRepo.GetUserByIdAsync(id)).UserName;
            StockEmpEditAddDTO stockTransDTO = await _userStockRepo.GetEmptyStockTransAsync();
            return View(stockTransDTO);
        }

        //// Post Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add_StockTrans(CustCodeEditAddDTO customerDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        CustCode customer = _mapper.Map<CustCode>(customerDTO);
        //        customer.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        //        customer.Flag = 2;

        //        await _customerRepo.AddAsync(customer);
        //        await _customerRepo.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(customerDTO);
        //}

        // Get Edit
        //public async Task<IActionResult> Edit_StockTrans(int id)
        //{
        //    CustCodeEditAddDTO customerDTO = await _customerRepo.GetCustomerByIdAsync(id);
        //    return View(customerDTO);
        //}

        // Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_StockTrans(int id, CustCodeEditAddDTO customerDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        CustCode customer = await _customerRepo.GetByIdAsync(id);
        //        _mapper.Map(customerDTO, customer);
        //        customer.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        //        customer.Flag = 2;

        //        await _customerRepo.UpdateAsync(customer);
        //        await _customerRepo.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(customerDTO);
        //}

        // Delete
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete_StockTrans(int id)
        //{
        //    CustCode customer = await _customerRepo.GetByIdAsync(id);

        //    await _customerRepo.DeleteAsync(customer);
        //    await _customerRepo.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
