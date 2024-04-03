using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class MainClinicRepo : Repository<MainClinic, int>, IMainClinicRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public MainClinicRepo(AppDbContext Context , IMapper mapper) : base(Context)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<List<MainClinicViewDTO>> GetAllClinics()
        {
            List<MainClinicViewDTO> mainClinicsDTO =await _Context.MainClinics
                .Select(obj => new MainClinicViewDTO()
                {
                    ClinicId=obj.ClinicId,
                    ClinicDesc=obj.ClinicDesc,
                    SecName=obj.SysSectionNavigation.SecName,
                    MOnOff= Convert.ToInt32(obj.MOnOff),

                }).ToListAsync();
            return mainClinicsDTO;
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.ClinicId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
        public async Task<MainClinicEditAddDTO> GetMainClinicByIdAsync(int id)
        {
            MainClinic mainClinic = await _Context.MainClinics.FirstOrDefaultAsync(obj=>obj.ClinicId == id);
            MainClinicEditAddDTO mainClinicDTO = _mapper.Map<MainClinicEditAddDTO>(mainClinic);

            List<EisSectionType> eisSectionTypes = await _Context.EisSectionTypes.ToListAsync();
            mainClinicDTO.eisSectionTypes = _mapper.Map<List<EisSectionTypeDTO>>(eisSectionTypes);
            //reset sys code <Mapping can not do that>
            mainClinicDTO.SecCode = Convert.ToInt32(mainClinic.SysSection);

            return mainClinicDTO;   
        }
        public async Task<MainClinicEditAddDTO> GetEmptyClinicAsync()
        {
            MainClinicEditAddDTO mainClinicDTO = new MainClinicEditAddDTO();

            List<EisSectionType> eisSectionTypes = await _Context.EisSectionTypes.ToListAsync();
            mainClinicDTO.eisSectionTypes = _mapper.Map<List<EisSectionTypeDTO>>(eisSectionTypes);

            return mainClinicDTO;
        }


        public async Task AddMainClinicAsync(MainClinicEditAddDTO mainClinicDTO)
        {
            MainClinic mainClinic = _mapper.Map<MainClinic>(mainClinicDTO);
            mainClinic.SysSection = mainClinicDTO.SecCode;
            await _Context.AddAsync(mainClinic);
            await _Context.SaveChangesAsync();
        }
        public async Task EditMainClinicAsync(int id, MainClinicEditAddDTO mainClinicDTO)
        {
            MainClinic mainClinic = await _Context.MainClinics.FirstOrDefaultAsync(obj=>obj.ClinicId == id);
            //reset sys code
            mainClinic.SysSection = mainClinicDTO.SecCode;
            _mapper.Map(mainClinicDTO, mainClinic);
            _Context.Update(mainClinic);
            await _Context.SaveChangesAsync();
        }

    }
}
