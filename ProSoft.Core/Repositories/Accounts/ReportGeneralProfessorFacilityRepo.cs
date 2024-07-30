using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class ReportGeneralProfessorFacilityRepo: IReportGeneralProfessorFacilityRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public ReportGeneralProfessorFacilityRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<ReportGeneralProfessorFacilityDTO> GetAllDataAsync()
        {
            ReportGeneralProfessorFacilityDTO reportGeneralProfessorFacilityDTO = new ReportGeneralProfessorFacilityDTO();
            List<Branch> branches = await _Context.Branchs.ToListAsync();
            reportGeneralProfessorFacilityDTO.branchs = _mapper.Map<List<BranchDTO>>(branches);

            return reportGeneralProfessorFacilityDTO;
        }
        public async Task<List<GeneralProfessorFacilityDTO>> GetGeneralProfessorAsync(int branch, DateTime toPeriod)
        {
            int year = toPeriod.Year;
            DateTime startDate = new DateTime(year, 1, 31); // Start date for the filter

            var query = from sub in _Context.AccSubCodes
                        join startBal in _Context.AccStartBals
                            on new { sub.MainCode, sub.SubCode } equals new { startBal.MainCode, startBal.SubCode } into startBalGroup
                        from startBal in startBalGroup.DefaultIfEmpty()
                        join transDetail in _Context.AccTransDetails
                            on new { sub.MainCode, sub.SubCode, FYear = startBal != null ? startBal.FYear : (int?)null }
                            equals new { transDetail.MainCode, transDetail.SubCode, transDetail.FYear } into transDetailGroup
                        from transDetail in transDetailGroup.DefaultIfEmpty()
                        join main in _Context.AccMainCodes
                            on new { sub.MainCode, sub.CoCode } equals new { main.MainCode, main.CoCode }
                        where (startBal == null || startBal.FYear == year) // Handle the case where startBal is null
                              && (transDetail == null || (transDetail.TransDate >= startDate && transDetail.TransDate <= toPeriod))// && transDetail.TransDate <= toPeriod // Handle the case where transDetail is null
                              && (transDetail == null || transDetail.FYear == year)
                        group new { startBal, transDetail, sub, main } by new
                        {
                            sub.MainCode,
                            sub.SubCode,
                            sub.SubName,
                            main.MainName
                        } into g
                        select new GeneralProfessorFacilityDTO
                        {
                            MainCode = g.Key.MainCode,
                            SubCode = g.Key.SubCode,
                            SubName = g.Key.SubName,
                            FDepCur = g.Sum(x => x.startBal != null ? x.startBal.FDepCur : 0),
                            FCreditOr = g.Sum(x => x.startBal != null ? x.startBal.FCreditOr : 0),
                            ValDep = g.Sum(x => x.transDetail != null ? x.transDetail.ValDep : 0),
                            ValCredit = g.Sum(x => x.transDetail != null ? x.transDetail.ValCredit : 0),
                            MainName = g.Key.MainName
                        };

            return await query.OrderBy(x => x.MainCode)
                              .ThenBy(x => x.SubCode)
                              .ToListAsync();
        }

        //public async Task<List<GeneralProfessorFacilityDTO>> GetGeneralProfessorAsync(int branch, DateTime toPeriod, int? movementToDate)
        //{
        //    int year = toPeriod.Year;
        //    DateTime startDate = new DateTime(year, 1, 1); // Start date for the filter

        //    // Get relevant start balances
        //    var startBalances = _Context.AccStartBals
        //        .Where(sb => sb.FYear == year)
        //        .ToList();

        //    // Get relevant transaction details
        //    var transDetails = _Context.AccTransDetails
        //        .Where(td => td.FYear == year && (td.TransDate >= startDate && td.TransDate <= toPeriod))
        //        .ToList();

        //    // Get all relevant sub codes and their related main codes
        //    var subCodes = _Context.AccSubCodes.ToList();

        //    var mainCodes = _Context.AccMainCodes.ToList();

        //    var query = from sub in subCodes
        //                join startBal in startBalances
        //                    on new { sub.MainCode, sub.SubCode } equals new { startBal.MainCode, startBal.SubCode } into startBalGroup
        //                from startBal in startBalGroup.DefaultIfEmpty()
        //                join transDetail in transDetails
        //                    on new { sub.MainCode, sub.SubCode, FYear = startBal?.FYear } equals new { transDetail.MainCode, transDetail.SubCode, transDetail.FYear } into transDetailGroup
        //                from transDetail in transDetailGroup.DefaultIfEmpty()
        //                join main in mainCodes
        //                    on new { sub.MainCode, sub.CoCode } equals new { main.MainCode, main.CoCode }
        //                group new { startBal, transDetail, sub, main } by new
        //                {
        //                    sub.MainCode,
        //                    sub.SubCode,
        //                    sub.SubName,
        //                    main.MainName
        //                } into g
        //                select new GeneralProfessorFacilityDTO
        //                {
        //                    MainCode = g.Key.MainCode,
        //                    SubCode = g.Key.SubCode,
        //                    SubName = g.Key.SubName,
        //                    FDepCur = g.Sum(x => x.startBal != null ? x.startBal.FDepCur : 0),
        //                    FCreditOr = g.Sum(x => x.startBal != null ? x.startBal.FCreditOr : 0),
        //                    ValDep = g.Sum(x => x.transDetail != null ? x.transDetail.ValDep : 0),
        //                    ValCredit = g.Sum(x => x.transDetail != null ? x.transDetail.ValCredit : 0),
        //                    MainName = g.Key.MainName
        //                };

        //    return  query.OrderBy(x => x.MainCode)
        //                      .ThenBy(x => x.SubCode)
        //                      .ToList();
        //}
    }
}
