using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class PriceListRepo : Repository<PriceList, int>, IPriceListRepo
    {
        private readonly IMapper _mapper;

        public PriceListRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;

        }

        public async Task<List<PriceListViewDTO>> GetAllPriceList()
        {
            List<PriceList> priceLists =await _Context.PriceLists.ToListAsync();
            List<PriceListViewDTO> priceListDTOs =_mapper.Map<List<PriceListViewDTO>>(priceLists); 
            return priceListDTOs;
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.PLId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
        public async Task AddPriceListAsync(PriceListEditAddDTO priceListDTO)
        {
            PriceList priceList = _mapper.Map<PriceList>(priceListDTO);
            await _Context.AddAsync(priceList);
            await _Context.SaveChangesAsync();
        }

    }
}
