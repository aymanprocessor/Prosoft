using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Treasury
{
    public class AccSafeCheckRepo : Repository<AccSafeCheck, int>, IAccSafeCheckRepo
    {
        private readonly IMapper _mapper;

        public AccSafeCheckRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<AccSafeCheckViewDTO>> GetAccSafeCashAsync(string tranType, string flagType, int fYear, int safeCode)
        {
            List<AccSafeCheckViewDTO> accSafeCeckDTOs = new List<AccSafeCheckViewDTO>();

            if (flagType == "oneANDtwo")
            {
                accSafeCeckDTOs = await _Context.AccSafeChecks
                    .Where(obj => obj.TranType == tranType && (obj.Flag == 1 || obj.Flag == 2) && obj.FYear == fYear && obj.SafeCode == safeCode)
                    .Select(obj => new AccSafeCheckViewDTO()
                    {
                        SafeCeckId = obj.SafeCeckId,
                        DocNo = (int)obj.DocNo,
                        SafeName = obj.SafeName.SafeNames,
                        DocDate = obj.DocDate,
                        PersonName = obj.PersonName,
                        ValuePay = obj.ValuePay,
                        ChekNo =obj.ChekNo,
                    }).ToListAsync();
            }
            else if (flagType == "oneANDtwoAndthree")
            {
                accSafeCeckDTOs = await _Context.AccSafeChecks
                    .Where(obj => obj.TranType == tranType && (obj.Flag == 1 || obj.Flag == 2 || obj.Flag == 3))
                    .Select(obj => new AccSafeCheckViewDTO()
                    {
                        SafeCeckId = obj.SafeCeckId,
                        DocNo = (int)obj.DocNo,
                        SafeName = obj.SafeName.SafeNames,
                        DocDate = obj.DocDate,
                        PersonName = obj.PersonName,
                        ValuePay = obj.ValuePay,
                        ChekNo = obj.ChekNo,
                    }).ToListAsync();
            }

            return accSafeCeckDTOs;
        }
        public async Task<int> GetNewSerialAsync(string tranType, int safeCode, int fYear)
        {
            int newSerial;
            var accSafeCeck = await _Context.AccSafeChecks
                .Where(obj => obj.TranType == tranType && obj.SafeCode == safeCode && obj.FYear == fYear).CountAsync();

            if (accSafeCeck != 0)
            {
                //var lastID = await _DbSet.MaxAsync(obj => obj.DocNo);
                newSerial = (int)accSafeCeck + 1;
            }
            else
                newSerial = 1;
            return newSerial;
        }
        public async Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode)
        {
            List<AccSubCode> subAccCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == mainAccCode).ToListAsync();
            var subAccCodesDTO = _mapper.Map<List<AccSubCodeDTO>>(subAccCodes);
            return subAccCodesDTO;
        }

        public async Task<AccSafeCheckEditAddDTO> GetEmptyAccSafeCeckAsync()
        {
            AccSafeCheckEditAddDTO accSafeCeckDTO = new AccSafeCheckEditAddDTO();

            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            List<CostCenter> costCenters = await _Context.CostCenters.ToListAsync();
            List<SafeName> safeNames = await _Context.SafeNames.ToListAsync();
            List<AccGlobalDef> accGlobalDefs = await _Context.accGlobalDefs.ToListAsync();
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes.ToListAsync();
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.ToListAsync();

            var mainCode = (await _Context.EisPostings.FindAsync(13)).MainCode;
            List<AccSubCode> banks = await _Context.AccSubCodes.Where(obj=>obj.MainCode ==mainCode).ToListAsync();

            var mainName13 = (await _Context.EisPostings.FindAsync(13)).ModuleN;
            var mainName17 = (await _Context.EisPostings.FindAsync(17)).ModuleN;

            accSafeCeckDTO.journalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            accSafeCeckDTO.costCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            accSafeCeckDTO.treasuryNames = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames);
            accSafeCeckDTO.accGlobalDefs = _mapper.Map<List<AccGlobalDefDTO>>(accGlobalDefs);
            accSafeCeckDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);
            accSafeCeckDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);
            accSafeCeckDTO.banks = _mapper.Map<List<AccSubCodeDTO>>(banks);
            accSafeCeckDTO.mainName13 = mainName13;
            accSafeCeckDTO.mainName17 = mainName17;

            return accSafeCeckDTO;
        }

        public async Task AddAccSafeCeckAsync(AccSafeCheckEditAddDTO accSafeCeckDTO)
        {
            AccSafeCheck accSafeCeck = _mapper.Map<AccSafeCheck>(accSafeCeckDTO);
            //accSafeCash.DocType = "SFCIN";
            if (accSafeCeck.TranType == "SFSIN")
            {
                accSafeCeck.MCodeDtl = 33;
            }
            else if (accSafeCeck.TranType == "SFOUT")
            {
                accSafeCeck.MCodeDtl = 34;
            }

            accSafeCeck.FlagS = "1";
            accSafeCeck.Flag = 1;
            accSafeCeck.CheckStatus = "1";
            accSafeCeck.FlagPayStatus = "0";
            accSafeCeck.DiscountVal = 0;            
            accSafeCeck.ProfitTax = 0;            
            accSafeCeck.FlagApr = "NO";            
            accSafeCeck.EntryDate = DateTime.Now;

            await _Context.AddAsync(accSafeCeck);
            await _Context.SaveChangesAsync();
        }

        public async Task<AccSafeCheckEditAddDTO> GetAccSafeCheckByIdAsync(int id)
        {
            AccSafeCheck accSafeCheck = await _Context.AccSafeChecks.FirstOrDefaultAsync(obj => obj.SafeCeckId == id);

            AccSafeCheckEditAddDTO accSafeCheckDTO = _mapper.Map<AccSafeCheckEditAddDTO>(accSafeCheck);

            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            List<CostCenter> costCenters = await _Context.CostCenters.ToListAsync();
            List<SafeName> safeNames = await _Context.SafeNames.ToListAsync();
            List<AccGlobalDef> accGlobalDefs = await _Context.accGlobalDefs.ToListAsync();
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes.ToListAsync();
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.Where(obj => obj.MainCode == accSafeCheck.MainCode).ToListAsync();
           
            var mainCode = (await _Context.EisPostings.FindAsync(13)).MainCode;
            List<AccSubCode> banks = await _Context.AccSubCodes.Where(obj => obj.MainCode == mainCode).ToListAsync();
          
            var mainName13 = (await _Context.EisPostings.FindAsync(13)).ModuleN;
            var mainName17 = (await _Context.EisPostings.FindAsync(17)).ModuleN;

            accSafeCheckDTO.journalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            accSafeCheckDTO.costCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            accSafeCheckDTO.treasuryNames = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames);
            accSafeCheckDTO.accGlobalDefs = _mapper.Map<List<AccGlobalDefDTO>>(accGlobalDefs);
            accSafeCheckDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);
            accSafeCheckDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);
            accSafeCheckDTO.banks = _mapper.Map<List<AccSubCodeDTO>>(banks);
            accSafeCheckDTO.mainName13 = mainName13;
            accSafeCheckDTO.mainName17 = mainName17;

            return accSafeCheckDTO;
        }

        public async Task EditAccSafeCheckAsync(int id, AccSafeCheckEditAddDTO accSafeCeckDTO)
        {

            AccSafeCheck accSafeCeck = await _Context.AccSafeChecks.FirstOrDefaultAsync(obj => obj.SafeCeckId == id);
            _mapper.Map(accSafeCeckDTO, accSafeCeck);
            //accSafeCash.DocType = "SFCIN";
            if (accSafeCeck.TranType == "SFSIN")
            {
                accSafeCeck.MCodeDtl = 33;
            }
            else if (accSafeCeck.TranType == "SFOUT")
            {
                accSafeCeck.MCodeDtl = 34;
            }
            accSafeCeck.CheckStatus = "1";
            accSafeCeck.EntryDate = DateTime.Now;
            _Context.Update(accSafeCeck);
            await _Context.SaveChangesAsync();
        }


        public async Task<bool> HasRelatedDataAsync(int id,string doctype)
        {
            // Check if there are related records in custCollectionsDiscounts
            var hasRelatedData = await _Context.custCollectionsDiscounts.AnyAsync(p => p.SafeCashId == id && p.DocType == doctype);
            return hasRelatedData;
        }

    }
}
