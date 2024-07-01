using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class AccTransDetailRepo : Repository<AccTransDetail, int>, IAccTransDetailRepo
    {
        private readonly IMapper _mapper;
        public AccTransDetailRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }
        public async Task<List<AccTransDetailViewDTO>> GetAccTransDetailAsync(int transId, int journalCode)
        {
            List<AccTransDetail> accTransDetails = await _Context.AccTransDetails.Where(obj => obj.TransType == journalCode.ToString() && obj.TransId==transId).ToListAsync();
            var accTransDetailDTOs = new List<AccTransDetailViewDTO>();
            foreach (var item in accTransDetails)
            {
                var myAccTransDetailDTO = _mapper.Map<AccTransDetailViewDTO>(item);
                myAccTransDetailDTO.AccountName = await GetAccountName(item.MainCode, item.SubCode);
                accTransDetailDTOs.Add(myAccTransDetailDTO);
            }
            return accTransDetailDTOs;
        }
        private async Task<string> GetAccountName(string mainCode, string subCode)
        {
            AccMainCode myMainCode = await _Context.AccMainCodes
                .FirstOrDefaultAsync(obj => obj.MainCode == mainCode);
            string mainName = myMainCode.MainName;

            AccSubCode mySubCode = await _Context.AccSubCodes
                .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == mainCode);
            string subName = mySubCode! != null ? mySubCode.SubName : string.Empty;

            return subName != "" ? $"{mainName} / {subName}" : mainName;
        }
        public async Task<AccTransDetailEditAddDTO> GetEmptyAccTransDetailAsync(int id,int journalCode)
        {
            AccTransMaster accTransMaster = await _Context.AccTransMasters.FindAsync(id);

            AccTransDetailEditAddDTO accTransDetailDTO = new AccTransDetailEditAddDTO();
            accTransDetailDTO.DocNo = accTransMaster.DocNo;
            accTransDetailDTO.DocDate = accTransMaster.DocDate;
            accTransDetailDTO.LineDesc = accTransMaster.TransDesc;
            accTransDetailDTO.FYear = accTransMaster.FYear;
            accTransDetailDTO.TransDate = accTransMaster.TransDate;
            accTransDetailDTO.TransNo = accTransMaster.TransNo;
            accTransDetailDTO.CurCode = accTransMaster.CurCode;
            accTransDetailDTO.TransType = accTransMaster.TransType;
            accTransDetailDTO.YearTransNo = accTransMaster.YearTransNo;
            accTransDetailDTO.CoCode = accTransMaster.CoCode;
            accTransDetailDTO.TransId =id;

            List<CostCenter> costCenters = await _Context.CostCenters.ToListAsync();
            List<AccMainCode> mainAccCodes = await _Context.AccMainCodes.ToListAsync();

            accTransDetailDTO.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            accTransDetailDTO.MainAccCodes = _mapper.Map<List<AccMainCodeDTO>>(mainAccCodes);

            //accTransDetailDTO.JournalName = (await _Context.JournalTypes.FindAsync(journalCode)).JournalName;
            //accTransDetailDTO.JournalCode = (await _Context.JournalTypes.FindAsync(journalCode)).JournalCode;
            return accTransDetailDTO;
        }
        //get all details for trans master
        public async Task<decimal> GetValDep(int transId)
        {
            List<AccTransDetail> accTransDetails = await _Context.AccTransDetails
                 .Where(obj => obj.TransId == transId).ToListAsync();

            decimal valDep = 0;
            foreach (var item in accTransDetails)
            {
                valDep += Convert.ToDecimal(item.ValDep ?? 0);
            }
            return valDep;
        }
        public async Task<decimal> GetValCredit(int transId)
        {
            List<AccTransDetail> accTransDetails = await _Context.AccTransDetails
                 .Where(obj => obj.TransId == transId).ToListAsync();

            decimal valCredit = 0;
            foreach (var item in accTransDetails)
            {
                valCredit += Convert.ToDecimal(item.ValCredit ?? 0);
            }
            return valCredit;
        }
        public async Task AddAccTransDetailAsync(AccTransDetailEditAddDTO accTransDetailDTO)
        {
            AccTransDetail accTransDetail = _mapper.Map<AccTransDetail>(accTransDetailDTO);

            var count =(await _Context.AccTransDetails.Where(obj=>obj.TransId == accTransDetailDTO.TransId).ToListAsync()).Count;
            accTransDetail.TransSerial = count + 1;

            accTransDetail.EntryDate = DateTime.Now;
            await _Context.AddAsync(accTransDetail);
            await _Context.SaveChangesAsync();
        }
        public async Task<AccTransDetailEditAddDTO> GetAccTransDetailByIdAsync(int id)
        {
            AccTransDetail accTransDetail = await _Context.AccTransDetails.FirstOrDefaultAsync(obj => obj.TransDtlId == id);

            AccTransDetailEditAddDTO accTransDetailDTO = _mapper.Map<AccTransDetailEditAddDTO>(accTransDetail);

            List<CostCenter> costCenters = await _Context.CostCenters.ToListAsync();
            List<AccMainCode> mainAccCodes = await _Context.AccMainCodes.ToListAsync();
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.Where(obj=>obj.MainCode== accTransDetail.MainCode).ToListAsync();


            accTransDetailDTO.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            accTransDetailDTO.MainAccCodes = _mapper.Map<List<AccMainCodeDTO>>(mainAccCodes);
            accTransDetailDTO.SubAccCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);


            return accTransDetailDTO;
        }
        public async Task EditAccTransDetailAsync(int id, AccTransDetailEditAddDTO accTransDetailDTO)
        {

            AccTransDetail accTransDetail = await _Context.AccTransDetails.FirstOrDefaultAsync(obj => obj.TransDtlId == id);
            _mapper.Map(accTransDetailDTO, accTransDetail);
            accTransDetail.UserDateModify = DateTime.Now;
            _Context.Update(accTransDetail);
            await _Context.SaveChangesAsync();
        }

        public async Task DeletedAllDetailAsync(int id)
        {
            List<AccTransDetail> accTransDetails = await _Context.AccTransDetails.Where(obj => obj.TransId == id).ToListAsync();
            _Context.RemoveRange(accTransDetails);
            await _Context.SaveChangesAsync();
        }
    }
}
