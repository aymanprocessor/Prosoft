using AutoMapper;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Stocks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class SupplierRepo: ISupplierRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public SupplierRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
    }
}
