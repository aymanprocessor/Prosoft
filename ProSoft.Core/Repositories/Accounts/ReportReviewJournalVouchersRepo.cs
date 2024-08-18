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
    public class ReportReviewJournalVouchersRepo : IReportReviewJournalVouchersRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public ReportReviewJournalVouchersRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<ReportReviewJournalVouchersDTO> GetAllDataAsync()
        {
            ReportReviewJournalVouchersDTO reportReviewJournalVouchersDTO = new ReportReviewJournalVouchersDTO();
            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            reportReviewJournalVouchersDTO.branchs = _mapper.Map<List<BranchDTO>>(branches);
            reportReviewJournalVouchersDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);

            return reportReviewJournalVouchersDTO;
        }
        public async Task<List<ReviewJournalVouchersDTO>> GetReviewDisplay(int journal, int branche, DateTime? fromPeriod, DateTime? toPeriod, int displayType)
        {
            var subCodes = await _Context.AccSubCodes.ToListAsync();

            var query = _Context.AccTransDetails
                .Where(obj =>
                    (journal == 100 || obj.TransType == journal.ToString()) &&
                    (branche == 100 || obj.CoCode == branche) &&
                    (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod));

            var groupedQuery = await query
                .GroupBy(obj => obj.TransNo)
                .ToListAsync();

            List<ReviewJournalVouchersDTO> reviewJournalVouchersDTOs = groupedQuery
                .Where(group =>
                    displayType == 1 ||
                    (displayType == 3 && (
                        group.Any(obj => obj.SubCode == null) ||
                        group.Any(obj => !subCodes.Any(sub => sub.MainCode == obj.MainCode && sub.SubCode == obj.SubCode))
                    )) ||
                    (displayType == 2 && group.Sum(obj => obj.ValDep) - group.Sum(obj => obj.ValCredit) != 0))
                .Select(group =>
                {
                    var mainCodeWithIssue = group.FirstOrDefault(obj => obj.SubCode == null) ??
                                             group.FirstOrDefault(obj => !subCodes.Any(sub => sub.MainCode == obj.MainCode && sub.SubCode == obj.SubCode));

                    return new ReviewJournalVouchersDTO
                    {
                        TransNo = group.Key,
                        TransDate = group.First().TransDate,
                        ValDep = group.Sum(obj => obj.ValDep),
                        ValCredit = group.Sum(obj => obj.ValCredit),
                        MainCode = mainCodeWithIssue?.MainCode, // إرجاع MainCode الذي لديه مشكلة
                        SubCode = mainCodeWithIssue?.SubCode,   // إرجاع SubCode الذي لديه مشكلة
                        LineDesc = mainCodeWithIssue?.LineDesc  // إرجاع LineDesc من أول عنصر في المجموعة
                    };
                })
                .ToList();
            return reviewJournalVouchersDTOs;
        }

        //public async Task<List<ReviewJournalVouchersDTO>> GetReviewDisplay(int journal, int branche, DateTime? fromPeriod, DateTime? toPeriod, int displayType)
        //{
        //    var subCodes = await _Context.AccSubCodes.ToListAsync();

        //    List<ReviewJournalVouchersDTO> reviewJournalVouchersDTOs = await _Context.AccTransDetails
        //        .Where(obj =>
        //            (journal == 100 || obj.TransType == journal.ToString()) &&
        //            (branche == 100 || obj.CoCode == branche) &&
        //            (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod))
        //        .GroupBy(obj => obj.TransNo)
        //        .Where(group => displayType == 1 ||
        //            ((displayType == 3 && group.Any(obj => subCodes.Any(sub => sub.MainCode == obj.MainCode && sub.SubCode == obj.SubCode))) ||
        //            (displayType == 3 && group.Any(obj => obj.SubCode == null))) ||
        //            (displayType == 2 && group.Sum(obj => obj.ValDep) - group.Sum(obj => obj.ValCredit) != 0))
        //        .Select(group => new ReviewJournalVouchersDTO
        //        {
        //            TransNo = group.Key,
        //            TransDate = group.First().TransDate,
        //            ValDep = group.Sum(obj => obj.ValDep),
        //            ValCredit = group.Sum(obj => obj.ValCredit)
        //        })
        //        .ToListAsync();

        //    return reviewJournalVouchersDTOs;
        //}
    }
}
