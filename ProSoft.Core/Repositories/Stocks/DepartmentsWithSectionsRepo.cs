using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class DepartmentsWithSectionsRepo : Repository<Region, int>, IDepartmentsWithSectionsRepo
    {
        private readonly IMapper _mapper;
        public DepartmentsWithSectionsRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }
        public async Task<List<RegionsViewDTO>> GetDepartmentsWithSectionsAll(int id)
        {
            List<Region> regions = await _Context.Regions.Where(obj=>obj.Flag ==0 &&obj.ClassificationCust ==id).ToListAsync();
            List<RegionsViewDTO> regionsViewDTOs =new List<RegionsViewDTO>();
            
            foreach (var item in regions)
            {
                RegionsViewDTO regionsViewDTO = _mapper.Map<RegionsViewDTO>(item);
                var stock = await _Context.Stocks.FirstOrDefaultAsync(obj => obj.Stkcod == item.StockCode);
                regionsViewDTO.StockName = stock?.Stknam ?? "المخزن الرئيسي";

                //var section = await _Context.Sections2s.FirstOrDefaultAsync(obj => obj.SecCd == item.RegionCode);
                //regionsViewDTO.SectionName = section?.SecDesc ?? "";
                regionsViewDTOs.Add(regionsViewDTO);
            }
            return regionsViewDTOs;
        }
        public async Task<RegionsEditAddDTO> GetEmptyDepartmentsWithSections(int regionCode)
        {
            var regionsEditAddDTO = new RegionsEditAddDTO();

            List<Stock> stocks = await _Context.Stocks.ToListAsync();
            List<Sections2> sections2s = await _Context.Sections2s.ToListAsync();

            regionsEditAddDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);
            regionsEditAddDTO.Sections = _mapper.Map<List<SectionViewDTO>>(sections2s);
            return regionsEditAddDTO;
        }

        public async Task AddDepartmentsWithSectionsAsync(int id, RegionsEditAddDTO regionsEditAddDTO)
        {
           // var region = _mapper.Map<Region>(regionsEditAddDTO);
            var regionn = new Region();
            regionn.ClassificationCust = id;
            regionn.Flag = 0;
            regionn.RegionDesc = regionsEditAddDTO.RegionDesc;
            regionn.RegYear = regionsEditAddDTO.RegYear;
            regionn.OnOff = regionsEditAddDTO.OnOff;
            regionn.StockCode = regionsEditAddDTO.StockCode;
            regionn.SectionCode = regionsEditAddDTO.SectionCode;

            await _Context.AddAsync(regionn);
            await _Context.SaveChangesAsync();
        }
        public async Task<RegionsEditAddDTO> GetdepartmentsWithSectionsByIdAsync(int id)
        {
            var region = await _Context.Regions
                .FirstOrDefaultAsync(obj => obj.RegionCode == id);
            RegionsEditAddDTO regionsEditAddDTO = _mapper.Map<RegionsEditAddDTO>(region);

            List<Stock> stocks = await _Context.Stocks.ToListAsync();
            List<Sections2> sections2s = await _Context.Sections2s.ToListAsync();

            regionsEditAddDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);
            regionsEditAddDTO.Sections = _mapper.Map<List<SectionViewDTO>>(sections2s);
            return regionsEditAddDTO;
        }
        public async Task EditDepartmentsWithSectionsAsync(int id, RegionsEditAddDTO regionsEditAddDTO)
        {
            var region = await _Context.Regions
                .FirstOrDefaultAsync(obj => obj.RegionCode == id);

            _mapper.Map(regionsEditAddDTO, region);

            _Context.Update(region);
            await _Context.SaveChangesAsync();
        }
    }
}
