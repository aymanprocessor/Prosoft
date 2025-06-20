﻿using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IDepositVisitRepo : IRepository<Deposit,int>
    {
        Task<List<DepositViewDTO>> GetAllDepositesAsync(int id);
        Task<PatAdmissionViewDTO> GetPatData(int id);
        Task<int> GetNewIdAsync();
        Task<DepositEditAddDTO> GetEmptyDepositAsync(int id);
        Task<DepositIndexMessagesDTO> AddDepositDtllAsync(int id, DepositEditAddDTO depositDTO);
        Task<DepositEditAddDTO> GetDepositByIdAsync(int id);
        Task<DepositIndexMessagesDTO> EditDepositAsync(int id, DepositEditAddDTO depositDTO);
        Task<string> PostingToCash(Deposit deposit, PatAdmission patAdmission);
        Task<PostingAccMasterMINIDTO> PostingToAcctransMaster(Deposit deposit, PatAdmission patAdmission);
        Task<string> PostingToAcctransDetail(Deposit deposit, PatAdmission patAdmission, int transNo,int transId);
    }
}
