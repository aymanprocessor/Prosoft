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
    public class CancelJournalVoucherRepo: ICancelJournalVoucherRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public CancelJournalVoucherRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<CancelJournalVoucherDTO> GetAllDataAsync()
        {
            CancelJournalVoucherDTO cancelJournalVoucherDTO = new CancelJournalVoucherDTO();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            List<AccMonth> accMonths = await _Context.AccMonths.ToListAsync();
            cancelJournalVoucherDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            cancelJournalVoucherDTO.AccMonths = _mapper.Map<List<AccMonth>>(accMonths);

            return cancelJournalVoucherDTO;
        }
        public async Task<string> CancelAsync(int journal, int fromVoucher, int toVoucher, int year, int mounth,int branch)
        {
            List<AccTransMaster> accTransMasters = await _Context.AccTransMasters.Where(obj => (branch == 100 || obj.CoCode == branch) && (journal == 100 || obj.TransType == journal.ToString())
               && obj.FYear == year && obj.FMonth == mounth && (obj.TransNo >= fromVoucher && obj.TransNo <= toVoucher)).ToListAsync();
            List<AccTransDetail> accTransDetails = await _Context.AccTransDetails.Where(obj => obj.CoCode == branch && obj.TransType == journal.ToString()
               && obj.FYear == year && obj.FMonth == mounth && (obj.TransNo >= fromVoucher && obj.TransNo <= toVoucher)).ToListAsync();

            foreach (var master in accTransMasters)//accTransMasters.ForEach(master => master.DocStatus = 0);
            {
                master.DocStatus = 0;
            }
            foreach (var detail in accTransDetails)
            {
                detail.DocStatus = 0;
            }
            await _Context.SaveChangesAsync();

            return "Journal Voucher have been cancelled";
        }
        public async Task<string> RetrievedAsync(int journal, int fromVoucher, int toVoucher, int year, int mounth, int branch)
        {
            List<AccTransMaster> accTransMasters = await _Context.AccTransMasters.Where(obj => (branch == 100 || obj.CoCode == branch) && (journal == 100 || obj.TransType == journal.ToString())
                && obj.FYear == year && obj.FMonth == mounth && (obj.TransNo >= fromVoucher && obj.TransNo <= toVoucher)).ToListAsync();
            List<AccTransDetail> accTransDetails = await _Context.AccTransDetails.Where(obj => obj.CoCode == branch && obj.TransType == journal.ToString()
               && obj.FYear == year && obj.FMonth == mounth && (obj.TransNo >= fromVoucher && obj.TransNo <= toVoucher)).ToListAsync();
            
            foreach (var master in accTransMasters)//accTransMasters.ForEach(master => master.DocStatus = 0);
            {
                master.DocStatus = 1;
            }
            foreach (var detail in accTransDetails)
            {
                detail.DocStatus = 1;
            }
            await _Context.SaveChangesAsync();

            return "Journal Voucher have been retrieved";
        }
    }
}
