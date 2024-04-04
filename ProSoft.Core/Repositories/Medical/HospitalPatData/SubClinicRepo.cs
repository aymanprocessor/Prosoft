using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class SubClinicRepo : Repository<SubClinic, int>, ISubClinicRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public SubClinicRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<List<SubClinicViewDTO>> GetAllSubClinicsAsync(int id)
        {
            List<SubClinicViewDTO> subClinics = await _Context.SubClinics
                .Where(obj=> obj.ClinicId ==id)
                .Select(c => new SubClinicViewDTO()
                {
                    SClinicId = (int)c.SClinicId,
                    SClinicDesc = c.SClinicDesc,
                    TypDesc = c.Typ.TypDesc,
                    CostDesc = c.CostCodeNavigation.CostDesc,
                    MedicalFlag = c.MedicalFlag,
                    EinvItem = c.EinvItem,
                    SOnOff = c.SOnOff,
                    SrvInvShowFlg = c.SrvInvShowFlg,
                    Stknam = c.StockCdNavigation.Stknam,

                }).ToListAsync();

            return subClinics;
        }


        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.SClinicId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
      
        public async Task<SubClinicEditAddDTO> GetSubClinicByIdAsync(int id)
        {
            SubClinic subClinic = await _Context.SubClinics
                .FirstOrDefaultAsync(obj => obj.SClinicId == id);
            SubClinicEditAddDTO SubClinicDTO = _mapper.Map<SubClinicEditAddDTO>(subClinic);
            List<ServiceType> serviceTypes = await _Context.ServiceTypes.ToListAsync();
            List<Stock> stocks = await _Context.Stocks.ToListAsync();
            List<CostCenter> costs = await _Context.CostCenters.ToListAsync();

            SubClinicDTO.servesTypes = _mapper.Map<List<ServiceTypeViewDTO>>(serviceTypes);
            SubClinicDTO.stocks = _mapper.Map<List<StockViewDTO>>(stocks);
            SubClinicDTO.costs = _mapper.Map<List<CostCenterViewDTO>>(costs);
            return SubClinicDTO;
        }

        public async Task<SubClinicEditAddDTO> GetEmptySubClinicAsync()
        {
            SubClinicEditAddDTO subClinicDTO = new SubClinicEditAddDTO();
            List<ServiceType> serviceTypes = await _Context.ServiceTypes.ToListAsync();
            List<Stock> stocks = await _Context.Stocks.ToListAsync();
            List<CostCenter> costs = await _Context.CostCenters.ToListAsync();

            subClinicDTO.servesTypes = _mapper.Map<List<ServiceTypeViewDTO>>(serviceTypes);
            subClinicDTO.stocks = _mapper.Map<List<StockViewDTO>>(stocks);
            subClinicDTO.costs = _mapper.Map<List<CostCenterViewDTO>>(costs);
            return subClinicDTO;
        }
    }
}
