using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
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
    public class MonthlyClosingRepo : IMonthlyClosingRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public MonthlyClosingRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<MonthlyClosingDTO> GetAllDataAsync()
        {
            MonthlyClosingDTO monthlyClosingDTO = new MonthlyClosingDTO();
            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            monthlyClosingDTO.branchs = _mapper.Map<List<BranchDTO>>(branches);
            monthlyClosingDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);

            return monthlyClosingDTO;
        }

        public async Task<string> CloseAsync(int journal, int branch, int? fromVoucher, int? toVoucher, DateTime? fromPeriod, DateTime? toPeriod)
        {
            if (fromVoucher != null && toVoucher != null)
            {
                List<AccTransMaster> accTransMasters = await _Context.AccTransMasters.Where(obj => (branch == 100 || obj.CoCode == branch) && (journal == 100 || obj.TransType == journal.ToString())
                        && (obj.TransNo >= fromVoucher && obj.TransNo <= toVoucher)).ToListAsync();
               
                foreach (var master in accTransMasters)//accTransMasters.ForEach(master => master.DocStatus = 0);
                {
                    master.OkPost = "1";
                }

                await _Context.SaveChangesAsync();
            }
            else if (fromPeriod != null && toPeriod != null)
            {
                List<AccTransMaster> accTransMasters = await _Context.AccTransMasters.Where(obj => (branch == 100 || obj.CoCode == branch) && (journal == 100 || obj.TransType == journal.ToString())
                       && (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod)).ToListAsync();

                foreach (var master in accTransMasters)
                {
                    master.OkPost = "1";
                }

                await _Context.SaveChangesAsync();
            }
            return "Monthly Closing Done";
        }
        public async Task<string> CancelAsync(int journal, int branch, int? fromVoucher, int? toVoucher, DateTime? fromPeriod, DateTime? toPeriod)
        {
            if (fromVoucher != null && toVoucher != null)
            {
                List<AccTransMaster> accTransMasters = await _Context.AccTransMasters.Where(obj => (branch == 100 || obj.CoCode == branch) && (journal == 100 || obj.TransType == journal.ToString())
                        && (obj.TransNo >= fromVoucher && obj.TransNo <= toVoucher)).ToListAsync();

                foreach (var master in accTransMasters)//accTransMasters.ForEach(master => master.DocStatus = 0);
                {
                    master.OkPost = "0";
                }

                await _Context.SaveChangesAsync();
            }
            else if (fromPeriod != null && toPeriod != null)
            {
                List<AccTransMaster> accTransMasters = await _Context.AccTransMasters.Where(obj => (branch == 100 || obj.CoCode == branch) && (journal == 100 || obj.TransType == journal.ToString())
                       && (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod)).ToListAsync();

                foreach (var master in accTransMasters)
                {
                    master.OkPost = "0";
                }

                await _Context.SaveChangesAsync();
            }
            return "Monthly Closing Cancel";
        }

    }
}
