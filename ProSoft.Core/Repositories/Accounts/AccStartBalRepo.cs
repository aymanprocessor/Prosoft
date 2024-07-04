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
    public class AccStartBalRepo : Repository<AccStartBal, int>, IAccStartBalRepo
    {
        private readonly IMapper _mapper;

        public AccStartBalRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;   
        }
        public async Task<List<AccStartBalViewDTO>> GetAccStartBalAsync(int journalCode, int fYear,int branch)
        {
            // Load existing data
            //List<AccMainCode> accMainCodes = await _Context.AccMainCodes.ToListAsync();
            //List<AccSubCode> accSubCodes = new List<AccSubCode>();

            //foreach (var item in accMainCodes)
            //{
            //    var subCodes = await _Context.AccSubCodes.Where(obj => obj.MainCode == item.MainCode).ToListAsync();
            //    accSubCodes.AddRange(subCodes);
            //}
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.ToListAsync();

            List<AccStartBal> accStartBals = await _Context.AccStartBals
                .Where(obj => obj.TransType == journalCode.ToString())
                .ToListAsync();

            // Create a list to store new AccStartBal objects
            List<AccStartBal> accStartBalForInsert = new List<AccStartBal>();

            // Check each AccSubCode
            foreach (var item in accSubCodes)
            {
                // Check if the combination of MainCode and SubCode exists in accStartBals
                bool exists = accStartBals.Any(bal => bal.MainCode == item.MainCode && bal.SubCode == item.SubCode && bal.TransType == journalCode.ToString() && bal.FYear ==fYear && bal.CoCode ==branch);

                // If it doesn't exist, create a new AccStartBal and add to the list
                if (!exists)
                {
                    AccStartBal newAccStartBal = new AccStartBal
                    {
                        MainCode = item.MainCode,
                        SubCode = item.SubCode,
                        AccName = await GetAccountName(item.MainCode, item.SubCode),
                        TransType = journalCode.ToString(),
                        FDepOr = 0,
                        FCreditOr = 0,
                        FDepCur = 0,
                        FCreditCur = 0,
                        FYear =fYear,
                        CoCode =branch
                    };

                    accStartBalForInsert.Add(newAccStartBal);
                }
            
            }
            // Bulk insert all 
            if (accStartBalForInsert.Any())
            {
                await _Context.AccStartBals.AddRangeAsync(accStartBalForInsert);
                await _Context.SaveChangesAsync();
            }
            accStartBals = accStartBals.OrderBy(bal => bal.MainCode).ThenBy(bal => bal.SubCode).ToList();//Sorting
            List<AccStartBalViewDTO> AccStartBalDTOs = _mapper.Map<List<AccStartBalViewDTO>>(accStartBals);

            List<CostCenter> costCenters = await _Context.CostCenters.ToListAsync();
            foreach (var item in AccStartBalDTOs) //to set cost center Name
            {
                var costCenter = costCenters.FirstOrDefault(cc => cc.CostCode.ToString() == item.CostCenterCode);
                if (costCenter != null)
                {
                    item.CostCenterName = costCenter.CostDesc;
                }
            }

            return AccStartBalDTOs;
        }
       
        //Get AccName
        private async Task<string> GetAccountName(string mainCode, string subCode)
        {
            AccMainCode myMainCode = await _Context.AccMainCodes
                .FirstOrDefaultAsync(obj => obj.MainCode == mainCode);
            string mainName = myMainCode.MainName;

            AccSubCode mySubCode = await _Context.AccSubCodes
                .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == mainCode);
            string subName = mySubCode! != null ? mySubCode.SubName : string.Empty;

            return subName != "" ? $"{mainName} / {subName}" : mainName;
        }
    }
}
