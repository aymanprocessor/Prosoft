using AutoMapper;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class AccSafeCashRepo : Repository<AccSafeCash, int>, IAccSafeCashRepo
    {

        private readonly IMapper _mapper;
        public AccSafeCashRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }
    }
}
