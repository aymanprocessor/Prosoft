using AutoMapper;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class TransDtlRepo: Repository<TransDtl, int>, ITransDtlRepo
    {
        private readonly IMapper _mapper;
        public TransDtlRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }


    }
}
