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
            var accTransDetails = await _Context.AccTransDetails
                .Where(obj => (branch == 100 || obj.CoCode == branch) &&
                              (journal == 100 || obj.TransType == journal.ToString()) &&
                              (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod))
                .ToListAsync();

            var mainNames = await GetMainNameAsync(branch, journal, fromPeriod, toPeriod);

            var groupedTrans = new List<AmericanDailyDTO>();

            foreach (var g in accTransDetails.GroupBy(t => t.TransNo))
            {
                var dto = new AmericanDailyDTO
                {
                    TransNo = g.Key,
                    TransDate = g.First().TransDate,
                    LineDesc = g.First().LineDesc,
                    MainCodeValues = new List<Tuple<string, decimal?, decimal?>>()
                };

                foreach (var mainName in mainNames)
                {
                    var mainCode = await GetMainCode(mainName);
                    var transaction = g.FirstOrDefault(t => t.MainCode == mainCode);

                    dto.MainCodeValues.Add(new Tuple<string, decimal?, decimal?>(mainName,
                                                                                 transaction?.ValDep,
                                                                                 transaction?.ValCredit));
                }

                groupedTrans.Add(dto);
            }

            return groupedTrans;
        }
        public async Task<List<string>> GetMainCodeAsync(int branch, int journal, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var mainCodes = await _Context.AccTransDetails
                   .Where(obj => (branch == 100 || obj.CoCode == branch) && (journal == 100 || obj.TransType == journal.ToString()) &&
                      (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod))
                   .Select(obj => obj.MainCode)
                   .Distinct()
                   .ToListAsync();

            var mainNames = new List<string>();

            foreach (var item in mainCodes)
            {
                var mainName = await GetMainCode(item);
                mainNames.Add(mainName);
            }

            return mainNames;
        }


        //Get main Name
        private async Task<string> GetMainCode(string mainName)
        {
            var accMainCode = await _Context.AccMainCodes
                                           .FirstOrDefaultAsync(cc => cc.MainName == mainName);
            return accMainCode?.MainCode ?? ""; // Return "" if no matching cost center is found
        }

        public async Task<List<string>> GetMainNameAsync(int branch, int journal, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var mainCodes = await _Context.AccTransDetails
                   .Where(obj => (branch == 100 || obj.CoCode == branch) && (journal == 100 || obj.TransType == journal.ToString()) &&
                      (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod))
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
