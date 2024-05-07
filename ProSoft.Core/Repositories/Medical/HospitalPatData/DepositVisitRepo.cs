using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class DepositVisitRepo : Repository<Deposit, int>, IDepositVisitRepo
    {
        private readonly IMapper _mapper;

        public DepositVisitRepo(AppDbContext Context , IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<DepositViewDTO>> GetAllDepositesAsync(int id)
        {
            List<DepositViewDTO> depositDTOs = await _Context.Deposits.Where(obj => obj.MasterId == id)
                 .Select(obj => new DepositViewDTO()
                 {
                     DpsSer = (int)obj.DpsSer,
                     PostRecipt = (int)obj.PostRecipt,
                     DpsType = (int)obj.DpsType,
                     DpsVal =(decimal)obj.DpsVal,
                     DepositDesc = obj.DepositDesc,
                     //EinvType = (int)obj.EinvType,
                     //WinvItemsFlg = obj.WinvItemsFlg,
                 })
                 .ToListAsync();
            return depositDTOs;
        }
        public async Task<PatAdmissionViewDTO> GetPatData(int id)
        {
            PatAdmission patAdmission = await _Context.PatAdmissions.FirstOrDefaultAsync(obj=>obj.MasterId ==id);
            //get pat to get patname
            Pat pat = await _Context.Pats.FirstOrDefaultAsync(obj => obj.PatId == patAdmission.PatId);

            PatAdmissionViewDTO patAdmissionDTO = _mapper.Map<PatAdmissionViewDTO>(patAdmission);
            //set patname
            patAdmissionDTO.PatName = pat.PatName;

            return patAdmissionDTO;
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.DpsSer);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }

        public async Task<DepositEditAddDTO> GetEmptyDepositAsync(int id)
        {
            DepositEditAddDTO depositDTO = new DepositEditAddDTO();

            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.ToListAsync();

            depositDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);
            return depositDTO;
        }

        public async Task<DepositEditAddDTO> GetDepositByIdAsync(int id)
        {
            Deposit deposit = await _Context.Deposits.FirstOrDefaultAsync(obj=>obj.DpsSer ==id);

            DepositEditAddDTO depositDTO = _mapper.Map<DepositEditAddDTO>(deposit);
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.ToListAsync();

            depositDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);
            return depositDTO;
        }

        public async Task AddDepositDtllAsync(int id, DepositEditAddDTO depositDTO)
        {
            Deposit deposit = _mapper.Map<Deposit>(depositDTO);
            deposit.MasterId = id;
            PatAdmission patAdmission =await _Context.PatAdmissions.FirstOrDefaultAsync(obj=>obj.MasterId==id);
            deposit.PatId = patAdmission.PatId;
            deposit.ModId = 11;

            await _Context.AddAsync(deposit);
            await _Context.SaveChangesAsync();
        }

        public async Task EditDepositAsync(int id, DepositEditAddDTO depositDTO)
        {
            Deposit deposit = await _Context.Deposits.FirstOrDefaultAsync(obj => obj.DpsSer == id);
            deposit.ModId = 11;
            _mapper.Map(depositDTO, deposit);
            _Context.Update(deposit);
            await _Context.SaveChangesAsync();
        }
    }
}
