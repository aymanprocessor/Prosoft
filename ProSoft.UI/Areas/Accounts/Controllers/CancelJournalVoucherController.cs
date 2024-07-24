using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class CancelJournalVoucherController : Controller
    {
        private readonly ICancelJournalVoucherRepo _cancelJournalVoucherRepo;
        public CancelJournalVoucherController(ICancelJournalVoucherRepo cancelJournalVoucherRepo)
        {
            _cancelJournalVoucherRepo = cancelJournalVoucherRepo;
        }
        public async Task<IActionResult> Index()
        {
            CancelJournalVoucherDTO reportExpenseAnalysisDTO = await _cancelJournalVoucherRepo.GetAllDataAsync();
            return View(reportExpenseAnalysisDTO);
        }
        public async Task<IActionResult> CancelOrRetrieved(int id, int fromVoucher, int toVoucher, int year, int mounth,int canOrRet)
        {
            var branch = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            var changeData="";
            if (canOrRet == 1) 
            { 
                 changeData = await _cancelJournalVoucherRepo.CancelAsync(id, fromVoucher, toVoucher, year, mounth, branch);
            }
            else if (canOrRet == 2)
            {
                 changeData = await _cancelJournalVoucherRepo.RetrievedAsync(id, fromVoucher, toVoucher, year, mounth, branch);
            }
            return Json(changeData);
        }
    }
}
