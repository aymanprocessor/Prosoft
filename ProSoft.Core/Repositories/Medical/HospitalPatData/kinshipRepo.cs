using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class kinshipRepo : Repository<GTable, int>, IkinshipRepo
    {
        private readonly IMapper _mapper;

        public kinshipRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }
        public async Task<List<GTablelDTO>> GetAllkinshipAsync()
        {
            List<GTable> gTables = await _Context.gTables.Where(obj => obj.Flag == 40).ToListAsync();
            List<GTablelDTO> gTablelDTOs = _mapper.Map<List<GTablelDTO>>(gTables);
            return gTablelDTOs;
        }
    }
}
