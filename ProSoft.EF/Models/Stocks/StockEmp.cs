using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[PrimaryKey("UserId", "Stkcod", "TransType", "StockDef")]
[Table("STOCK_EMP")]
public partial class StockEmp
{
    [Key]
    [Column("USER_ID")]
    public int UserId { get; set; }

    [Key]
    [Column("STKCOD")]
    public int Stkcod { get; set; }

    [Key]
    [Column("TRANS_TYPE")]
    public int TransType { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Key]
    [Column("STOCK_DEF")]
    public int StockDef { get; set; }

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
}
