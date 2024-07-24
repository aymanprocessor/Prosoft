using ProSoft.EF.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface ICancelJournalVoucherRepo
    {
        Task<CancelJournalVoucherDTO> GetAllDataAsync();
        Task<string> CancelAsync(int journal, int fromVoucher, int toVoucher, int year, int mounth,int branch);
        Task<string> RetrievedAsync(int journal, int fromVoucher, int toVoucher, int year, int mounth,int branch);

    }
}
