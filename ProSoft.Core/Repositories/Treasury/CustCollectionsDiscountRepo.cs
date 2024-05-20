using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Treasury
{
    public class CustCollectionsDiscountRepo : Repository<CustCollectionsDiscount, int>, ICustCollectionsDiscountRepo
    {
        private readonly IMapper _mapper;
        public CustCollectionsDiscountRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }
        public async Task<List<CustCollectionsDiscountViewDTO>> GetAllCustCollectionsDiscountAsync()
        {
            List<CustCollectionsDiscount> custCollectionsDiscounts = await _DbSet.ToListAsync();
            var custCollectionsDiscountDTOs = _mapper.Map<List<CustCollectionsDiscountViewDTO>>(custCollectionsDiscounts);

            foreach (var item in custCollectionsDiscountDTOs)
                item.CodeDesc = await GetCodeDesc(item.MainCode, item.SubCode);

            return custCollectionsDiscountDTOs;
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
    }
}
