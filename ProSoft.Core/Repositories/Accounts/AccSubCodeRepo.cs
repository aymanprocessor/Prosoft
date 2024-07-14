using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class AccSubCodeRepo :IAccSubCodeRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;

        public AccSubCodeRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<List<AccSubCodeDTO>> GetSubsByMainAsync(string id)
        {
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == id).ToListAsync();

            List<AccSubCodeDTO> accSubCodesDTO = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);
            return accSubCodesDTO;
        }

        public async Task<AccSubCodeDTO> GetSubByIDsAsync(string subCode, string maincode)
        {
            AccSubCode AccSubCode = await _Context.AccSubCodes
                .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == maincode);
            AccSubCodeDTO accSubCodeDTO = _mapper.Map<AccSubCodeDTO>(AccSubCode);

            return accSubCodeDTO;
        }

        public async Task<string> GetNewSubAsync(string mainCode)
        {
            var lastRecord = await _Context.AccSubCodes.OrderBy(obj => obj.SubCode)
                     .LastOrDefaultAsync(obj => obj.MainCode == mainCode);

            string subcode = "";
            if (lastRecord != null)
            {
                subcode = lastRecord.SubCode;
                var value = int.Parse(subcode) + 1;

                if (value < 10)
                    subcode = $"00000{value}";
                else if (value < 100 && value >= 10)
                    subcode = $"0000{value}";
                else if (value < 1000 && value >= 100)
                    subcode = $"000{value}";
                else if (value < 10000 && value >= 1000)
                    subcode = $"00{value}";
                else if (value < 100000 && value >= 10000)
                    subcode = $"0{value}";
                else if (value < 999999 && value >= 100000)
                    subcode = $"{value}";
            }
            else
            {
                subcode = "000001";
            }
            return subcode;
        }

        public async Task<string> GetParentCodeAsync(string code)
        {
            AccMainCode parentCode = await _Context.AccMainCodes
                .FirstOrDefaultAsync(obj => obj.MainCode == code);
            string grandCode = parentCode.ParentCode;
            return grandCode;
        }

        //Add 
        public async Task AddAccSubCodeAsync(string id, AccSubCodeDTO accSubDTO)
        {
            AccSubCode accSubCode = _mapper.Map<AccSubCode>(accSubDTO);

            accSubCode.CoCode = 1;
            accSubCode.MainCode = id;

            await _Context.AddAsync(accSubCode);
            await _Context.SaveChangesAsync();
        }

        public async Task EditAccSubCodeAsync(string subCode, AccSubCodeDTO subDTO)
        {
            AccSubCode existingSub = await _Context.AccSubCodes
                 .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == subDTO.MainCode);

            _mapper.Map(subDTO, existingSub);

            _Context.Update(existingSub);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteAccSubCodeAsync(string subCode, string mainCode)
        {
            AccSubCode existingSub = await _Context.AccSubCodes
                 .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == mainCode);

            _Context.Remove(existingSub);
            await _Context.SaveChangesAsync();
        }
    }
}
