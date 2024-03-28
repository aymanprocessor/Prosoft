using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.IRepositories.Medical.Analysis;
using ProSoft.EF.Models.Medical.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.Analysis
{
    public class ItemAnalysisRepo : IItemAnalysisRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;

        public ItemAnalysisRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<List<ItemViewDTO>> GetItemsBySubAsync(string id ,string maincode)
        {
            List<Itemanalysis> itemanalysis = await _Context.Itemanalyses
                .Where(obj => obj.Codeanalcode == id && obj.Subanalcode == maincode).ToListAsync();

            List<ItemViewDTO> itemAnalysisDTO = _mapper.Map<List<ItemViewDTO>>(itemanalysis);
            return itemAnalysisDTO;
        }
        public async Task<ItemViewDTO> GetItemByIDsAsync(int itemcode, string subCode, string maincode)
        {
            Itemanalysis existingItemAnalysis=await _Context.Itemanalyses
                .FirstOrDefaultAsync(obj=>obj.Itemanalcode==itemcode && obj.Codeanalcode==subCode 
                && obj.Subanalcode==maincode);

            ItemViewDTO itemAnalysisDTO =_mapper.Map<ItemViewDTO>(existingItemAnalysis);
            return itemAnalysisDTO;
        }

        public async Task<int> GetNewItemAsync(string id, string maincode)
        {
            List<Itemanalysis> SubItemCodes = await _Context.Itemanalyses
                .Where(obj => obj.Codeanalcode == id && obj.Subanalcode == maincode).ToListAsync();
            int newItemCode =0;
            if (SubItemCodes.Count() != 0)
                newItemCode = SubItemCodes.Max(obj => obj.Itemanalcode) + 1;
            else
                newItemCode = 1;
            return newItemCode;

        }

        public async Task AddItemAnalysisAsync(string id, ItemEditAddDTO itemDTO)
        {
            Itemanalysis itemAnalysis = _mapper.Map<Itemanalysis>(itemDTO);

            await _Context.AddAsync(itemAnalysis);
            await _Context.SaveChangesAsync();
        }
        public async Task EditItemAnalysisAsync(int id, ItemEditAddDTO itemDTO)
        {
            Itemanalysis existingItemAnalysis = await _Context.Itemanalyses
                .FirstOrDefaultAsync(obj => obj.Itemanalcode == id && obj.Codeanalcode == itemDTO.Codeanalcode
                && obj.Subanalcode == itemDTO.Subanalcode);

            _mapper.Map(itemDTO, existingItemAnalysis);

            _Context.Update(existingItemAnalysis);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteItemAnalysisAsync(int id, string subcode, string maincode)
        {
            Itemanalysis existingItemAnalysis = await _Context.Itemanalyses
              .FirstOrDefaultAsync(obj => obj.Itemanalcode == id && obj.Codeanalcode == subcode
              && obj.Subanalcode == maincode);

            _Context.Remove(existingItemAnalysis);
            await _Context.SaveChangesAsync();
        }
    }
}
