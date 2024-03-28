using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.IRepositories.Medical.Analysis;
using ProSoft.EF.Models.Medical.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.Analysis
{
    public class SubRepo : ISubRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;

        public SubRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<List<SubViewDTO>> GetSubsByMainAsync(string id)
        {
            List<MedicalAnalysisSub> subAnalysis = await _Context.MedicalAnalysisSubs
                .Where(obj => obj.MainCode == id).ToListAsync();

            List<SubViewDTO> subAnalysisDTO = _mapper.Map<List<SubViewDTO>>(subAnalysis);
            return subAnalysisDTO;
        }

        public async Task<SubViewDTO> GetSubByIDsAsync(string subCode, string maincode)
        {
            MedicalAnalysisSub subAnalysis = await _Context.MedicalAnalysisSubs
                .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == maincode);
            SubViewDTO subAnalysisDTO = _mapper.Map<SubViewDTO>(subAnalysis);

            return subAnalysisDTO;
        }

        public async Task<string> GetNewSubAsync(string mainCode)
        {
            var lastRecord = await _Context.MedicalAnalysisSubs.OrderBy(obj => obj.SubCode)
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
            MedicalAnalysisMain parentCode = await _Context.MedicalAnalysisMains
                .FirstOrDefaultAsync(obj => obj.MainCode == code);
            string grandCode = parentCode.ParentCode;
            return grandCode;
        }

        public async Task AddSubAnalysisAsync(string id, SubEditAddDTO subDTO)
        {
            MedicalAnalysisSub subAnalysis = _mapper.Map<MedicalAnalysisSub>(subDTO);

            subAnalysis.CoCode = 0;
            subAnalysis.MedicalFlag = 1;
            subAnalysis.MainCode = id;

            await _Context.AddAsync(subAnalysis);
            await _Context.SaveChangesAsync();
        }

        public async Task EditSubAnalysisAsync(string subCode, SubEditAddDTO subDTO)
        {
            MedicalAnalysisSub existingSub = await _Context.MedicalAnalysisSubs
                 .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == subDTO.MainCode);

            _mapper.Map(subDTO, existingSub);

            _Context.Update(existingSub);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteSubAnalysisAsync(string subCode, string mainCode)
        {
            MedicalAnalysisSub existingSub = await _Context.MedicalAnalysisSubs
                 .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == mainCode);

            _Context.Remove(existingSub);
            await _Context.SaveChangesAsync();
        }
    }
}
