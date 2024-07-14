using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Treasury
{
    public class GTableRepo : Repository<GTable, int>, IGTableRepo
    {
        private readonly IMapper _mapper;

        public GTableRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<GTablelDTO>> GetAllCostCenterTreasuryAsync(int flag)
        {
            List<GTable> gTables = await _Context.gTables.Where(obj => obj.Flag == flag).ToListAsync();
            List<GTablelDTO> gTablelDTOs = _mapper.Map<List<GTablelDTO>>(gTables);
            return gTablelDTOs;
        }
        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.GCode);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
    }
}
