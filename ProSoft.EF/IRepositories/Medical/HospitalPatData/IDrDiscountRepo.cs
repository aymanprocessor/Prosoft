﻿using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IDrDiscountRepo : IRepository<DrDiscount,int>
    {
        Task<List<DrDiscountViewDTO>> GetDoctorCommissions();
    }
}
