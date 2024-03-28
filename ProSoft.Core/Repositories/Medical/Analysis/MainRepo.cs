using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Medical.Analysis;
using ProSoft.EF.Models.Medical.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.Analysis
{
    public class MainRepo : IMainRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;

        public MainRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<List<MainViewDTO>> GetMainsByLevelAsync(double level)
        {
            List<MedicalAnalysisMain> mainLevels = await _Context.MedicalAnalysisMains
                .Where(obj => obj.CurrentLevel == level).ToListAsync();
            List<MainViewDTO> mainLevelsDTO = _mapper.Map<List<MainViewDTO>>(mainLevels);
            return mainLevelsDTO;
        }

        public async Task<MainViewDTO> GetMainByIdAsync(string id)
        {
            MedicalAnalysisMain mainById = await _Context.MedicalAnalysisMains
                .FirstOrDefaultAsync(obj => obj.MainCode == id);

            MainViewDTO mainByIdDTO = _mapper.Map<MainViewDTO>(mainById);

            return mainByIdDTO;
        }

        public async Task<string> GetParentCodeAsync(string code)
        {
            MedicalAnalysisMain existingMain = await _Context.MedicalAnalysisMains
                .FirstOrDefaultAsync(obj => obj.MainCode == code);

            string parentCode = existingMain?.ParentCode;
            return parentCode;
        }

        public async Task<string> GetNewMain_3_Async()
        {
            MedicalAnalysisMain lastRecord = await _Context.MedicalAnalysisMains.OrderBy(obj => obj.MainCode)
                   .LastOrDefaultAsync(obj => obj.CurrentLevel == 3);

            var maincode = "";
            if (lastRecord != null)
            {
                maincode = lastRecord.MainCode;
                var firstsup = maincode.Substring(0, 1);
                var secondsup = maincode.Substring(1, 2);
                var thirdsup = maincode.Substring(3);

                var value = int.Parse(secondsup) + 1;

                if (value < 10)
                    secondsup = $"0{value}";
                else
                    secondsup = $"{value}";

                maincode = firstsup + secondsup + thirdsup;
            }
            else
                maincode = "101000000";

            return maincode;
        }

        public async Task<string> GetNewMain_4_Async(string id)
        {
            MedicalAnalysisMain lastRecord = await _Context.MedicalAnalysisMains.OrderBy(obj => obj.MainCode)
                   .LastOrDefaultAsync(obj => obj.CurrentLevel == 4 && obj.MainCode.Substring(0, 3) == id.Substring(0, 3));

            var maincode = "";
            if (lastRecord != null)
            {
                maincode = lastRecord.MainCode;
                var firstsup = maincode.Substring(0, 3);
                var secondsup = maincode.Substring(3, 2);
                var thirdsup = maincode.Substring(5);

                var value = int.Parse(secondsup) + 1;
                if (value < 10)
                    secondsup = $"0{value}";
                else
                    secondsup = $"{value}";

                maincode = firstsup + secondsup + thirdsup;
            }
            else
            {
                maincode = id;
                var firstsup = maincode.Substring(0, 3);
                var thirdsup = maincode.Substring(5);
                maincode = firstsup + "01" + thirdsup;
            }
            return maincode;
        }

        public async Task<string> GetNewMain_5_Async(string id)
        {
            var lastRecord = await _Context.MedicalAnalysisMains.OrderBy(obj => obj.MainCode)
                    .LastOrDefaultAsync(obj => obj.CurrentLevel == 5 && obj.MainCode.Substring(0, 5) == id.Substring(0, 5));

            string maincode = "";
            if (lastRecord != null)
            {
                maincode = lastRecord.MainCode;
                var firstsup = maincode.Substring(0, 5);
                var secondsup = maincode.Substring(5, 2);
                var thirdsup = maincode.Substring(7);

                var value = int.Parse(secondsup) + 1;
                if (value < 10)
                    secondsup = $"0{value}";
                else
                    secondsup = $"{value}";

                maincode = firstsup + secondsup + thirdsup; }
            else
            {
                maincode = id;
                var firstsup = maincode.Substring(0, 5);
                var thirdsup = maincode.Substring(7);
                maincode = firstsup + "01" + thirdsup;
            }
            return maincode;
        }

        public async Task<string> GetNewMain_6_Async(string id)
        {
            var lastRecord = await _Context.MedicalAnalysisMains.OrderBy(obj => obj.MainCode)
                    .LastOrDefaultAsync(obj => obj.CurrentLevel == 6 && obj.MainCode.Substring(0, 7) == id.Substring(0, 7));

            string maincode = "";
            if (lastRecord != null)
            {
                 maincode = lastRecord.MainCode;
                var firstsup = maincode.Substring(0, 7);
                var secondsup = maincode.Substring(7);
                var value = int.Parse(secondsup) + 1;
                if (value < 10)
                    secondsup = $"0{value}";
                else
                    secondsup = $"{value}";

                maincode = firstsup + secondsup;
            }
            else
            {
                 maincode = id;
                var firstsup = maincode.Substring(0, 7);
                maincode = firstsup + "01";
            }
            return maincode;
        }

        //////////////////////////////////////////////////
        // Add
        public async Task AddMainLevel_3_Async(MainEditAddDTO mainDTO)
        {
            MedicalAnalysisMain newMain = _mapper.Map<MedicalAnalysisMain>(mainDTO);
            newMain.CoCode = 0;
            newMain.ParentCode = "100000000";
            newMain.CurrentLevel = 3;
            newMain.MedicalFlag = 1;

            await _Context.AddAsync(newMain);
            await _Context.SaveChangesAsync();
        }

        public async Task AddMainLevel_4_Async(string Level_3_Code, MainEditAddDTO mainDTO)
        {
            MedicalAnalysisMain newMain = _mapper.Map<MedicalAnalysisMain>(mainDTO);
            newMain.CoCode = 0;
            newMain.ParentCode = Level_3_Code;
            newMain.CurrentLevel = 4;
            newMain.MedicalFlag = 1;

            await _Context.AddAsync(newMain);
            await _Context.SaveChangesAsync();
        }

        public async Task AddMainLevel_5_Async(string Level_4_Code, MainEditAddDTO mainDTO)
        {
            MedicalAnalysisMain newMain = _mapper.Map<MedicalAnalysisMain>(mainDTO);
            newMain.CoCode = 0;
            newMain.ParentCode = Level_4_Code;
            newMain.CurrentLevel = 5;
            newMain.MedicalFlag = 1;

            await _Context.AddAsync(newMain);
            await _Context.SaveChangesAsync();
        }

        public async Task AddMainLevel_6_Async(string Level_5_Code, MainEditAddDTO mainDTO)
        {
            MedicalAnalysisMain newMain = _mapper.Map<MedicalAnalysisMain>(mainDTO);
            newMain.CoCode = 0;
            newMain.ParentCode = Level_5_Code;
            newMain.CurrentLevel = 6;
            newMain.MedicalFlag = 1;

            await _Context.AddAsync(newMain);
            await _Context.SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Edit
        public async Task EditMainLevelAsync(string code, MainEditAddDTO mainDTO)
        {
            MedicalAnalysisMain existingMain = await _Context.MedicalAnalysisMains
                .FirstOrDefaultAsync(obj => obj.MainCode == code);

            _mapper.Map(mainDTO, existingMain);

            _Context.Update(existingMain);
            await _Context.SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Delete
        public async Task DeleteMainLevelAsync(string code)
        {
            MedicalAnalysisMain existingMain = await _Context.MedicalAnalysisMains
                .FirstOrDefaultAsync(obj => obj.MainCode == code);

            _Context.Remove(existingMain);
            await _Context.SaveChangesAsync();
        }
    }
}
