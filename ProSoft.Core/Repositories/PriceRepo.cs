using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories;
using ProSoft.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories
{
    public class PriceRepo : Repository<Price, int>, IPriceRepo
    {
        public PriceRepo(AppDbContext Context) : base(Context)
        {
        }
    }
}
