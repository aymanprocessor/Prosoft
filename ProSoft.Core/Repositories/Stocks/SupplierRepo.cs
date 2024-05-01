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
            return supplierDTO;
        }
    }
}
