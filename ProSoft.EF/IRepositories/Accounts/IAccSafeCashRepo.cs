using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IAccSafeCashRepo : IRepository<AccSafeCash,int>
    {
        Task<List<AccSafeCashViewDTO>> GetAccSafeCashAsync(string docType, string flagType);
        Task<int> GetNewIdAsync();
        Task<int> GetNewSerialAsync();
        Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode);

        Task<AccSafeCashEditAddDTO> GetEmptyPaymentReceiptAsync();
        Task AddPaymentReceiptAsync(AccSafeCashEditAddDTO accSafeCashDTO);
        Task<AccSafeCashEditAddDTO> GetPaymentReceiptByIdAsync(int id);
        Task EditPaymentReceiptAsync(int id, AccSafeCashEditAddDTO accSafeCashDTO);
    }
}
