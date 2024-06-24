using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class TransDtlRepo: Repository<TransDtl, int>, ITransDtlRepo
    {
        private readonly IMapper _mapper;
        public TransDtlRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<TransDtlWithPriceDTO>> GetPermissionDetailsAsync(int transMasterID)
        {
            List<TransDtl> transDtlList = await _DbSet.Where(obj => obj.TransMAsterID == transMasterID).ToListAsync();
            var transDtlListDTO = _mapper.Map<List<TransDtlWithPriceDTO>>(transDtlList);

            foreach (var item in transDtlListDTO)
            {
                if (item.ItemMaster is not (null or ""))
                {
                    item.ItemMasterName = (await _Context.SubItems
                        .FirstOrDefaultAsync(obj => obj.SubId.ToString() == item.ItemMaster)).SubName;
                }
                if (item.UnitCode != 0)
                {
                    item.UnitCodeName = (await _Context.UnitCodes
                        .FirstOrDefaultAsync(obj => obj.Code == item.UnitCode)).Names;
                }
            }

            return transDtlListDTO;
        }

        public async Task<string> GetBarCodeAsync(int transMAsterID, int serial, int itemMaster)
        {
            TransMaster transMAster = await _Context.TransMasters.FindAsync(transMAsterID);

            List<ItemBatch> batchList = await _Context.ItemBatches.Where(obj =>
                obj.StockCode == transMAster.StockCode && obj.TransN == transMAster.DocNo &&
                obj.ItemMaster == itemMaster.ToString() && obj.Serial == serial && obj.FYear == transMAster.FYear)
                .ToListAsync();
            if(batchList.Count == 0)
            {
                List<ItemBatch> newBatchList = await _Context.ItemBatches.Where(obj =>
                    obj.StockCode == transMAster.StockCode && obj.FYear == transMAster.FYear)
                    .ToListAsync();
                var itemBatch = newBatchList.Count != 0 ? newBatchList.Max(obj => obj.ItmBatchMax) + 1 : 1;

                var itemBatchCode = $"000000{itemBatch}"[^6..];
                var stockCode = $"00{transMAster.StockCode}"[^2..];
                var fYear = $"00{transMAster.FYear}"[^2..];
                var docNo = $"0000{transMAster.DocNo}"[^4..];

                var newBarCode = stockCode + fYear + docNo + itemBatchCode;

                return newBarCode;
            }
            return string.Empty;
            //throw new NotImplementedException();
        }

        ////////////////////////////////////////////////////////////////////////////
        // For Showing Trans Price
        public async Task<TransDtlWithPriceDTO> GetNewTransDtlWithPriceAsync(int transMAsterID)
        {
            var transDtlDTO = new TransDtlWithPriceDTO();
            transDtlDTO.TransMasterID = transMAsterID;

            List<TransDtl> transDtlList = await _DbSet.Where(obj => obj.TransMAsterID == transMAsterID).ToListAsync();
            transDtlDTO.Serial = transDtlList.Count != 0 ? transDtlList.Max(obj => obj.Serial) + 1 : 1;

            List<SubItem> subItems = await _Context.SubItems.ToListAsync();
            transDtlDTO.SubItems = _mapper.Map<List<SubItemViewDTO>>(subItems);

            List<UnitCode> unitCodes = await _Context.UnitCodes.ToListAsync();
            transDtlDTO.UnitCodes = _mapper.Map<List<UnitCodeDTO>>(unitCodes);

            return transDtlDTO;
        }

        public async Task<TransDtlWithPriceDTO> GetTransDtlWithPriceByIdAsync(int transDtlID)
        {
            TransDtl transDtl = await GetByIdAsync(transDtlID);
            var transDtlDTO = _mapper.Map<TransDtlWithPriceDTO>(transDtl);

            List<SubItem> subItems = await _Context.SubItems.ToListAsync();
            transDtlDTO.SubItems = _mapper.Map<List<SubItemViewDTO>>(subItems);

            List<UnitCode> unitCodes = await _Context.UnitCodes.ToListAsync();
            transDtlDTO.UnitCodes = _mapper.Map<List<UnitCodeDTO>>(unitCodes);

            return transDtlDTO;
        }

        public async Task AddTransDtlWithPriceAsync(TransDtlWithPriceDTO transDtlDTO)
        {
            TransMaster transMaster = await _Context.TransMasters
                .FirstOrDefaultAsync(obj => obj.TransMAsterID == transDtlDTO.TransMasterID);

            var transDtl = _mapper.Map<TransDtl>(transDtlDTO);
            transDtl.StockCode = 1;
            transDtl.SubCode = 2;
            transDtl.ItemQty = 1;
            transDtl.Price = transDtl.Price != null ? transDtl.Price : 0;
            transDtl.ItemUnitQty = 0;
            transDtl.Price2 = 0;
            transDtl.ItemVal2 = 0;
            transDtl.Flag1 = 1;
            transDtl.UnitQty = transDtl.UnitQty != null ? transDtl.UnitQty : 1;
            transDtl.ItemMaster2 = 0;
            transDtl.UnitCode = transDtl.UnitCode != null ? transDtl.UnitCode : 1;
            _mapper.Map(transMaster, transDtl);

            transDtl.PostPos = 1;

            await AddAsync(transDtl);
            await SaveChangesAsync();
        }

        public async Task UpdateTransDtlWithPriceAsync(int id, TransDtlWithPriceDTO transDtlDTO)
        {
            TransDtl transDtl = await GetByIdAsync(id);

            TransMaster transMaster = await _Context.TransMasters
                .FirstOrDefaultAsync(obj => obj.TransMAsterID == transDtlDTO.TransMasterID);

            _mapper.Map(transDtlDTO, transDtl);


            _mapper.Map(transMaster, transDtl);

            transDtl.PostPos = 1;

            await UpdateAsync(transDtl);
            await SaveChangesAsync();
        }

        ////////////////////////////////////////////////////////////////////////////
        // For Not Showing Trans Price
        public async Task<TransDtlDTO> GetNewTransDtlAsync(int transMAsterID)
        {
            var transDtlDTO = new TransDtlDTO();
            transDtlDTO.TransMasterID = transMAsterID;

            List<TransDtl> transDtlList = await _DbSet.Where(obj => obj.TransMAsterID == transMAsterID).ToListAsync();
            transDtlDTO.Serial = transDtlList.Count != 0 ? transDtlList.Max(obj => obj.Serial) + 1 : 1;

            List<SubItem> subItems = await _Context.SubItems.ToListAsync();
            transDtlDTO.SubItems = _mapper.Map<List<SubItemViewDTO>>(subItems);

            List<UnitCode> unitCodes = await _Context.UnitCodes.ToListAsync();
            transDtlDTO.UnitCodes = _mapper.Map<List<UnitCodeDTO>>(unitCodes);

            List<UnitCode> itemStatusList = await _Context.UnitCodes.Where(obj => obj.Flag1 == 3).ToListAsync();
            transDtlDTO.ItemStatusList = _mapper.Map<List<UnitCodeDTO>>(itemStatusList);

            return transDtlDTO;
        }

        public async Task<TransDtlDTO> GetTransDtlByIdAsync(int transDtlID)
        {
            TransDtl transDtl = await GetByIdAsync(transDtlID);
            var transDtlDTO = _mapper.Map<TransDtlDTO>(transDtl);

            List<SubItem> subItems = await _Context.SubItems.ToListAsync();
            transDtlDTO.SubItems = _mapper.Map<List<SubItemViewDTO>>(subItems);

            List<UnitCode> unitCodes = await _Context.UnitCodes.ToListAsync();
            transDtlDTO.UnitCodes = _mapper.Map<List<UnitCodeDTO>>(unitCodes);

            List<UnitCode> itemStatusList = await _Context.UnitCodes.Where(obj => obj.Flag1 == 3).ToListAsync();
            transDtlDTO.ItemStatusList = _mapper.Map<List<UnitCodeDTO>>(itemStatusList);

            return transDtlDTO;
        }

        public async Task AddTransDtlAsync(TransDtlDTO transDtlDTO)
        {
            TransMaster transMaster = await _Context.TransMasters
                .FirstOrDefaultAsync(obj => obj.TransMAsterID == transDtlDTO.TransMasterID);

            var transDtl = _mapper.Map<TransDtl>(transDtlDTO);
            _mapper.Map(transMaster, transDtl);

            transDtl.ItemMaster2 = 0;
            //List<TransDtl> transDtlList = await _DbSet.Where(obj => obj.TransMAsterID == transDtl.TransMAsterID).ToListAsync();
            //transDtl.Serial = transDtlList.Count != 0 ? transDtlList.Max(obj => obj.Serial) + 1 : 1;

            await AddAsync(transDtl);
            await SaveChangesAsync();
        }

        public async Task UpdateTransDtlAsync(int id, TransDtlDTO transDtlDTO)
        {
            TransDtl transDtl = await GetByIdAsync(id);

            TransMaster transMaster = await _Context.TransMasters
                .FirstOrDefaultAsync(obj => obj.TransMAsterID == transDtlDTO.TransMasterID);

            _mapper.Map(transDtlDTO, transDtl);
            _mapper.Map(transMaster, transDtl);

            await UpdateAsync(transDtl);
            await SaveChangesAsync();
        }
    }
}
