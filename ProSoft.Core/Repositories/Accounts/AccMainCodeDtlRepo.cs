using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class AccMainCodeDtlRepo : IAccMainCodeDtlRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;

        public AccMainCodeDtlRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<List<AccMainCodeDtlDTO>> GetAccMainCodeDtl(string id)
        {
            List<AccMainCodeDtl> accMainCodeDtls = await _Context.AccMainCodeDtls.Where(obj => obj.MainCode == id).ToListAsync();
            List<AccMainCodeDtlDTO> accMainCodeDtlDTOs = _mapper.Map<List<AccMainCodeDtlDTO>>(accMainCodeDtls);
            return accMainCodeDtlDTOs;
        }
        public async Task AddAccMainCodeDtlAsync(AccMainCodeDtlDTO accMainCodeDtlDTO)
        {
            var mainName =(await _Context.AccMainCodes.FirstOrDefaultAsync(obj=>obj.MainCode ==accMainCodeDtlDTO.SecCode)).MainName;
            accMainCodeDtlDTO.MainName =mainName;
            AccMainCodeDtl accMainCodeDtl = _mapper.Map<AccMainCodeDtl>(accMainCodeDtlDTO);
            //list i want to remove
            List<AccSubCode> accSubCodesWantUpdated =await _Context.AccSubCodes.Where(obj=>obj.MainCode == accMainCodeDtlDTO.SecCode).ToListAsync();
            _Context.AccSubCodes.RemoveRange(accSubCodesWantUpdated);
            await _Context.SaveChangesAsync();
            //---------------//
            //list i want to add
            List<AccSubCode> accSubCodesWantAdded = await _Context.AccSubCodes.Where(obj=>obj.MainCode == accMainCodeDtlDTO.MainCode).ToListAsync();
            foreach (var subCode in accSubCodesWantAdded)
            {
                subCode.MainCode = accMainCodeDtlDTO.SecCode;
            }
            await _Context.AccSubCodes.AddRangeAsync(accSubCodesWantAdded);

            await _Context.AddAsync(accMainCodeDtl);
            await _Context.SaveChangesAsync();
        }
        public async Task DeleteAccMainCodeDtlAsync(string secCode, string mainCode,int branch)
        {
            List<AccSubCode> accSubCodesWantDeleted = await _Context.AccSubCodes.Where(obj => obj.MainCode == secCode && obj.CoCode ==branch).ToListAsync();
            _Context.AccSubCodes.RemoveRange(accSubCodesWantDeleted);

            AccMainCodeDtl accMainCodeDtl = await _Context.AccMainCodeDtls.FirstOrDefaultAsync(obj=>obj.SecCode == secCode && obj.CoCode == branch && obj.MainCode ==mainCode);
            _Context.AccMainCodeDtls.Remove(accMainCodeDtl);

            await _Context.SaveChangesAsync();
        }
    }
}
