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
    public class ReportDailyAssistanceRepo: IReportDailyAssistanceRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public ReportDailyAssistanceRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<ReportDailyAssistanceDTO> GetAllDataAsync()
        {
            ReportDailyAssistanceDTO reportDailyAssistanceDTO = new ReportDailyAssistanceDTO();
            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            reportDailyAssistanceDTO.branchs = _mapper.Map<List<BranchDTO>>(branches);
            reportDailyAssistanceDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);

            return reportDailyAssistanceDTO;
        }
        public async Task<List<DailyAssistanceDTO>> GetDailyAssistanceAsync(int journal, DateTime fromPeriod, DateTime toPeriod, int branche,int fYear)
        {
            List<DailyAssistanceDTO> dailyAssistanceDTOs = new List<DailyAssistanceDTO>();

            var accTransMasterss = await _Context.AccTransMasters.Where(obj => obj.FYear == fYear &&
                    (journal == 100 || obj.TransType == journal.ToString()) && (branche == 100 || obj.CoCode == branche) &&
                    (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod))
                  .ToListAsync();

            foreach(var master in accTransMasterss)
            {
                DailyAssistanceDTO dailyAssistanceDTO = new DailyAssistanceDTO();
                var transactionList = await _Context.AccTransDetails.Where(obj => obj.TransId == master.TransId).ToListAsync();
                dailyAssistanceDTO.TransNo = master.TransNo;
                dailyAssistanceDTO.TransDate = master.TransDate;
                dailyAssistanceDTO.CurenncyName = "جنية مصري";
                dailyAssistanceDTO.TotalTrans = transactionList.Sum(obj=>obj.ValDep);

                dailyAssistanceDTO.DetailsDailyAssistanceDTOs = new List<DetailsDailyAssistanceDTO>();

                var accTransDetails =await _Context.AccTransDetails.Where(obj=>obj.TransId ==master.TransId).ToListAsync();
                foreach (var detail in accTransDetails)
                {
                    DetailsDailyAssistanceDTO detailsDailyAssistanceDTO = new DetailsDailyAssistanceDTO();
                    detailsDailyAssistanceDTO.MainName = await GetMainName(detail.MainCode);
                    detailsDailyAssistanceDTO.SubName= await GetSubName(detail.SubCode);
                    detailsDailyAssistanceDTO.ValDep=detail.ValDep;
                    detailsDailyAssistanceDTO.ValCredit=detail.ValCredit;
                    detailsDailyAssistanceDTO.ValDepCur=detail.ValDepCur;
                    detailsDailyAssistanceDTO.ValCreditCur=detail.ValDepCur;
                    detailsDailyAssistanceDTO.DocStatus=detail.DocStatus;
                    detailsDailyAssistanceDTO.DocNo=detail.DocNo;
                    detailsDailyAssistanceDTO.DocDate=detail.DocDate;
                    detailsDailyAssistanceDTO.LineDesc=detail.LineDesc;

                    dailyAssistanceDTO.DetailsDailyAssistanceDTOs.Add(detailsDailyAssistanceDTO);
                }
                dailyAssistanceDTOs.Add(dailyAssistanceDTO);
            }
            
            return dailyAssistanceDTOs;
        }       
        
        //Get main Name
        private async Task<string> GetMainName(string mainCode)
        {
            var accMainCode = await _Context.AccMainCodes
                                           .FirstOrDefaultAsync(cc => cc.MainCode == mainCode);
            return accMainCode?.MainName ?? ""; // Return "" if no matching cost center is found
        }
        //Get main Name
        private async Task<string> GetSubName(string subCode)
        {
            var accSubCode = await _Context.AccSubCodes
                                           .FirstOrDefaultAsync(cc => cc.SubCode == subCode);
            return accSubCode?.SubName ?? ""; // Return "" if no matching cost center is found
        }
    }
}
