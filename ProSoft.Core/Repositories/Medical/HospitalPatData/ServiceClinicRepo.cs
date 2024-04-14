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
    public class ServiceClinicRepo : Repository<ServiceClinic, int>, IServiceClinicRepo
    {
        private readonly IMapper _mapper;
        public ServiceClinicRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<ServiceClinicViewDTO>> GetAllServClinicAsync(int id)
        {
           List<ServiceClinicViewDTO> serviceClinicDTO = await _Context.ServiceClinics
                .Where(obj=>obj.SClinicId ==id)
                .Select(s => new ServiceClinicViewDTO()
                {
                    ServId = (int)s.ServId,
                    ServDesc = s.ServDesc,
                    CostDesc = s.CostCodeNavigation.CostDesc,
                    PlValue = s.PlValue,
                    ServType = s.ServType,
                    DrPerc = s.DrPerc,
                    DrVal = s.DrVal,
                    ProtectId = s.ProtectId,
                    ServOnOff = s.ServOnOff,    
                })
                .ToListAsync();
            return serviceClinicDTO;
        }


        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.ServId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
        public async Task<ServClinicEditAddDTO> GetServClinicByIdAsync(int id)
        {
            ServiceClinic serviceClinic= await _Context.ServiceClinics
                .FirstOrDefaultAsync(s => s.ServId == id);
            ServClinicEditAddDTO servClinicDTO = _mapper.Map<ServClinicEditAddDTO>(serviceClinic);

            List<CostCenter> costs = await _Context.CostCenters.ToListAsync();
            servClinicDTO.costs = _mapper.Map<List<CostCenterViewDTO>>(costs);
            return servClinicDTO;
        }

        public async Task<ServClinicEditAddDTO> GetEmptyServClinicAsync()
        {
            ServClinicEditAddDTO servClinicDTO =new ServClinicEditAddDTO();
            List<CostCenter> costs = await _Context.CostCenters.ToListAsync();

            servClinicDTO.costs = _mapper.Map<List<CostCenterViewDTO>>(costs);
            return servClinicDTO;
        }


        public async Task AddServClinicAsync(int id, int clinicID, ServClinicEditAddDTO servClinicDTO)
        {
            ServiceClinic serviceClinic = _mapper.Map<ServiceClinic>(servClinicDTO);
            serviceClinic.SClinicId = id;
            serviceClinic.ClinicId = clinicID;
            
            serviceClinic.DrVal = serviceClinic.PlValue * (serviceClinic.DrPerc / 100);
            await _Context.AddAsync(serviceClinic);
            await _Context.SaveChangesAsync();

        }

        public async Task EditServClinicAsync(int id, ServClinicEditAddDTO servClinicDTO)
        {
            ServiceClinic serviceClinic = await _Context.ServiceClinics
                .FirstOrDefaultAsync(obj => obj.ServId == id);
            _mapper.Map(servClinicDTO, serviceClinic);
            serviceClinic.DrVal = serviceClinic.PlValue * (serviceClinic.DrPerc / 100);
            _Context.Update(serviceClinic);
            await _Context.SaveChangesAsync();
        }
    }
}
