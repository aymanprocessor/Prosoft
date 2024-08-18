using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class OpenColseTransactionRepo : IOpenColseTransactionRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public OpenColseTransactionRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<List<PermissionDefViewDTO>> GetPermissionsAsync()
        {
            List<GeneralCode> permissions = await _Context.GeneralCodes.Where(obj => obj.GType == "4" && obj.ShowHide == 1).ToListAsync();
            var permissionDTOs = _mapper.Map<List<PermissionDefViewDTO>>(permissions);
            return permissionDTOs;
        }
        public async Task<string> OpenAsync(int stockCode, int permission, int? fromVoucher, int? toVoucher, DateTime? fromPeriod, DateTime? toPeriod, int closOrCanc)
        {
            if (fromVoucher != null && toVoucher != null)
            {
                List<TransMaster> TransMasters = await _Context.TransMasters.Where(obj => obj.TransType == permission && obj.StockCode == stockCode
                        && (obj.DocNo >= fromVoucher && obj.DocNo <= toVoucher)).ToListAsync();

                foreach (var master in TransMasters)
                {
                    AccTransMaster accTransMaster = await _Context.AccTransMasters.FirstOrDefaultAsync(obj => obj.TransType == master.TransType.ToString() && obj.TransNo == master.AccTransNo);
                    if (accTransMaster.OkPost == "1")
                    {
                        return "Monthly Closing Done";//غير مسموح باقفال القيد بالحسابات
                    }
                    master.Flag3 = "1";
                }

                await _Context.SaveChangesAsync();

            }
            else if (fromPeriod != null && toPeriod != null)
            {
                if (fromPeriod.Value.Year != toPeriod.Value.Year)
                {
                    return "Incorrect Year";
                }
                List<TransMaster> TransMasters = await _Context.TransMasters.Where(obj => obj.TransType == permission && obj.StockCode == stockCode
                       && (obj.DocDate >= fromPeriod && obj.DocDate <= toPeriod)).ToListAsync();

                foreach (var master in TransMasters)
                {
                    AccTransMaster accTransMaster = await _Context.AccTransMasters.FirstOrDefaultAsync(obj => obj.TransType ==master.TransType.ToString()&&obj.TransNo ==master.AccTransNo );
                    if (accTransMaster != null)
                    {
                        if (accTransMaster.OkPost == "1")
                        {
                            return "Monthly Closing Done";//غير مسموح باقفال القيد بالحسابات
                        }
                    }
                   
                    master.Flag3 = "1";
                }

                await _Context.SaveChangesAsync();
            }
            return "Transactions Opening Done";

        }
        public async Task<string> CloseAsync(int stockCode, int permission, int? fromVoucher, int? toVoucher, DateTime? fromPeriod, DateTime? toPeriod, int closOrCanc)
        {
            if (fromVoucher != null && toVoucher != null)
            {
                List<TransMaster> TransMasters = await _Context.TransMasters.Where(obj => obj.TransType == permission && obj.StockCode == stockCode
                        && (obj.DocNo >= fromVoucher && obj.DocNo <= toVoucher)).ToListAsync();

                foreach (var master in TransMasters)
                {
                    master.Flag3 = "2";
                }

                await _Context.SaveChangesAsync();

            }
            else if (fromPeriod != null && toPeriod != null)
            {
                if (fromPeriod.Value.Year != toPeriod.Value.Year)
                {
                    return "Incorrect Year";
                }
                List<TransMaster> TransMasters = await _Context.TransMasters.Where(obj => obj.TransType == permission && obj.StockCode == stockCode
                       && (obj.DocDate >= fromPeriod && obj.DocDate <= toPeriod)).ToListAsync();

                foreach (var master in TransMasters)
                {
                    master.Flag3 = "2";
                }

                await _Context.SaveChangesAsync();
            }
            return "Transactions Opening Done";
        }
    }
}
