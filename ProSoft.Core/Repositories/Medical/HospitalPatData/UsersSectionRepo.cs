using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Migrations;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class UsersSectionRepo : Repository<UsersSection, int>, IUsersSectionRepo
    {
        private readonly IMapper _mapper;

        public UsersSectionRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }
        public async Task<List<UsersSectionDTO>> GetMedicalServicesForUser(int userCode)
        {
            List<UsersSection> usersSections = await _Context.UsersSections
               .Where(obj => obj.UserCode == userCode) .ToListAsync();

            var UsersSectionDTO = new List<UsersSectionDTO>();
            foreach (var item in usersSections)
            {
                var myUsersSectionDTO = _mapper.Map<UsersSectionDTO>(item);
                if (item.ClinicId ==1000)
                {
                    myUsersSectionDTO.ClinicDes = "كل الاقسام";
                }
                else
                {
                    myUsersSectionDTO.ClinicDes = (await _Context.MainClinics
                        .FirstOrDefaultAsync(obj => obj.ClinicId == item.ClinicId)).ClinicDesc;
                }
                UsersSectionDTO.Add(myUsersSectionDTO);
            }
            return UsersSectionDTO;
        }
        public async Task<UsersSectionDTO> GetEmptyUsersSectionAsync(int userCode)
        {
            var usersSectionDTO = new UsersSectionDTO();

            List<UsersSection> usersSections = await _Context.UsersSections.Where(obj=>obj.UserCode ==userCode).ToListAsync();
            List<MainClinic> mainClinics = new List<MainClinic> { new MainClinic { ClinicId = 1000, ClinicDesc = "كل الاقسام" } };
            List<MainClinic> AllmainClinics = await _Context.MainClinics.ToListAsync();
            foreach(var main in AllmainClinics)
            {
                var isExisted = false;
                foreach (var item in usersSections) 
                {
                    if (main.ClinicId == item.ClinicId)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (!isExisted) 
                {
                    mainClinics.Add(main);
                }
            }
            usersSectionDTO.MainClinics = _mapper.Map<List<MainClinicViewDTO>>(mainClinics);
            return usersSectionDTO;
        }
        public async Task<UsersSectionDTO> GetUsersSectionByIdAsync(int id)
        {
            UsersSection usersSection = await _DbSet.FirstOrDefaultAsync(obj => obj.USecId == id);
            var usersSectionDTO = _mapper.Map<UsersSectionDTO>(usersSection);

            List<UsersSection> usersSections = await _Context.UsersSections.Where(obj => obj.UserCode == usersSection.UserCode).ToListAsync();
            List<MainClinic> mainClinics = new List<MainClinic> { new MainClinic { ClinicId = 1000, ClinicDesc = "كل الاقسام" } };
            List<MainClinic> AllmainClinics = await _Context.MainClinics.ToListAsync();
            foreach (var main in AllmainClinics)
            {
                var isExisted = false;
                foreach (var item in usersSections)
                {
                    if (main.ClinicId == item.ClinicId)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (!isExisted)
                {
                    mainClinics.Add(main);
                }
            }
            //to send mainClinic type he choose it
            MainClinic mainClinic = await _Context.MainClinics.FirstOrDefaultAsync(obj => obj.ClinicId == usersSection.ClinicId);
            mainClinics.Add(mainClinic);
            usersSectionDTO.MainClinics = _mapper.Map<List<MainClinicViewDTO>>(mainClinics);
            return usersSectionDTO;
        }
    }
}
