﻿using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Shared
{
    public interface ISectionRepo : IRepository<Sections2, int>
    {
        Task<int> GetNewIdAsync();
    }
}
