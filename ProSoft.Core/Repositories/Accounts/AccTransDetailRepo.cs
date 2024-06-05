using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
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
            List<AccTransDetail> accTransDetails = await _Context.AccTransDetails.Where(obj => obj.TransType == journalCode.ToString() &&obj.TransId==transId).ToListAsync();
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

            //accTransDetailDTO.JournalName = (await _Context.JournalTypes.FindAsync(journalCode)).JournalName;
            //accTransDetailDTO.JournalCode = (await _Context.JournalTypes.FindAsync(journalCode)).JournalCode;
            return accTransDetailDTO;
        }
    }
}
