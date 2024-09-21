using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[Table("STOCK_EMP")]
public partial class StockEmp
{
    [Key]
    public int StockEmpID { get; set; }

    [Column("USER_ID")]
    public int? UserId { get; set; }

    [Column("STKCOD")]
    [ForeignKey("Stock")]
    public int? Stkcod { get; set; }
    
    [Column("TRANS_TYPE")]
    public int? TransType { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("STOCK_DEF")]
    public int? StockDef { get; set; }

    [Column("SHOW_PRICE")]
    public int? ShowPrice { get; set; }

    [Column("MAIN_CODE_STK")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCodeStk { get; set; }

    [Column("SUB_CODE_STK")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCodeStk { get; set; }

    [Column("MAIN_CODE_ACC")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCodeAcc { get; set; }

    [Column("SUB_CODE_ACC")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCodeAcc { get; set; }

    [Column("JORNAL_CODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? JornalCode { get; set; }

    public Stock Stock { get; set; }
}
