﻿using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class DegreeCodeRepo : Repository<DegreeCode, int>, IDegreeCodeRepo
    {
        public DegreeCodeRepo(AppDbContext Context) : base(Context)
        {
        }
        public async Task<int> GetNewIdAsync()
        {
            var maxId = await _Context.DegreeCodes
                .MaxAsync(b => (int?)b.Id);

            return (maxId ?? 0) + 1;
        }
    }
}
