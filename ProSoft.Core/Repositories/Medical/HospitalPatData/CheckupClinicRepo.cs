using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class CheckupClinicRepo : Repository<CheckupClinic, int>, ICheckupClinicRepo
    {
        private readonly IMapper _mapper;
        
        public CheckupClinicRepo(AppDbContext context , IMapper mapper):base(context)
        {
            _mapper = mapper;   
        }
        public async Task<List<CheckupClinicDTO>> All()
        {
            List<CheckupClinic> checkupClinics = await _Context.CheckupClinics.ToListAsync();
            List<CheckupClinicDTO> checkupClinicDTOs = _mapper.Map<List<CheckupClinicDTO>>(checkupClinics);
            return checkupClinicDTOs;

        }

      

        public async Task<int> MaxCode()
        {
            int maxCode = 0;
            if (_DbSet.Count() != 0)
            {
                var max = await _DbSet.MaxAsync(obj => obj.CheckupId);
                maxCode = max + 1;
            }
            else
            {
                maxCode = 1;
            }

            return maxCode;
        }


        public async Task<CheckupClinicDTO> GetEditAsync(int id)
        {
            CheckupClinic checkup = await _Context.CheckupClinics.FirstOrDefaultAsync(obj => obj.CheckupId == id );

            CheckupClinicDTO _checkupClinicDTO = _mapper.Map<CheckupClinicDTO>(checkup);


            return _checkupClinicDTO;
        }

        public async Task<CheckupClinic>GetByIdAsync(int id)
        {
            CheckupClinic checkup = await _Context.CheckupClinics.FirstOrDefaultAsync(obj => obj.CheckupId == id);

            return checkup;
        }
    }
}
