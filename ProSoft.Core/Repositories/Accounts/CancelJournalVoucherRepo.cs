using AutoMapper;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class CancelJournalVoucherRepo: ICancelJournalVoucherRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public CancelJournalVoucherRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
    }
}
