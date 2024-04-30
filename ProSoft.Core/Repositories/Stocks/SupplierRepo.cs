using AutoMapper;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class SupplierRepo: Repository<SupCode, int>, ISupplierRepo
    {
        private readonly IMapper _mapper;
        public SupplierRepo(AppDbContext Context, IMapper mapper): base(Context)
        {
            _mapper = mapper;
        }
    }
}
