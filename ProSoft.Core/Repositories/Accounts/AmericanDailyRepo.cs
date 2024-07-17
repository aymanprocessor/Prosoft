using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class AmericanDailyRepo :IAmericanDailyRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public AmericanDailyRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<ReportAmericanDailyDTO> GetAllDataAsync()
        {
            ReportAmericanDailyDTO reportAmericanDailyDTO = new ReportAmericanDailyDTO();
            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            reportAmericanDailyDTO.branchs = _mapper.Map<List<BranchDTO>>(branches);
            reportAmericanDailyDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);

            return reportAmericanDailyDTO;
        }

        public async Task<List<AmericanDailyDTO>> GetAmericanDailyAsync(int branch, int journal, DateTime? fromPeriod, DateTime? toPeriod)
        {
            List<AmericanDailyDTO> americanDailyDTOs = new List<AmericanDailyDTO>();
            var mainCodes = await _Context.AccTransDetails
                    .Where(obj => (branch == 100 || obj.CoCode == branch) &&(journal == 100 || obj.TransType == journal.ToString()))
                    .Select(obj => obj.MainCode)
                    .Distinct()
                    .ToListAsync();
            // Use Task.WhenAll to parallelize fetching of MainNames
            var mainNamesTasks = mainCodes.Select(item => GetMainName(item));
            var mainNamesArray = await Task.WhenAll(mainNamesTasks);
            //list th
            var mainNames = mainNamesArray.ToList();

            return americanDailyDTOs;
        }

        public async Task<List<string>> GetMainNameAsync(int branch, int journal, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var mainCodes = await _Context.AccTransDetails
                   .Where(obj => (branch == 100 || obj.CoCode == branch) && (journal == 100 || obj.TransType == journal.ToString()))
                   .Select(obj => obj.MainCode)
                   .Distinct()
                   .ToListAsync();

            var mainNames = new List<string>();

            foreach (var item in mainCodes)
            {
                var mainName = await GetMainName(item);
                mainNames.Add(mainName);
            }

            return mainNames;
        }


        //Get main Name
        private async Task<string> GetMainName(string mainCode)
        {
            var accMainCode = await _Context.AccMainCodes
                                           .FirstOrDefaultAsync(cc => cc.MainCode == mainCode);
            return accMainCode?.MainName ?? ""; // Return "" if no matching cost center is found
        }
    }
}
