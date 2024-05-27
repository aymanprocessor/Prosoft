using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Treasury
{
    public class AccGlobalDefRepo : Repository<AccGlobalDef, int>, IAccGlobalDefRepo
    {
        private readonly IMapper _mapper;

        public AccGlobalDefRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<AccGlobalDefDTO>> GetAllCurrencyAsync()
        {
           List<AccGlobalDef> accGlobalDefs = await _Context.accGlobalDefs.Where(obj=>obj.TableCode =="CUR").ToListAsync();
            List<AccGlobalDefDTO> accGlobalDefDTOs = _mapper.Map<List<AccGlobalDefDTO>>(accGlobalDefs);
            return accGlobalDefDTOs;
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.CodeNo);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
    }
}
