using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using System.Reflection;

namespace ProSoft.Core.Repositories.Shared
{
    public class EisPostingRepo: Repository<EisPosting, int>, IEisPostingRepo
    {
        private readonly IMapper _mapper;
        public EisPostingRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<EisPostingViewDTO>> GetAllCodesAsync()
        {
            List<EisPosting> eisPostings = await _DbSet.ToListAsync();
            var eisPostingsDTO = _mapper.Map<List<EisPostingViewDTO>>(eisPostings);
            
            foreach (var item in eisPostingsDTO)
                item.CodeDesc = await GetCodeDesc(item.MainCode, item.SubCode);

            return eisPostingsDTO;
        }

        public async Task<EisPostingEditAddDTO> GetEisPostingByIdAsync(int id)
        {
            EisPosting eisPosting = await _DbSet.FindAsync(id);
            var eisPostingDTO = _mapper.Map<EisPostingEditAddDTO>(eisPosting);

            List<AccMainCode> mainCodes = await _Context.AccMainCodes.ToListAsync();
            List<AccSubCode> subCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == eisPostingDTO.MainCode).ToListAsync();

            eisPostingDTO.MainCodes = _mapper.Map<List<AccMainCodeDTO>>(mainCodes);
            eisPostingDTO.SubCodes = _mapper.Map<List<AccSubCodeDTO>>(subCodes);
            return eisPostingDTO;
        }

        private async Task<string> GetCodeDesc(string mainCode, string subCode)
        {
            AccMainCode myMainCode = await _Context.AccMainCodes
                .FirstOrDefaultAsync(obj => obj.MainCode == mainCode);
            string mainName = myMainCode! != null ? myMainCode.MainName : string.Empty;
            
            AccSubCode mySubCode = await _Context.AccSubCodes
                .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == mainCode);
            string subName = mySubCode! != null ? mySubCode.SubName : string.Empty;
            
            return subName != "" ? $"{mainName} / {subName}" : mainName;
        }

        public async Task<EisPostingEditAddDTO> GetEmptyEisPostingAsync()
        {
            var eisPostingDTO = new EisPostingEditAddDTO();
            List<AccMainCode> mainCodes = await _Context.AccMainCodes.ToListAsync();
            eisPostingDTO.MainCodes = _mapper.Map<List<AccMainCodeDTO>>(mainCodes);

            return eisPostingDTO;
        }

        public async Task<List<AccSubCodeDTO>> GetSubCodesForAsync(string mainCode)
        {
            List<AccSubCode> subCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == mainCode).ToListAsync();
            var subCodesDTO = _mapper.Map<List<AccSubCodeDTO>>(subCodes);
            return subCodesDTO;
        }
    }
}
