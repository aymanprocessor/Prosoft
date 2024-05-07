using AutoMapper;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class StockEmpRepo: IStockEmpRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public StockEmpRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        //public Task<List<PermissionDefViewDTO>> GetPermissionsForUserAsync(int userCode)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<StockEmpEditAddDTO>> GetPermissionsForUserAsync(int userCode, int transType)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<StockEmpEditAddDTO> GetUserTransByIdAsync(int userCode, int gId)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<StockEmpEditAddDTO> GetEmptyStockTransAsync()
        {
            var stockTransDTO = new StockEmpEditAddDTO();

            throw new NotImplementedException();
        }

        public Task UpdateUserTransAsync(int userCode, StockEmpEditAddDTO userStockDTO)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
