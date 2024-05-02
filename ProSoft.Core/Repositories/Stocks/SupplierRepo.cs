using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Calculus;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Calculus;
using ProSoft.EF.Models.Shared;
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

        public async Task<string> GetNewIdAsync()
        {
            string newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.SupCode1);
                newID = $"{int.Parse(lastID) + 1}";
            }
            else
                newID = "1";
            return newID;
        }

        public async Task<SupCodeEditAddDTO> GetEmptySupplierAsync()
        {
            var supplierDTO = new SupCodeEditAddDTO();

            EisPosting sysAccount = await _Context.EisPostings.FindAsync(11);

            List<AccSubCode> subCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == sysAccount.MainCode).ToListAsync();
            List<CityCode> cities = await _Context.CityCodes.ToListAsync();
            List<PlaceCode> places = await _Context.PlaceCodes.ToListAsync();

            supplierDTO.SubCodes = _mapper.Map<List<AccSubCodeDTO>>(subCodes);
            supplierDTO.Cities = _mapper.Map<List<CityCodeDTO>>(cities);
            supplierDTO.Places = _mapper.Map<List<PlaceCodeDTO>>(places);
            supplierDTO.MainCode = sysAccount.MainCode;
            supplierDTO.SupCode1 = await GetNewIdAsync();
            return supplierDTO;
        }

        public async Task<SupCodeEditAddDTO> GetSupplierByIdAsync(int id)
        {
            SupCode supplier = await GetByIdAsync(id);
            var supplierDTO = _mapper.Map<SupCodeEditAddDTO>(supplier);

            List<AccSubCode> subCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == supplierDTO.MainCode).ToListAsync();
            List<CityCode> cities = await _Context.CityCodes.ToListAsync();
            List<PlaceCode> places = await _Context.PlaceCodes.ToListAsync();

            supplierDTO.SubCodes = _mapper.Map<List<AccSubCodeDTO>>(subCodes);
            supplierDTO.Cities = _mapper.Map<List<CityCodeDTO>>(cities);
            supplierDTO.Places = _mapper.Map<List<PlaceCodeDTO>>(places);

            return supplierDTO;
        }
    }
}
