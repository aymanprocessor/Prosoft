using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
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
            List<TransDtl> transDtlList = await _DbSet.Where(obj => obj.TransMAsterID == transMasterID)
                .OrderByDescending(obj => obj.Serial).ToListAsync();
            var transDtlListDTO = new List<TransDtlWithPriceDTO>();

            foreach (var item in transDtlList)
            {
                TransDtlWithPriceDTO transDtlDTO = await GetForViewAsync(item);
                StockEmp stockTrans = await _Context.StockEmps.FirstOrDefaultAsync(obj =>
                    obj.Stkcod == item.StockCode && obj.TransType == item.TransType &&
                    obj.UserId == item.UserCode);

                transDtlDTO.ShowTransPrice = (int)stockTrans.ShowPrice;
                transDtlListDTO.Add(transDtlDTO);
            }
            return transDtlListDTO;
        }

        public async Task<TransDtlWithPriceDTO> GetForViewAsync(TransDtl transDtl)
        {
            if(transDtl != null)
            {
                var transDtlDTO = _mapper.Map<TransDtlWithPriceDTO>(transDtl);
                if (transDtl.ItemMaster is not (null or ""))
                {
                    SubItem subItem = await _Context.SubItems
                        .FirstOrDefaultAsync(obj => obj.ItemCode == transDtl.ItemMaster);
                    transDtlDTO.ItemMasterName = subItem != null ? subItem.SubName : "";
                }
                if (transDtl.UnitCode != 0)
                {
                    transDtlDTO.UnitCodeName = (await _Context.UnitCodes
                        .FirstOrDefaultAsync(obj => obj.Code == transDtl.UnitCode)).Names;
                }
                return transDtlDTO;
            }
            return null;
        }

        public async Task AddToReceiveFormAsync(TransDtl convertTransDtl)
        {
            TransMaster convertTransMaster = await _Context.TransMasters.FindAsync(convertTransDtl.TransMAsterID);
            
            TransMaster receiveTransMaster = await _Context.TransMasters.FirstOrDefaultAsync(obj =>
                obj.TransType == 23 && obj.DocNoFr == convertTransMaster.DocNo &&
                obj.StockCode == convertTransMaster.StockCode2);

            var receiveTransDtl = _mapper.Map<TransDtl>(convertTransDtl);
            List<TransDtl> transDtlList = await _DbSet.Where(obj => obj.TransMAsterID == receiveTransMaster.TransMAsterID)
                .ToListAsync();
            receiveTransDtl.Serial = transDtlList.Count != 0 ? transDtlList.Max(obj => obj.Serial) + 1 : 1;

            _mapper.Map(receiveTransMaster, receiveTransDtl);
            await AddAsync(receiveTransDtl);
        }

        public async Task UpdateReceiveFormDetailAsync(TransDtl convertTransDtl)
        {
            TransMaster transMaster = await _Context.TransMasters
                .FirstOrDefaultAsync(obj => obj.TransMAsterID == convertTransDtl.TransMAsterID);
            if (convertTransDtl.TransType == 13)
            {
                TransDtl receiveTransDtl = await _DbSet.FirstOrDefaultAsync(obj => obj.TransType == 23 &&
                    obj.DocNoFr == transMaster.DocNo && obj.StockCode == transMaster.StockCode2 &&
                    obj.Serial == convertTransDtl.Serial);

                receiveTransDtl.UnitQty = convertTransDtl.UnitQty;
                receiveTransDtl.UnitCode = convertTransDtl.UnitCode;
                receiveTransDtl.Price = convertTransDtl.Price;
                receiveTransDtl.ItmHaveTax = convertTransDtl.ItmHaveTax;
                receiveTransDtl.TaxVal = convertTransDtl.TaxVal;
                receiveTransDtl.ItemVal = convertTransDtl.ItemVal;
                receiveTransDtl.Flag1 = convertTransDtl.Flag1;
                receiveTransDtl.ExpireDate = convertTransDtl.ExpireDate;
                await UpdateAsync(receiveTransDtl);
            }
        }

        public async Task<string> GetItemAsync(string itemBarcode)
        {
            ItemBatch itemBatch = await _Context.ItemBatches.FirstOrDefaultAsync(obj => obj.ItmBatch == itemBarcode);
            return itemBatch != null ? itemBatch.ItemMaster : string.Empty;
        }

        public async Task<string> GetOldBarCodeAsync(string itemCode)
        {
            ItemBatch itemBatch = await _Context.ItemBatches.FirstOrDefaultAsync(obj => obj.ItemMaster == itemCode);
            return itemBatch != null ? itemBatch.ItmBatch : string.Empty;
        }

        public async Task<string> GetBarCodeAsync(int transMAsterID, int serial, string itemMaster)
        {
            TransMaster transMAster = await _Context.TransMasters.FindAsync(transMAsterID);

            List<ItemBatch> batchList = await _Context.ItemBatches.Where(obj =>
                obj.StockCode == transMAster.StockCode && obj.TransN == transMAster.DocNo &&
                obj.ItemMaster == itemMaster && obj.Serial == serial && obj.FYear == transMAster.FYear)
                .ToListAsync();
            if(batchList.Count == 0)
            {
                List<ItemBatch> newBatchList = await _Context.ItemBatches.Where(obj =>
                    obj.StockCode == transMAster.StockCode && obj.FYear == transMAster.FYear)
                    .ToListAsync();
                var itemBatch = newBatchList.Count != 0 ? newBatchList.Max(obj => obj.ItmBatchMax) + 1 : 1;

                var ls_maxSer_max = $"000000{itemBatch}"[^6..];
                var stockCode = $"00{transMAster.StockCode}"[^2..];
                var fYear = $"00{transMAster.FYear}"[^2..];
                var docNo = $"0000{transMAster.DocNo}"[^4..];

                var newBarCode = stockCode + fYear + docNo + ls_maxSer_max;

                return newBarCode;
            }
            return string.Empty;
        }
        
        public async Task InsertItemBatchAsync(TransDtl transDtl)
        {
            TransMaster transMaster = await _Context.TransMasters
                .FirstOrDefaultAsync(obj => obj.TransMAsterID == transDtl.TransMAsterID);

            var ls_Item_Barcode = string.Empty;
            if (transDtl.UnitQty == 1)
                ls_Item_Barcode = transDtl.ItemMaster;
            else if (transDtl.UnitQty > 1)
                ls_Item_Barcode = $"{transDtl.ItemMaster}_{transDtl.Serial}";

            var newItemBatch = new ItemBatch();
            newItemBatch.ItemMaster = transDtl.ItemMaster;
            newItemBatch.ItmBatch = await GetBarCodeAsync(transDtl.TransMAsterID, int.Parse(transDtl.Serial.ToString()), transDtl.ItemMaster);
            newItemBatch.SerBatch = int.Parse(transDtl.Serial.ToString());
            newItemBatch.TmBarcode = ls_Item_Barcode;
            //newItemBatch.UnitQty = 1;
            newItemBatch.Price = transDtl.Price;
            newItemBatch.ReqDate = DateTime.Now;
            newItemBatch.UserCode = transDtl.UserCode;
            newItemBatch.ExpDate = transDtl.ExpireDate;
            newItemBatch.TransN = transDtl.DocNo;
            newItemBatch.StockCode = transDtl.StockCode;
            var subItems_1 = await _Context.SubItems.FirstOrDefaultAsync(obj =>
                obj.MainCode == transDtl.MainCode && obj.ItemCode == transDtl.ItemMaster);
            newItemBatch.Flag1 = subItems_1 != null ? subItems_1.Flag1 : 0;

            var subItems_2 = await _Context.SubItems.FirstOrDefaultAsync(obj =>
                obj.MainCode == transDtl.MainCode && obj.ItemCode == transDtl.ItemMaster &&
                obj.Flag1 == newItemBatch.Flag1);
            newItemBatch.BatchCounter = subItems_2 != null ? subItems_2.BatchCounter : 0;
            newItemBatch.UnitQty = newItemBatch.BatchCounter == 1 ? 1 : transDtl.UnitQty;
            newItemBatch.FYear = transDtl.FYear;

            List<ItemBatch> newBatchList = await _Context.ItemBatches.Where(obj =>
                obj.StockCode == transMaster.StockCode && obj.FYear == transMaster.FYear)
                .ToListAsync();
            var itemBatch = newBatchList.Count != 0 ? newBatchList.Max(obj => obj.ItmBatchMax) + 1 : 1;
            var ls_maxSer_max = $"000000{itemBatch}"[^6..];

            newItemBatch.ItmBatchMax = long.Parse(ls_maxSer_max);
            newItemBatch.BranchId = transDtl.BranchId;
            newItemBatch.UserCodeModify = transDtl.UserCode;
            newItemBatch.Serial = (int)transDtl.Serial;

            await _Context.AddAsync(newItemBatch);
            transDtl.ItmBarcode = newItemBatch.ItmBatch;
        }

        ////////////////////////////////////////////////////////////////////////////
        // For Showing Trans Price
        public async Task<TransDtlWithPriceDTO> GetNewTransDtlWithPriceAsync(int transMAsterID)
        {
            TransMaster transMaster = await _Context.TransMasters.FindAsync(transMAsterID);

            var transDtlDTO = new TransDtlWithPriceDTO();
            _mapper.Map(transMaster, transDtlDTO);

            transDtlDTO.PermissionName = (await _Context.GeneralCodes.FirstOrDefaultAsync(obj =>
                obj.UniqueType == transMaster.TransType)).GDesc;

            List<TransDtl> transDtlList = await _DbSet.Where(obj => obj.TransMAsterID == transMAsterID).ToListAsync();
            transDtlDTO.Serial = transDtlList.Count != 0 ? transDtlList.Max(obj => obj.Serial) + 1 : 1;

            List<SubItem> subItems = await _Context.SubItems.ToListAsync();
            transDtlDTO.SubItems = subItems.Select(obj => new SubItemViewDTO
            {
                SubId = obj.SubId,
                SubName = obj.SubName,
                ItemCode = obj.ItemCode,
                CodeAndName = $"{obj.SubName} \\ {obj.ItemCode}",
            }).ToList();

            List<UnitCode> unitCodes = await _Context.UnitCodes.ToListAsync();
            transDtlDTO.UnitCodes = _mapper.Map<List<UnitCodeDTO>>(unitCodes);

            return transDtlDTO;
        }

        public async Task<TransDtlWithPriceDTO> GetTransDtlWithPriceByIdAsync(int transDtlID)
        {
            TransDtl transDtl = await GetByIdAsync(transDtlID);
            var transDtlDTO = _mapper.Map<TransDtlWithPriceDTO>(transDtl);

            transDtlDTO.PermissionName = (await _Context.GeneralCodes.FirstOrDefaultAsync(obj =>
                obj.UniqueType == transDtl.TransType)).GDesc;
            transDtlDTO.ShowItemMaster = transDtlDTO.ItemMaster;

            List<SubItem> subItems = await _Context.SubItems.ToListAsync();
            transDtlDTO.SubItems = subItems.Select(obj => new SubItemViewDTO
            {
                SubId = obj.SubId,
                SubName = obj.SubName,
                ItemCode = obj.ItemCode,
                CodeAndName = $"{obj.SubName} \\ {obj.ItemCode}",
            }).ToList();

            List<UnitCode> unitCodes = await _Context.UnitCodes.ToListAsync();
            transDtlDTO.UnitCodes = _mapper.Map<List<UnitCodeDTO>>(unitCodes);

            return transDtlDTO;
        }

        public async Task<TransDtl> AddTransDtlWithPriceAsync(TransDtlWithPriceDTO transDtlDTO)
        {
            TransMaster transMaster = await _Context.TransMasters
                .FirstOrDefaultAsync(obj => obj.TransMAsterID == transDtlDTO.TransMasterID);

            var transDtl = _mapper.Map<TransDtl>(transDtlDTO);
            transDtl.StockCode = 1;

            transDtl.SubCode = 2;
            var subItems_1 = await _Context.SubItems.FirstOrDefaultAsync(obj =>
                obj.MainCode == transDtl.ItemMaster);
            transDtl.MainCode = subItems_1 != null ? subItems_1.MainCode : "";

            transDtl.ItemQty = 1;
            transDtl.Price = transDtl.Price != null ? transDtl.Price : 0;
            transDtl.ItemUnitQty = 0;
            transDtl.Price2 = 0;
            transDtl.ItemVal2 = 0;
            transDtl.UnitQty = transDtl.UnitQty != null ? transDtl.UnitQty : 1;
            transDtl.ItemMaster2 = 0;
            transDtl.UnitCode = transDtl.UnitCode != null ? transDtl.UnitCode : 1;
            _mapper.Map(transMaster, transDtl);

            transDtl.ItemMaster = transDtlDTO.ShowItemMaster != null ? transDtlDTO.ShowItemMaster : transDtlDTO.ItemMaster;
            SubItem subItem = await _Context.SubItems.FirstOrDefaultAsync(obj => obj.ItemCode == transDtl.ItemMaster);
            transDtl.Flag1 = subItem.Flag1 != null ? subItem.Flag1 : 1;

            transDtl.PostPos = 1;
            /////////////////////////////////////////////////
            // Add to ItemBatch
            if (transDtl.TransType == 2)
                await InsertItemBatchAsync(transDtl);
            /////////////////////////////////////////////////
            // Add To Receive Form Detail
            else if (transDtl.TransType == 13)
                await AddToReceiveFormAsync(transDtl);

            /////////////////////////////////////////////////
            // Update TotTransVal, TaxValue, DueValue in transMaster
            //transMaster.TotTransVal = transMaster.TotTransVal != null ?
            //    transMaster.TotTransVal + (transDtl.UnitQty * transDtl.Price) :
            //    transDtl.UnitQty * transDtl.Price;
            transMaster.TotTransVal = transMaster.TotTransVal != null ?
                transMaster.TotTransVal + transDtl.ItemVal : transDtl.ItemVal;
            transMaster.TaxValue = transMaster.TaxValue != null ?
                transMaster.TaxValue + transDtl.TaxVal : transDtl.TaxVal;
            transMaster.DueValue = transMaster.DueValue != null ?
                transMaster.DueValue + (transDtl.UnitQty * transDtl.Price) :
                transDtl.UnitQty * transDtl.Price;

            await AddAsync(transDtl);
            await SaveChangesAsync();
            return transDtl;
        }
        public async Task UpdateTransDtlWithPriceAsync(int id, TransDtlWithPriceDTO transDtlDTO)
        {
            TransDtl transDtl = await GetByIdAsync(id);

            TransMaster transMaster = await _Context.TransMasters
                .FirstOrDefaultAsync(obj => obj.TransMAsterID == transDtlDTO.TransMasterID);

            /////////////////////////////////////////////////
            // Update TotTransVal, TaxValue, DueValue in transMaster
            transMaster.TotTransVal -= transDtl.ItemVal;
            transMaster.TaxValue -= transDtl.TaxVal;
            transMaster.DueValue -= transDtl.UnitQty * transDtl.Price;
            /////////////////////////////////////////////////
            transMaster.TotTransVal += transDtlDTO.ItemVal;
            transMaster.TaxValue += transDtlDTO.TaxVal;
            transMaster.DueValue += transDtlDTO.UnitQty * transDtlDTO.Price;

            _mapper.Map(transDtlDTO, transDtl);
            _mapper.Map(transMaster, transDtl);
            transDtl.ItemMaster = transDtlDTO.ShowItemMaster != null ? transDtlDTO.ShowItemMaster : transDtlDTO.ItemMaster;
            if (transDtl.TransType == 2)
            {
                List<ItemBatch> batchList = await _Context.ItemBatches.Where(obj =>
                    obj.StockCode == transMaster.StockCode && obj.TransN == transMaster.DocNo &&
                    obj.ItemMaster == transDtl.ItemMaster && obj.Serial == transDtl.Serial && obj.FYear == transMaster.FYear)
                    .ToListAsync();
                var sumUnitQty = batchList.Sum(obj => obj.UnitQty);
                var sumPrice = batchList.Sum(obj => obj.Price);
                if (batchList.Count == 0)
                {
                }
                else if (sumUnitQty == transDtl.UnitQty && sumPrice == transDtl.Price)
                {
                    transDtl.ItmBarcode = batchList.Max(obj => obj.ItmBatch);
                }
                else if (sumUnitQty != transDtl.UnitQty || sumPrice != transDtl.Price)
                {
                    List<ItemBatch> itemBatchesToAdd = await _Context.ItemBatches.Where(obj =>
                        obj.StockCode == transMaster.StockCode && obj.TransN == transMaster.DocNo &&
                        obj.ItemMaster == transDtl.ItemMaster && obj.FYear == transMaster.FYear)
                        .ToListAsync();
                    List<ItemBatch> itemBatchesToRemove = await _Context.ItemBatches.Where(obj =>
                        obj.StockCode == transMaster.StockCode && obj.TransN == transMaster.DocNo &&
                        obj.ItemMaster == transDtl.ItemMaster && obj.Serial == transDtl.Serial &&
                        obj.FYear == transMaster.FYear)
                        .ToListAsync();
                    foreach (var item in itemBatchesToAdd)
                    {
                        var itemHistory = _mapper.Map<ItemBatchHistory>(item);
                        await _Context.AddAsync(itemHistory);
                    }
                    foreach (var item in itemBatchesToRemove)
                        _Context.Remove(item);

                    await SaveChangesAsync();
                    await InsertItemBatchAsync(transDtl);
                }
            }
            /////////////////////////////////////////////////
            // Update Receive Form Detail
            else if (transDtl.TransType == 13)
                await UpdateReceiveFormDetailAsync(transDtl);

            await UpdateAsync(transDtl);
            await SaveChangesAsync();
        }
        ////////////////////////////////////////////////////////////////////////////
        // For Not Showing Trans Price
        public async Task<TransDtlDTO> GetNewTransDtlAsync(int transMAsterID)
        {
            TransMaster transMaster = await _Context.TransMasters.FindAsync(transMAsterID);

            var transDtlDTO = new TransDtlDTO();
            _mapper.Map(transMaster, transDtlDTO);

            transDtlDTO.PermissionName = (await _Context.GeneralCodes.FirstOrDefaultAsync(obj =>
                obj.UniqueType == transMaster.TransType)).GDesc;

            List<TransDtl> transDtlList = await _DbSet.Where(obj => obj.TransMAsterID == transMAsterID).ToListAsync();
            transDtlDTO.Serial = transDtlList.Count != 0 ? transDtlList.Max(obj => obj.Serial) + 1 : 1;

            List<SubItem> subItems = await _Context.SubItems.ToListAsync();
            transDtlDTO.SubItems = subItems.Select(obj => new SubItemViewDTO
            {
                SubId = obj.SubId,
                SubName = obj.SubName,
                ItemCode = obj.ItemCode,
                CodeAndName = $"{obj.SubName} / {obj.ItemCode}",
            }).ToList();

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

            transDtlDTO.PermissionName = (await _Context.GeneralCodes.FirstOrDefaultAsync(obj =>
                obj.UniqueType == transDtl.TransType)).GDesc;
            transDtlDTO.ShowItemMaster = transDtlDTO.ItemMaster;

            List<SubItem> subItems = await _Context.SubItems.ToListAsync();
            transDtlDTO.SubItems = subItems.Select(obj => new SubItemViewDTO
            {
                SubId = obj.SubId,
                SubName = obj.SubName,
                ItemCode = obj.ItemCode,
                CodeAndName = $"{obj.SubName} / {obj.ItemCode}",
            }).ToList();

            List<UnitCode> unitCodes = await _Context.UnitCodes.ToListAsync();
            transDtlDTO.UnitCodes = _mapper.Map<List<UnitCodeDTO>>(unitCodes);

            List<UnitCode> itemStatusList = await _Context.UnitCodes.Where(obj => obj.Flag1 == 3).ToListAsync();
            transDtlDTO.ItemStatusList = _mapper.Map<List<UnitCodeDTO>>(itemStatusList);

            return transDtlDTO;
        }

        public async Task<TransDtl> AddTransDtlAsync(TransDtlDTO transDtlDTO)
        {
            TransMaster transMaster = await _Context.TransMasters
                .FirstOrDefaultAsync(obj => obj.TransMAsterID == transDtlDTO.TransMasterID);

            var transDtl = _mapper.Map<TransDtl>(transDtlDTO);
            transDtl.StockCode = 1;
            transDtl.SubCode = 2;
            transDtl.ItemQty = 1;
            transDtl.Price = 0;
            transDtl.ItemUnitQty = 0;
            transDtl.Price2 = 0;
            transDtl.ItemVal2 = 0;
            transDtl.Flag1 = transDtl.Flag1 != null ? transDtl.Flag1 : 1;
            transDtl.ItemMaster2 = 0;
            transDtl.ItemMaster = transDtlDTO.ShowItemMaster != null ? transDtlDTO.ShowItemMaster : transDtlDTO.ItemMaster;
            _mapper.Map(transMaster, transDtl);

            SubItem subItem = await _Context.SubItems.FirstOrDefaultAsync(obj => obj.ItemCode == transDtl.ItemMaster);
            transDtl.Flag1 = subItem.Flag1 != null ? subItem.Flag1 : 1;
            /////////////////////////////////////////////////
            // Add To Receive Form
            if (transDtl.TransType == 13)
                await AddToReceiveFormAsync(transDtl);

            await AddAsync(transDtl);
            await SaveChangesAsync();
            return transDtl;
        }

        public async Task UpdateTransDtlAsync(int id, TransDtlDTO transDtlDTO)
        {
            TransDtl transDtl = await GetByIdAsync(id);

            TransMaster transMaster = await _Context.TransMasters
                .FirstOrDefaultAsync(obj => obj.TransMAsterID == transDtlDTO.TransMasterID);

            _mapper.Map(transDtlDTO, transDtl);
            _mapper.Map(transMaster, transDtl);
            transDtl.ItemMaster = transDtlDTO.ShowItemMaster != null ? transDtlDTO.ShowItemMaster : transDtlDTO.ItemMaster;
            /////////////////////////////////////////////////
            // Update Receive Form Detail
            if (transDtl.TransType == 13)
                await UpdateReceiveFormDetailAsync(transDtl);

            await UpdateAsync(transDtl);
            await SaveChangesAsync();
        }

        public async Task DeleteTransDtlAsync(int id)
        {
            TransDtl transDtl = await GetByIdAsync(id);

            if (transDtl.TransType == 13)
            {
                TransMaster transMaster = await _Context.TransMasters
                    .FirstOrDefaultAsync(obj => obj.TransMAsterID == transDtl.TransMAsterID);

                TransDtl receiveTransDtl = await _DbSet.FirstOrDefaultAsync(obj => obj.TransType == 23 &&
                    obj.DocNoFr == transMaster.DocNo && obj.StockCode == transMaster.StockCode2 &&
                    obj.Serial == transDtl.Serial);

                await DeleteAsync(receiveTransDtl);
            }

            await DeleteAsync(transDtl);
            await SaveChangesAsync();
        }
    }
}
