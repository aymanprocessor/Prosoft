using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProSoft.Core.Repositories;
using ProSoft.EF.DTOs.Stocks.ExpenseData;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Area("Stocks")]
    [Authorize]
    public class ExpenseDataController : Controller
    {

        private readonly IExpenseDataRepo _expenseDataRepo;
        private readonly IMapper _mapper;

        public ExpenseDataController(IExpenseDataRepo expenseDataRepo, IMapper mapper)
        {
            _expenseDataRepo = expenseDataRepo;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IActionResult Add()
        {
            ExpenseDataAddEditDTO expenseDataAddEditDTO = new();
            return View(expenseDataAddEditDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ExpenseDataAddEditDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ExpenseData expenseData = _mapper.Map<ExpenseData>(model);
            await _expenseDataRepo.AddAsync(expenseData);
            await _expenseDataRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var existingExpense = await _expenseDataRepo.GetByIdAsync(id);
            if (existingExpense == null) return NotFound("Expense Id is Not Found");

            ExpenseDataAddEditDTO expenseDataAddEditDTO = _mapper.Map<ExpenseDataAddEditDTO>(existingExpense);

            return View(expenseDataAddEditDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExpenseDataAddEditDTO model)
        {
            var existingExpense = await _expenseDataRepo.GetByIdAsync(model.Id);
            if (existingExpense == null) return NotFound("Expanse Id is Not Found");

            _mapper.Map(model, existingExpense);
            await _expenseDataRepo.UpdateAsync(existingExpense);
            await _expenseDataRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var existingExpense = await _expenseDataRepo.GetByIdAsync(id);
            if (existingExpense == null) return NotFound("Expanse Id is Not Found");

            await _expenseDataRepo.DeleteAsync(existingExpense);
            await _expenseDataRepo.SaveChangesAsync();
            return Json(new GeneralResponse<ExpenseData>() { Success=true,Message="Item Delete Successfully"});
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpense()
        {
            var Expenses = (await _expenseDataRepo.GetAllAsync()).ToList();
            var result = new
            {
                draw = 1,
                recordsTotal = Expenses.Count(),
                recordsFiltered = Expenses.Count(),
                data = Expenses
            };

            return Json(result);
        }
    }
}
