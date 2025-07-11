﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.TransferSuppliesToStocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class PostSuppliesToStocksRepo : IPostSuppliesToStocksRepo
    {
        private readonly AppDbContext _context;


        public PostSuppliesToStocksRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransferSuppliesToStocksMasterDTO>> GetPatAdmissions(int branchId, int RegionId, DateTime FromDate, DateTime ToDate, int? postId)
        {
            List<TransferSuppliesToStocksMasterDTO> transferSuppliesToStocksMasterDTOs = new();
            var patAdmissions = _context.PatAdmissions.Where(p =>
            p.BranchId == branchId &&
            p.PatDateExit.Value.Date >= FromDate.Date &&
            p.PatDateExit.Value.Date <= ToDate.Date &&
            p.SendFr == RegionId &&
            p.PostId == postId
            ).ToList();

            foreach (var patAdmission in patAdmissions)
            {
                TransferSuppliesToStocksMasterDTO reportDTO = new();
                var Doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.DrId == patAdmission.DrCode && d.BranchId == branchId);
                var Pat = await _context.Pats.FirstOrDefaultAsync(p => p.PatId == patAdmission.PatId && p.BranchId == branchId);
                var Company = await _context.Companies.FirstOrDefaultAsync(c => c.CompId == patAdmission.CompId && c.BranchId == branchId);


                reportDTO.MasterId = patAdmission?.MasterId;
                reportDTO.PatId = patAdmission?.PatId;
                reportDTO.Date = patAdmission?.PatAdDate?.Date;
                reportDTO.CompanyName = Company?.CompName;
                reportDTO.PatientName = Pat?.PatName;
                reportDTO.DoctorName = Doctor?.DrDesc;
                reportDTO.ExitDate = patAdmission?.PatDateExit?.Date;
                reportDTO.ExitStatus = patAdmission?.PatClose;
                reportDTO.InvoiceTotal = patAdmission?.MainInvTot;
                reportDTO.InvoiceNo = patAdmission?.MainInvNo;




                transferSuppliesToStocksMasterDTOs.Add(reportDTO);

            }

            return transferSuppliesToStocksMasterDTOs;

        }

        public async Task<List<TransferSuppliesToStocksDetailDTO>> GetClinicTnxs(int branchId, int MasterId, int PatId, int Year)
        {
            List<TransferSuppliesToStocksDetailDTO> transferSuppliesToStocksDetailDTOs = new();

            var ClinicTnxs = _context.ClinicTrans.Where(
                c =>
                c.MasterId == MasterId &&
                c.BranchId == branchId &&
                c.ExYear == Year &&
                c.ItmServFlag == 2 &&
                c.PipeFlag == null &&
                c.PatId == PatId).ToList();

            foreach (var ClinicTnx in ClinicTnxs)
            {

                var SubItem = await _context.SubItems.FirstOrDefaultAsync(s => s.ItemCode == ClinicTnx.ItemMaster);
                var Stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Stkcod == ClinicTnx.StockCode);

                TransferSuppliesToStocksDetailDTO reportDTO = new()
                {
                    Date = ClinicTnx?.ExDate?.Date,
                    ItemCode = ClinicTnx?.ItemMaster,
                    ItemName = SubItem?.SubName,
                    Qty = ClinicTnx?.Qty,
                    StockName = Stock?.Stknam,
                    Cost = ClinicTnx?.Qty * ClinicTnx?.UnitPrice,
                    Sell = ClinicTnx?.ValueService,
                    PermissionNo = ClinicTnx?.SerSys
                };

                transferSuppliesToStocksDetailDTOs.Add(reportDTO);


            }

            return transferSuppliesToStocksDetailDTOs;
        }

        public async Task TransferSuppliesToStocks(List<TransferSuppliesToStocksAJAXReqDTO> rows, int branchId, int year, int userCode)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var row in rows)
                {
                    var patAd = await _context.PatAdmissions.FirstOrDefaultAsync(p => p.PatId == row.patId && p.MasterId == row.masterId);

                    var clinicTnxs = _context.ClinicTrans.Where(c =>
                        c.MasterId == row.masterId &&
                        c.PatId == row.patId &&
                        c.ItmServFlag == 2
                    ).ToList();

                    decimal? totalValue = 0;

                    var newDocNo = (await _context.TransMasters.MaxAsync(t => t.DocNo)) + 1;

                    var newTransMstr = await _context.TransMasters.AddAsync(new TransMaster
                    {
                        BranchId = branchId,
                        TransType = 5,
                        DocNo = newDocNo,
                        SerSys = newDocNo,
                        DocDate = DateTime.Now,
                        Flag1 = 2,
                        FYear = year,
                        UserCode = userCode,

                    });
                    await _context.SaveChangesAsync();

                    foreach (var clinicTnx in clinicTnxs)
                    {

                        await _context.TransDtls.AddAsync(new TransDtl
                        {
                            ItemMaster = clinicTnx?.ItemMaster,
                            MainCode = clinicTnx?.MainCode,
                            SubCode = clinicTnx?.SubCode != null ? int.Parse(clinicTnx?.SubCode) : null,
                            UnitQty = clinicTnx?.Qty,
                            Price = clinicTnx?.UnitPrice,
                            ItemVal = clinicTnx?.Qty + clinicTnx?.UnitPrice,
                            StockCode = (short)clinicTnx?.StockCode,
                            FYear = year,
                            DocNo = newDocNo,
                            SerSys = newDocNo,
                            DocDate = DateTime.Now,
                            Flag1 = 2,
                            UserCode = userCode,
                            TransType = 5,
                            TransMAsterID = newTransMstr.Entity.TransMAsterID
                        });
                        newTransMstr.Entity.StockCode = (short)clinicTnx?.StockCode;

                        totalValue += clinicTnx?.Qty + clinicTnx?.UnitPrice;

                        clinicTnx.PostId = 1;
                        clinicTnx.SerSys = (int)newDocNo;
                        _context.Entry(clinicTnx).State = EntityState.Modified;

                        await _context.SaveChangesAsync();
                    }

                    newTransMstr.Entity.TotTransVal = totalValue;

                    patAd.PostId = 1;
                    _context.Entry(patAd).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                // Commit transaction after successful processing
                await transaction.CommitAsync();
                //await transaction.RollbackAsync();
            }
            catch (Exception)
            {
                // Rollback transaction in case of an error
                await transaction.RollbackAsync();
                throw; // Re-throw exception to handle it in the caller
            }
        }


        public async Task CancelTransferSuppliesToStocks(List<TransferSuppliesToStocksAJAXReqDTO> rows, int branchId, int year, int userCode)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var row in rows)
                {
                    var patAd = await _context.PatAdmissions.FirstOrDefaultAsync(p => p.PatId == row.patId && p.MasterId == row.masterId);

                    var SerSyss = _context.ClinicTrans.Where(c =>
                        c.MasterId == row.masterId &&
                        c.PatId == row.patId &&
                        c.ItmServFlag == 2)
                        .GroupBy(c => c.SerSys)
                        .Select(c => c.Key)
                        .ToList();

                    foreach (var SerSys in SerSyss)
                    {
                        var tnxDtls = await _context.TransDtls.Where(t => t.SerSys == SerSys && t.DocNo == SerSys).ToListAsync();
                        if (tnxDtls != null)
                        {

                            _context.TransDtls.RemoveRange(tnxDtls);

                        }
                        var tnxMstr = await _context.TransMasters.FirstOrDefaultAsync(t => t.SerSys == SerSys && t.DocNo == SerSys);
                        if (tnxMstr != null)
                        {
                            _context.TransMasters.Remove(tnxMstr);

                        }


                        await _context.ClinicTrans.Where(c =>
                       c.MasterId == row.masterId &&
                       c.PatId == row.patId &&
                       c.ItmServFlag == 2 &&
                       c.PostId == 1 &&
                       c.SerSys == SerSys).ExecuteUpdateAsync(c => c
                       .SetProperty(c => c.PostId, c => null)
                       .SetProperty(c => c.SerSys, c => null));


                        await _context.PatAdmissions.Where(p => p.PatId == row.patId && p.MasterId == row.masterId)
                            .ExecuteUpdateAsync(p => p.SetProperty(p => p.PostId, p => null));

                        await _context.SaveChangesAsync();

                    }


                }

                // Commit transaction after successful processing
                await transaction.CommitAsync();
                //await transaction.RollbackAsync();
            }
            catch (Exception)
            {
                // Rollback transaction in case of an error
                await transaction.RollbackAsync();
                throw; // Re-throw exception to handle it in the caller
            }
        }

    }
}
