using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
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
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes.Where(main => _Context.AccSubCodes.Any(sub => sub.MainCode == main.MainCode)).ToListAsync();
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.ToListAsync();
            reportAmericanDailyDTO.branchs = _mapper.Map<List<BranchDTO>>(branches);
            reportAmericanDailyDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            reportAmericanDailyDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);
            reportAmericanDailyDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);

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

            foreach (var g in accTransDetails.GroupBy(t => t.TransNo)) // transno بجمع السطور بناء علي ال
            {
                var dto = new AmericanDailyDTO
                {
                    TransNo = g.Key,
                    TransDate = g.First().TransDate, //في السطور transdate بجيب اول
                    LineDesc = g.First().LineDesc, //في السطور LineDesc بجيب اول
                    MainCodeValues = new List<Tuple<string, decimal?, decimal?>>() // وبعد كده اضيف foreach وهملي في ال new object بعمل
                };

                foreach (var mainName in mainNames)
                {
                    var mainCode = await GetMainCode(mainName);
                    var transactions = g.Where(t => t.MainCode == mainCode).ToList();
                    //بجمع ال depit and Credit كل واحد لوحده
                    decimal? valDepSum = transactions.Sum(t => t.ValDep);
                    decimal? valCreditSum = transactions.Sum(t => t.ValCredit);

                    dto.MainCodeValues.Add(new Tuple<string, decimal?, decimal?>(mainName, valDepSum, valCreditSum));
                }

                groupedTrans.Add(dto);
            }

            return groupedTrans;
        }

        //all main code
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
       
        //Get main Code
        private async Task<string> GetMainCode(string mainName)
        {
            var accMainCode = await _Context.AccMainCodes
                                           .FirstOrDefaultAsync(cc => cc.MainName == mainName);
            return accMainCode?.MainCode ?? ""; // Return "" if no matching cost center is found
        }

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        public async Task<List<AmericanDailyDTO>> GetAmericanDailyForMainSubAsync(int branch, int journal, DateTime? fromPeriod, DateTime? toPeriod, string? mainCodeFilter, string? subCodeFilter)
        {
            var accTransDetails = await _Context.AccTransDetails
                .Where(obj => (branch == 100 || obj.CoCode == branch) &&
                              (journal == 100 || obj.TransType == journal.ToString()) &&
                              (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod) &&
                              obj.MainCode == mainCodeFilter &&
                              (string.IsNullOrEmpty(subCodeFilter) || obj.SubCode == subCodeFilter))
                .ToListAsync();

            var subNames = await GetSubNameAsync(branch, journal, fromPeriod, toPeriod, mainCodeFilter, subCodeFilter);

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

                foreach (var subName in subNames)
                {
                    var subCode = await GetSubCode(subName);
                    var transactions = g.Where(t => t.SubCode == subCode).ToList();
                    decimal? valDepSum = transactions.Sum(t => t.ValDep);
                    decimal? valCreditSum = transactions.Sum(t => t.ValCredit);

                    dto.MainCodeValues.Add(new Tuple<string, decimal?, decimal?>(subName, valDepSum, valCreditSum));
                }

                groupedTrans.Add(dto);
            }

            return groupedTrans;
        }

        public async Task<List<string>> GetSubNameAsync(int branch, int journal, DateTime? fromPeriod, DateTime? toPeriod, string? mainCodeFilter, string? subCodeFilter)
        {
            var subCodes = await _Context.AccTransDetails
                   .Where(obj => (branch == 100 || obj.CoCode == branch) &&
                                 (journal == 100 || obj.TransType == journal.ToString()) &&
                                 (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod) &&
                                 obj.MainCode == mainCodeFilter &&
                                 (string.IsNullOrEmpty(subCodeFilter) || obj.SubCode == subCodeFilter))
                   .Select(obj => new { obj.SubCode, obj.MainCode })
                   .Distinct()
                   .ToListAsync();

            var subNames = new List<string>();

            foreach (var item in subCodes)
            {
                var subName = await GetSubName(item.SubCode, item.MainCode);
                subNames.Add(subName);
            }

            return subNames;
        }

        private async Task<string> GetSubName(string subCode, string mainCode)
        {
            var accSubCode = await _Context.AccSubCodes
                                           .FirstOrDefaultAsync(cc => cc.SubCode == subCode && cc.MainCode == mainCode);
            return accSubCode?.SubName ?? ""; // Return "" if no matching sub code is found
        }

        private async Task<string> GetSubCode(string subName)
        {
            var accSubCode = await _Context.AccSubCodes
                                           .FirstOrDefaultAsync(cc => cc.SubName == subName);
            return accSubCode?.SubCode ?? ""; // Return "" if no matching sub code is found
        }

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        public async Task<List<AmericanTotalJournalDTO>> GetAmericanTotalJournal(int journal, int branch, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var americanTotalJournalDTOs = new List<AmericanTotalJournalDTO>();

            var accTransDetails = await _Context.AccTransDetails
                .Where(obj => (journal == 100 || obj.TransType == journal.ToString()) &&
                              (branch == 100 || obj.CoCode == branch) &&
                              (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod))
                .OrderBy(obj => obj.MainCode)
                .ToListAsync();

            var groupedTransDetails = accTransDetails
                .GroupBy(obj => obj.MainCode)
                .Select(g => new
                {
                    MainCode = g.Key,
                    TotalValDep = g.Sum(obj => obj.ValDep),
                    TotalValCredit = g.Sum(obj => obj.ValCredit)
                });

            foreach (var g in groupedTransDetails)
            {
                var dto = new AmericanTotalJournalDTO
                {
                    MainCode = g.MainCode,
                    MainName = await GetMainName(g.MainCode),
                    ValDep = g.TotalValDep,
                    ValCredit = g.TotalValCredit
                };
                americanTotalJournalDTOs.Add(dto);
            }

            return americanTotalJournalDTOs;
        }

    }
}
