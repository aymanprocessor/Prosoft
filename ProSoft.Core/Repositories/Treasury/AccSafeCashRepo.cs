using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
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
    public class AccSafeCashRepo : Repository<AccSafeCash, int>, IAccSafeCashRepo
    {

        private readonly IMapper _mapper;
        public AccSafeCashRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<AccSafeCashViewDTO>> GetAccSafeCashAsync(string docType, string flagType, int fYear, int safeCode)
        {
            List<AccSafeCashViewDTO> accSafeCashDTOs = new List<AccSafeCashViewDTO>();

            if (flagType == "oneANDtwo")
            {
                accSafeCashDTOs = await _Context.AccSafeCashes
                    .Where(obj => obj.DocType == docType && (obj.Flag == 1 || obj.Flag == 2) && obj.FYear==fYear && obj.SafeCode== safeCode)
                    .Select(obj => new AccSafeCashViewDTO()
                    {
                        SafeCashId = obj.SafeCashId,
                        DocNo = (int)obj.DocNo,
                        SafeName = obj.SafeName.SafeNames,
                        DocDate = obj.DocDate,
                        PersonName = obj.PersonName,
                        ValuePay = obj.ValuePay,
                    }).ToListAsync();
            }
            else if (flagType == "oneANDtwoAndthree")
            {
                accSafeCashDTOs = await _Context.AccSafeCashes
                    .Where(obj => obj.DocType == docType && (obj.Flag == 1 || obj.Flag == 2 || obj.Flag == 3))
                    .Select(obj => new AccSafeCashViewDTO()
                    {
                        SafeCashId = obj.SafeCashId,
                        DocNo = (int)obj.DocNo,
                        SafeName = obj.SafeName.SafeNames,
                        DocDate = obj.DocDate,
                        PersonName = obj.PersonName,
                        ValuePay = obj.ValuePay
                    }).ToListAsync();
            }

            return accSafeCashDTOs;
        }

        //public async Task<int> GetNewIdAsync()
        //{
        //    int newID;
        //    if (_DbSet.Count() != 0)
        //    {
        //        var lastID = await _DbSet.MaxAsync(obj => obj.SafeCashId);
        //        newID = lastID + 1;
        //    }
        //    else
        //        newID = 1;
        //    return newID;
        //}
        public async Task<int> GetNewSerialAsync(string docType, int safeCode, int fYear)
        {
            int newSerial;
            var accSafeCash = await _Context.AccSafeCashes
                .Where(obj => obj.DocType == docType && obj.SafeCode == safeCode && obj.FYear == fYear).CountAsync();
           
            if (accSafeCash != 0)
            {
                //var lastID = await _DbSet.MaxAsync(obj => obj.DocNo);
                newSerial = (int)accSafeCash + 1;
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

        public async Task<AccSafeCashEditAddDTO> GetEmptyPaymentReceiptAsync()
        {
            AccSafeCashEditAddDTO accSafeCashDTO = new AccSafeCashEditAddDTO();

            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            List<GTable> gTables = await _Context.gTables.Where(obj => obj.Flag == 30).ToListAsync();
            List<CostCenter> costCenters = await _Context.CostCenters.ToListAsync();
            List<SafeName> safeNames = await _Context.SafeNames.ToListAsync();
            List<AccGlobalDef> accGlobalDefs = await _Context.accGlobalDefs.ToListAsync();
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes.ToListAsync();
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.ToListAsync();

            accSafeCashDTO.journalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            accSafeCashDTO.gTablels = _mapper.Map<List<GTablelDTO>>(gTables);
            accSafeCashDTO.costCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            accSafeCashDTO.treasuryNames = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames);
            accSafeCashDTO.accGlobalDefs = _mapper.Map<List<AccGlobalDefDTO>>(accGlobalDefs);
            accSafeCashDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);
            accSafeCashDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);

            return accSafeCashDTO;
        }

        public async Task AddPaymentReceiptAsync(AccSafeCashEditAddDTO accSafeCashDTO)
        {
            AccSafeCash accSafeCash = _mapper.Map<AccSafeCash>(accSafeCashDTO);
            //accSafeCash.DocType = "SFCIN";
            accSafeCash.MCodeDtl = 31;
            accSafeCash.SerId = 1;
            accSafeCash.EntryDate = DateTime.Now;

            await _Context.AddAsync(accSafeCash);
            await _Context.SaveChangesAsync();
        }

        public async Task<AccSafeCashEditAddDTO> GetPaymentReceiptByIdAsync(int id)
        {
            AccSafeCash accSafeCash = await _Context.AccSafeCashes.FirstOrDefaultAsync(obj => obj.SafeCashId == id);

            AccSafeCashEditAddDTO accSafeCashDTO = _mapper.Map<AccSafeCashEditAddDTO>(accSafeCash);

            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            List<GTable> gTables = await _Context.gTables.Where(obj => obj.Flag == 30).ToListAsync();
            List<CostCenter> costCenters = await _Context.CostCenters.ToListAsync();
            List<SafeName> safeNames = await _Context.SafeNames.ToListAsync();
            List<AccGlobalDef> accGlobalDefs = await _Context.accGlobalDefs.ToListAsync();
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes.ToListAsync();
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.Where(obj => obj.MainCode == accSafeCash.MainCode).ToListAsync();

            accSafeCashDTO.journalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            accSafeCashDTO.gTablels = _mapper.Map<List<GTablelDTO>>(gTables);
            accSafeCashDTO.costCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            accSafeCashDTO.treasuryNames = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames);
            accSafeCashDTO.accGlobalDefs = _mapper.Map<List<AccGlobalDefDTO>>(accGlobalDefs);
            accSafeCashDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);
            accSafeCashDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);

            return accSafeCashDTO;
        }

        public async Task EditPaymentReceiptAsync(int id, AccSafeCashEditAddDTO accSafeCashDTO)
        {
            AccSafeCash accSafeCash = await _Context.AccSafeCashes.FirstOrDefaultAsync(obj => obj.SafeCashId == id);
            _mapper.Map(accSafeCashDTO, accSafeCash);
            //accSafeCash.DocType = "SFCIN";
            accSafeCash.MCodeDtl = 31;
            accSafeCash.SerId = 1;
            accSafeCash.EntryDate = DateTime.Now;
            _Context.Update(accSafeCash);
            await _Context.SaveChangesAsync();
        }
    }
}
