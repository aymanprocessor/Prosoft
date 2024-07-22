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

        public async Task<List<GeneralProfessorFacilityDTO>> GetGeneralProfessorAsync(int branch, DateTime? toPeriod, int movementToDate)
        {
            List<GeneralProfessorFacilityDTO> eneralProfessorFacilityDTO = new List<GeneralProfessorFacilityDTO>();
            var lsAccType = "BAL";
            var llMounth = toPeriod?.Month;
            var liYear = toPeriod?.Year;
            var llDay = toPeriod?.Day;
            var ldDatePrevious = toPeriod.Value.AddDays(-llDay.Value);
            var ldtDatePrevious = ldDatePrevious;
            var dsMeazn =await _Context.AccBalAlls.Where(obj=>obj.DocType == lsAccType).ToListAsync();
            foreach (var item in dsMeazn)
            {
                var checkValue = 0;
                if (item.LineType == "tot")
                {
                  //  var lcTotalBalDep = lcTotalBalDep +item.
                }
                else if (item.LineType == "mn")
                {
                    var lsMainCode = item.MainCode;
                    var llPos = item.MainCode.IndexOf("00");
                    switch (llPos) 
                    {
                      //  case 2:
                    }
                }
               
            }

            //get first balance
            var accStartBals = await _Context.AccStartBals
                   .Where(obj => (branch == 100 || obj.CoCode == branch))
                     .ToListAsync();
            decimal lcFCreditOr = accStartBals.Sum(obj => obj.FCreditOr) ?? 0;
            decimal lcFDepOr = accStartBals.Sum(obj => obj.FDepOr) ?? 0;

            //var lsStartDate = new DateTime(fromPeriod.Value.Year, 1, 1);
            //balance before period
            //var accTransDetailList = await _Context.AccTransDetails
            //         .Where(obj => obj.FYear == fYear && (branch == 100 || obj.CoCode == branch)
            //          && (obj.TransDate >= lsStartDate && obj.TransDate < fromPeriod))
            //          .ToListAsync();

            //decimal lcGapValCredit = accTransDetailList.Sum(obj => obj.ValCredit) ?? 0;
            //decimal lcGapValDep = accTransDetailList.Sum(obj => obj.ValDep) ?? 0;

            return eneralProfessorFacilityDTO;
        }
    }
}
