using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class TransMasterEditAddDTO
    {
        public int TransMAsterID { get; set; }
        public short StockCode { get; set; }
        public int TransType { get; set; }
        public long? DocNo { get; set; }
        public string? DocNo2 { get; set; }

        [DisplayName("Permission Date")]
        [Required(ErrorMessage = "The field is required")]
        public DateTime? DocDate { get; set; }

        [DisplayName("Supplier Invoice No")]
        public string? SupInvNo { get; set; }

        [DisplayName("Supplier Invoice Date")]
        public DateTime? SupInvDate { get; set; }

        [DisplayName("Lot Number")]
        public string? LotNo { get; set; }

        [DisplayName("Purchase No")]
        public string? RefDocNo { get; set; }

        [DisplayName("The Supplier")]
        public string? SupNo { get; set; }

        [DisplayName("Discount Percentage %")]
        public decimal? DiscPers { get; set; }

        [DisplayName("Discount Value")]
        public decimal? Descount { get; set; }

        [DisplayName("Discount/Addition")]
        public decimal? CustDisc2 { get; set; }

        [DisplayName("Value")]
        public decimal? CustDisc1 { get; set; }

        [DisplayName("Prices Include Tax")]
        public short? PriceIncTax { get; set; }

        [DisplayName("Permission Approval")]
        public string? Flag3 { get; set; }

        [DisplayName("Payment Type")]
        public string? Pay { get; set; }

        [DisplayName("Purchases Value")]
        public decimal? TotTransVal { get; set; }

        [DisplayName("Sales Tax Percentage %")]
        public decimal? TaxValue { get; set; }

        [DisplayName("Sales Tax Value")]
        public decimal? TaxPrc { get; set; }

        [DisplayName("Due Value")]
        public decimal? DueValue { get; set; }
        public string? CustNo { get; set; }
        public string? MainCode { get; set; }
        public string? Remark { get; set; }
        public string? StatusBal { get; set; }
        public string? Flag { get; set; }
        public decimal? TotWeight { get; set; }
        public int? FYear { get; set; }
        public string? Flag2 { get; set; }
        public decimal? TotTransVal2 { get; set; }
        public decimal? AmountVisa { get; set; }
        public decimal? CashAmount { get; set; }
        public string? SaleStatus { get; set; }
        public decimal? AddPers { get; set; }
        public int? UserCode { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? CustDisc3 { get; set; }
        public decimal? CustDisc4 { get; set; }
        public decimal? CustDisc5 { get; set; }
        public long? InvNo { get; set; }
        public int? SmNo { get; set; }
        public short? StockCode2 { get; set; }
        public int? DueMonth { get; set; }
        public int? DueYear { get; set; }
        public DateTime? RefDocDate { get; set; }
        public string? InvType { get; set; }
        public long? AccTransNo { get; set; }
        public string? AccTransType { get; set; }
        public int? FMonth { get; set; }
        public int? Flag1 { get; set; }
        public int? SideId { get; set; }
        public int? BranchId { get; set; }
        public string? RecChecksNo { get; set; }
        public string? SupplyOrderNo { get; set; }
        public string? LcNo { get; set; }
        public int? MsgCode { get; set; }
        public int? UserConfirm { get; set; }
        public long? SerSys { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public int? TransConfirm { get; set; }
        public int? ShowRow { get; set; }
        public DateTime? EndDate { get; set; }
        public int? OperationType { get; set; }
        public string? AttachFile { get; set; }
        public int? SmNo2 { get; set; }
        public int? PrcSm1 { get; set; }
        public int? PrcSm2 { get; set; }
        public int? Quote { get; set; }
        public string? ContactPerson { get; set; }
        public int? Replcate { get; set; }
        public string? BrReplc { get; set; }
        public long? DocNoFr { get; set; }
        public string? TaxBase { get; set; }
        public string? DiscountBase { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? SendFr { get; set; }
        /////////////////////////////////////////////////
        public string? MonthName { get; set; }
        public int? PermissionsCount { get; set; }
        public string? StockName { get; set; }
        public string? PermissionName { get; set; }
        public string? UserName { get; set; }

        public List<SupCodeViewDTO>? Suppliers {  get; set; }
    }
}
