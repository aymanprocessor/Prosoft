using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class CustomerRepo : Repository<CustCode, int>, ICustomerRepo
    {
        private readonly IMapper _mapper;
        public CustomerRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<string> GetNewIdAsync()
        {
            string newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.CustCode1);
                newID = $"{int.Parse(lastID) + 1}";
            }
            else
                newID = "1";
            return newID;
        }

        public async Task<CustCodeEditAddDTO> GetEmptyCustomerAsync()
        {
            var customerDTO = new CustCodeEditAddDTO();

            EisPosting sysAccount = await _Context.EisPostings.FindAsync(1);

            List<AccSubCode> subCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == sysAccount.MainCode).ToListAsync();
            List<PriceList> pricesLists = await _Context.PriceLists.ToListAsync();
            List<AdjectiveCust> priceTypes = await _Context.AdjectiveCusts.ToListAsync();

            customerDTO.SubCodes = _mapper.Map<List<AccSubCodeDTO>>(subCodes);
            customerDTO.PricesLists = _mapper.Map<List<PriceListViewDTO>>(pricesLists);
            customerDTO.PriceTypes = _mapper.Map<List<AdjectiveCustDTO>>(priceTypes);
            customerDTO.MainCode = sysAccount.MainCode;
            customerDTO.CustCode1 = await GetNewIdAsync();
            return customerDTO;
        }

        public async Task<CustCodeEditAddDTO> GetCustomerByIdAsync(int id)
        {
            CustCode customer = await GetByIdAsync(id);
            var customerDTO = _mapper.Map<CustCodeEditAddDTO>(customer);

            List<AccSubCode> subCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == customerDTO.MainCode).ToListAsync();
            List<PriceList> pricesLists = await _Context.PriceLists.ToListAsync();
            List<AdjectiveCust> priceTypes = await _Context.AdjectiveCusts.ToListAsync();

            customerDTO.SubCodes = _mapper.Map<List<AccSubCodeDTO>>(subCodes);
            customerDTO.PricesLists = _mapper.Map<List<PriceListViewDTO>>(pricesLists);
            customerDTO.PriceTypes = _mapper.Map<List<AdjectiveCustDTO>>(priceTypes);

            return customerDTO;
        }
    }
}
