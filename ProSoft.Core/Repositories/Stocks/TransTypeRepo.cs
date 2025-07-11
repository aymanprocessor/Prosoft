﻿using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class TransTypeRepo: Repository<StoreTran, int>, ITransTypeRepo
    {
        public TransTypeRepo(AppDbContext Context): base(Context) { }
    }
}
