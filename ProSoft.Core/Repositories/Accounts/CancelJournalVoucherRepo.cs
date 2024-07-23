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
    }
}
