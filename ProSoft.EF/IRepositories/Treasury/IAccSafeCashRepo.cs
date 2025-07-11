﻿using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Treasury
{
    public interface IAccSafeCashRepo : IRepository<AccSafeCash, int>
    {
        Task<List<AccSafeCashViewDTO>> GetAccSafeCashAsync(string docType, string flagType, int fYear ,int safeCode);
       // Task<int> GetNewIdAsync();
        Task<int> GetNewSerialAsync(string docType, int safeCode, int fYear);
        Task<UserCashNoEditAddDTO> GetMainSubForSafe(int safeCode);
        Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode);
        Task<AccSafeCashEditAddDTO> GetEmptyAccSafeCashAsync(int userCode);
        Task<string> AddAccSafeCashAsync(AccSafeCashEditAddDTO accSafeCashDTO);
        Task<AccSafeCashEditAddDTO> GetAccSafeCashByIdAsync(int id,int userCode);
        Task<string> EditAccSafeCashAsync(int id, AccSafeCashEditAddDTO accSafeCashDTO);
        Task<bool> HasRelatedDataAsync(int id, string doctype);
        Task<string> PostingToAcctrans_PaymentReceipt(AccSafeCash accSafeCash);
        Task<string> PostingToAcctrans_DisbursementPermission(AccSafeCash accSafeCash);
    }
}
