using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class SubItemRepo : Repository<SubItem, int>, ISubItemRepo
    {
        private readonly IMapper _mapper;
        public SubItemRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<SubItemViewDTO>> GetSubItemsByMainIdAsync(int mainId)
        {
            List<SubItem> subItems = await _DbSet.Where(obj => obj.MainId == mainId).ToListAsync();
            var subItemsDTO = _mapper.Map<List<SubItemViewDTO>>(subItems);
            return subItemsDTO;
        }
    }
}
