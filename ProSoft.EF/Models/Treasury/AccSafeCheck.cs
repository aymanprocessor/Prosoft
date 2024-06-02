using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Shared;

namespace ProSoft.EF.Models.Treasury
{
    [Table("ACC_SAFE_CHECK")]
    public class AccSafeCheck
    {
        [Key]
        [Column("SAFE_CECK_ID")]
        public int SafeCeckId { get; set; }

        [Column("TRAN_TYPE")]
        [StringLength(6)]
        [Unicode(false)]
        public string? TranType { get; set; }

        [Column("CO_CODE")]
        public int? CoCode { get; set; }

        [Column("F_YEAR")]
        public int? FYear { get; set; }

        [Column("DOC_NO")]
        public int? DocNo { get; set; }

        [Column("DOC_DATE")]
        [Precision(6)]
        public DateTime? DocDate { get; set; }

        [Column("PERSON_NAME")]
        [StringLength(50)]
        [Unicode(false)]
        public string? PersonName { get; set; }

        [Column("CHEK_NO")]
        [StringLength(20)]
        [Unicode(false)]
        public string? ChekNo { get; set; }

        [Column("CHEK_DATE")]
        [Precision(6)]
        public DateTime? ChekDate { get; set; }

        [Column("SATTL_DATE")]
        [Precision(6)]
        public DateTime? SattlDate { get; set; }

        [Column("VALUE_PAY", TypeName = "decimal(12, 3)")]
        public decimal? ValuePay { get; set; }

        [Column("COMMENTT")]
        [StringLength(200)]
        [Unicode(false)]
        public string? Commentt { get; set; }

        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("SUB_CODE")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCode { get; set; }

        [Column("FLAG_APR")]
        [StringLength(3)]
        [Unicode(false)]
        public string? FlagApr { get; set; }

        [Column("FLAG_PAY")]
        [StringLength(3)]
        [Unicode(false)]
        public string? FlagPay { get; set; }

        [Column("BANK_MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? BankMainCode { get; set; }

        [Column("BANK_SUB_CODE")]
        [StringLength(6)]
        [Unicode(false)]
        public string? BankSubCode { get; set; }

        [Column("FLAG_PAY_STATUS")]
        [StringLength(6)]
        [Unicode(false)]
        public string? FlagPayStatus { get; set; }

        [Column("CURRENCY_CODE")]
        public int? CurrencyCode { get; set; }

        [Column("CHECK_STATUS")]
        [StringLength(3)]
        [Unicode(false)]
        public string? CheckStatus { get; set; }

        [Column("ACC_NAME")]
        [StringLength(150)]
        [Unicode(false)]
        public string? AccName { get; set; }

        [Column("FLAG_S")]
        [StringLength(1)]
        [Unicode(false)]
        public string? FlagS { get; set; }

        [Column("CUR_SER")]
        public int? CurSer { get; set; }

        [Column("FLAG")]
        public int? Flag { get; set; }

        [Column("RATE1", TypeName = "decimal(7, 4)")]
        public decimal? Rate1 { get; set; }

        [Column("F_MONTH")]
        public int? FMonth { get; set; }

        [Column("CHEK_DATE2")]
        [Precision(6)]
        public DateTime? ChekDate2 { get; set; }

        [Column("DOC_NO1")]
        public int? DocNo1 { get; set; }

        [Column("DOC_NO2")]
        public int? DocNo2 { get; set; }

        [Column("PROFIT_TAX", TypeName = "decimal(9, 3)")]
        public decimal? ProfitTax { get; set; }

        [Column("DISCOUNT_VAL", TypeName = "decimal(12, 3)")]
        public decimal? DiscountVal { get; set; }

        [Column("MAIN_CODE2")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode2 { get; set; }

        [Column("SUB_CODE2")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCode2 { get; set; }

        [Column("ACC_NAME2")]
        [StringLength(70)]
        [Unicode(false)]
        public string? AccName2 { get; set; }

        [Column("ACC_TRANS_NO")]
        public int? AccTransNo { get; set; }

        [Column("ACC_TRANS_TYPE")]
        public int? AccTransType { get; set; }

        [Column("SAFE_CODE")]
        public int? SafeCode { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("SM_NO")]
        public int? SmNo { get; set; }

        [Column("ACC_TRANS_NO2")]
        public int? AccTransNo2 { get; set; }

        [Column("ACC_TRANS_TYPE2")]
        public int? AccTransType2 { get; set; }

        [Column("ACC_TRANS_NO3")]
        public int? AccTransNo3 { get; set; }

        [Column("ACC_TRANS_TYPE3")]
        public int? AccTransType3 { get; set; }

        [Column("CHECK_TYPE")]
        public int? CheckType { get; set; }

        [Column("ENTRY_DATE", TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }

        [Column("BR_REPLC")]
        [StringLength(20)]
        [Unicode(false)]
        public string? BrReplc { get; set; }

        [Column("M_CODE_DTL")]
        public int? MCodeDtl { get; set; }

        [Column("SUB_CODE_BANK")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCodeBank { get; set; }

        [Column("COST_CENTER_CODE")]
        public int? CostCenterCode { get; set; }

        [Column("G_VALUE_PAY", TypeName = "decimal(12, 3)")]
        public decimal? GValuePay { get; set; }

        [Column("USER_CODE")]
        public int? UserCode { get; set; }

        [Column("CSH_ORD_NUM")]
        public int? CshOrdNum { get; set; }


        public ICollection<CustCollectionsDiscount>? custCollectionsDiscounts { get; set; }


        //[Column("JOURNAL_CODE")]
        // public int? JournalCode { get; set; }

        [ForeignKey("AccTransType")]
        [InverseProperty("AccSafeChecks")]
        public JournalType? JournalType { get; set; }

        // [Column("COST_CODE")]
        // public int? CostCode { get; set; }

        [ForeignKey("CostCenterCode")]
        [InverseProperty("AccSafeChecks")]
        public CostCenter? CostCenter { get; set; }

        //[Column("SAFE_CODE")]
        //  public int? SAFE_CODE { get; set; }

        [ForeignKey("SafeCode")]
        [InverseProperty("AccSafeChecks")]
        public SafeName? SafeName { get; set; }

        //[Column("CODE_NO")]
        // public int? CodeNo { get; set; }

        [ForeignKey("CurrencyCode")]
        [InverseProperty("AccSafeChecks")]
        public AccGlobalDef? AccGlobalDef { get; set; }

    }
}
