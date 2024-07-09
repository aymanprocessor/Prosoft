using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class AccMainCodeRepo :IAccMainCodeRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;

        public AccMainCodeRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<List<AccMainCodeDTO>> GetMainsByLevelAsync(double level)
        {
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes
                .Where(obj => obj.CurrentLevel == level).ToListAsync();
            List<AccMainCodeDTO> mainLevelsDTO = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);
            return mainLevelsDTO;
        }
        public async Task<AccMainCodeDTO> GetMainByIdAsync(string id)
        {
            AccMainCode mainById = await _Context.AccMainCodes
                .FirstOrDefaultAsync(obj => obj.MainCode == id);

            AccMainCodeDTO mainByIdDTO = _mapper.Map<AccMainCodeDTO>(mainById);

            return mainByIdDTO;
        }
    }
}
