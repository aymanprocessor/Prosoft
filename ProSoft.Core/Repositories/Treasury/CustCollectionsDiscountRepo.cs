using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
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
        public async Task<List<CustCollectionsDiscountViewDTO>> GetAllCustCollectionsDiscountAsync(int id, string docType)
        {
            List<CustCollectionsDiscount> custCollectionsDiscounts = await _DbSet.Where(obj=>obj.SafeCashId == id && obj.DocType ==docType).ToListAsync();
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


        public async Task<CustCollectionsDiscountEditAddDTO> GetEmptycustCollectionsDiscountAsync(int id, string docType)
        {
            var custCollectionsDiscountDTO = new CustCollectionsDiscountEditAddDTO();

            List<AccMainCode> mainAccCodes = await _Context.AccMainCodes.ToListAsync();

            custCollectionsDiscountDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(mainAccCodes);
            //get value pay
            if (docType == "SFSIN")
            {
                var accsafeCheck = await _Context.AccSafeChecks.FirstOrDefaultAsync(obj => obj.SafeCeckId == id && obj.TranType == docType);
                custCollectionsDiscountDTO.ValuePay = accsafeCheck.ValuePay;
            }
            else if (docType == "SFOUT")
            {
                var accsafeCheck = await _Context.AccSafeChecks.FirstOrDefaultAsync(obj => obj.SafeCeckId == id && obj.TranType == docType);
                custCollectionsDiscountDTO.ValuePay = accsafeCheck.ValuePay;
            }
            else
            {             
                var accSafeCash = await _Context.AccSafeCashes.FirstOrDefaultAsync(obj => obj.SafeCashId == id && obj.DocType ==docType);
                custCollectionsDiscountDTO.ValuePay = accSafeCash.ValuePay;
            }
            return custCollectionsDiscountDTO;
        }

        public async Task AddcustCollectionsDiscountAsync(int id,CustCollectionsDiscountEditAddDTO custCollectionsDiscountDTO)
        {
            var custCollectionsDiscount = _mapper.Map<CustCollectionsDiscount>(custCollectionsDiscountDTO);
            if (custCollectionsDiscount.DocType == "SFCIN") 
            {
                var accsafeCashe = await _Context.AccSafeCashes.FirstOrDefaultAsync(obj => obj.SafeCashId == id && obj.DocType == custCollectionsDiscountDTO.DocType);
                custCollectionsDiscount.ReceiptNo = accsafeCashe.DocNo;
                custCollectionsDiscount.ReceiptDate = accsafeCashe.DocDate;
                custCollectionsDiscount.FYear = accsafeCashe.FYear;
                custCollectionsDiscount.DocType = accsafeCashe.DocType;
                custCollectionsDiscount.SafeCode = accsafeCashe.SafeCode;
            }
            else if (custCollectionsDiscount.DocType == "SFCOT")
            {
                var accsafeCashe = await _Context.AccSafeCashes.FirstOrDefaultAsync(obj => obj.SafeCashId == id && obj.DocType == custCollectionsDiscountDTO.DocType);
                custCollectionsDiscount.ReceiptNo = accsafeCashe.DocNo;
                custCollectionsDiscount.ReceiptDate = accsafeCashe.DocDate;
                custCollectionsDiscount.FYear = accsafeCashe.FYear;
                custCollectionsDiscount.DocType = accsafeCashe.DocType;
                custCollectionsDiscount.SafeCode = accsafeCashe.SafeCode;
            }
            else if (custCollectionsDiscount.DocType == "SFTIN")
            {
                var accsafeCashe = await _Context.AccSafeCashes.FirstOrDefaultAsync(obj => obj.SafeCashId == id && obj.DocType == custCollectionsDiscountDTO.DocType);
                custCollectionsDiscount.ReceiptNo = accsafeCashe.DocNo;
                custCollectionsDiscount.ReceiptDate = accsafeCashe.DocDate;
                custCollectionsDiscount.FYear = accsafeCashe.FYear;
                custCollectionsDiscount.DocType = accsafeCashe.DocType;
                custCollectionsDiscount.SafeCode = accsafeCashe.SafeCode;
            }
            else if (custCollectionsDiscount.DocType == "SFTOT")
            {
                var accSafeCash = await _Context.AccSafeCashes.FirstOrDefaultAsync(obj => obj.SafeCashId == id);
                custCollectionsDiscount.ReceiptNo = accSafeCash.DocNo;
                custCollectionsDiscount.ReceiptDate = accSafeCash.DocDate;
                custCollectionsDiscount.FYear = accSafeCash.FYear;
                custCollectionsDiscount.DocType = accSafeCash.DocType;
                custCollectionsDiscount.SafeCode = accSafeCash.SafeCode;
                await _Context.AddAsync(custCollectionsDiscount);
                await _Context.SaveChangesAsync();
                //تحول للاستلام
                CustCollectionsDiscount custCollectionsDiscountForRecieve = _mapper.Map<CustCollectionsDiscount>(custCollectionsDiscount);
                custCollectionsDiscountForRecieve.DocType = "SFTIN";
                custCollectionsDiscountForRecieve.SafeCode = accSafeCash.SafeCode2;
                custCollectionsDiscountForRecieve.SafeCashId = id-1;
                custCollectionsDiscountForRecieve.DiscountCode = null;
                await _Context.AddAsync(custCollectionsDiscountForRecieve);
                await _Context.SaveChangesAsync();
            }
            if (custCollectionsDiscount.DocType == "SFCIN" || custCollectionsDiscount.DocType == "SFCOT" || custCollectionsDiscount.DocType == "SFTIN")
            {
                await _Context.AddAsync(custCollectionsDiscount);
                await _Context.SaveChangesAsync();    
            }
        }
        public async  Task<CustCollectionsDiscountEditAddDTO> GetcustCollectionsDiscountByIdAsync(int id)
        {
            CustCollectionsDiscount custCollectionsDiscount = await _Context.custCollectionsDiscounts
                .FirstOrDefaultAsync(obj=>obj.DiscountCode ==id);
            var custCollectionsDiscountDTO = _mapper.Map<CustCollectionsDiscountEditAddDTO>(custCollectionsDiscount);
            
            List<AccMainCode> mainAccCodes = await _Context.AccMainCodes.ToListAsync();
            custCollectionsDiscountDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(mainAccCodes);
            //get value pay
            if (custCollectionsDiscount.DocType == "SFSIN")
            {
                var accsafeCheck = await _Context.AccSafeChecks.FirstOrDefaultAsync(obj => obj.SafeCeckId == custCollectionsDiscount.SafeCashId && obj.TranType == custCollectionsDiscount.DocType);
                custCollectionsDiscountDTO.ValuePay = accsafeCheck.ValuePay;
            }
            else
            {
                var accSafeCash = await _Context.AccSafeCashes.FirstOrDefaultAsync(obj => obj.SafeCashId == custCollectionsDiscount.SafeCashId && obj.DocType == custCollectionsDiscount.DocType);
                custCollectionsDiscountDTO.ValuePay = accSafeCash.ValuePay;
            }

            return custCollectionsDiscountDTO;
        }

        public async Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode)
        {
            List<AccSubCode> subAccCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == mainAccCode).ToListAsync();
            var subAccCodesDTO = _mapper.Map<List<AccSubCodeDTO>>(subAccCodes);
            return subAccCodesDTO;
        }

        public async Task EditcustCollectionsDiscountAsync(int id, CustCollectionsDiscountEditAddDTO custCollectionsDiscountDTO)
        {
            CustCollectionsDiscount custCollectionsDiscount = _DbSet.Find(id);
            _mapper.Map(custCollectionsDiscountDTO, custCollectionsDiscount);
            _Context.Update(custCollectionsDiscount);
            await _Context.SaveChangesAsync();
            if (custCollectionsDiscountDTO.DocType =="SFTOT")
            {
                CustCollectionsDiscount custCollectionsDiscountForRecieve = _DbSet.Find(id+1);
                custCollectionsDiscountForRecieve.DiscPrc = custCollectionsDiscountDTO.DiscPrc;
                custCollectionsDiscountForRecieve.DiscValue = custCollectionsDiscountDTO.DiscValue;
               // custCollectionsDiscountForRecieve.DocType = "SFTIN";
               //custCollectionsDiscountForRecieve.SafeCode = accSafeCash.SafeCode2; //عشان اعرض في الاستلام الخزينة اللي هيتحول ليها
                _Context.Update(custCollectionsDiscountForRecieve);
                await _Context.SaveChangesAsync();
            }
            
        }
    }
}
