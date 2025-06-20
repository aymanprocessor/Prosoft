﻿using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Treasury
{
    public interface ICustCollectionsDiscountRepo :IRepository<CustCollectionsDiscount,int>
    {
        Task<List<CustCollectionsDiscountViewDTO>> GetAllCustCollectionsDiscountAsync(int id,string docType);
        Task<CustCollectionsDiscountEditAddDTO> GetEmptycustCollectionsDiscountAsync(int id, string docType);
        Task AddcustCollectionsDiscountAsync(int id,CustCollectionsDiscountEditAddDTO custCollectionsDiscountDTO);
        Task<CustCollectionsDiscountEditAddDTO> GetcustCollectionsDiscountByIdAsync(int id);
        Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode);
        Task EditcustCollectionsDiscountAsync(int id, CustCollectionsDiscountEditAddDTO custCollectionsDiscountDTO);


    }
}
