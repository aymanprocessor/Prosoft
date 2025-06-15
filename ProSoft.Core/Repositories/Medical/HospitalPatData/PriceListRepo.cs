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

        public async Task AddBatchPriceListsAsync(IEnumerable<PriceListEditAddDTO> priceListDTOs)
        {
            var priceLists = _mapper.Map<List<PriceList>>(priceListDTOs);
            await _Context.AddRangeAsync(priceLists);
            await _Context.SaveChangesAsync();
        }
        public async Task<PriceListEditAddDTO> GetPriceListByIdAsync(int id)
        {
            PriceList priceList = await _Context.PriceLists.FirstOrDefaultAsync(obj=>obj.PLId == id);
            PriceListEditAddDTO priceListDTO = _mapper.Map<PriceListEditAddDTO>(priceList);
            return priceListDTO;
        }

        public async Task EditPriceListAsync(int id, PriceListEditAddDTO priceListDTO)
        {
            PriceList priceList = await _Context.PriceLists.FirstOrDefaultAsync(obj => obj.PLId == id);
            _mapper.Map(priceListDTO, priceList);
            _Context.Update(priceList);
            await _Context.SaveChangesAsync();
            
        }

        public async Task EditPriceListBatchAsync(IEnumerable<PriceListEditAddDTO> priceListDTOs)
        {
            var ids = priceListDTOs.Select(dto => dto.PLId).ToList();

            // Fetch all matching records in one query
            var priceLists = await _Context.PriceLists
                .Where(pl => ids.Contains(pl.PLId))
                .ToListAsync();

            foreach (var dto in priceListDTOs)
            {
                var entity = priceLists.FirstOrDefault(pl => pl.PLId == dto.PLId);
                if (entity != null)
                {
                    _mapper.Map(dto, entity);
                }
            }

            // Save all changes in a single DB call
            await _Context.SaveChangesAsync();
        }

    }
}
