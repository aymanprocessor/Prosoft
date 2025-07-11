﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Accounts;

[PrimaryKey("CoCode", "SubCode", "MainCode")]
[Table("ACC_SUB_CODE")]
public partial class AccSubCode
{
    [Key]
    [Column("CO_CODE")]
    public int CoCode { get; set; }

    [Key]
    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string SubCode { get; set; } = null!;


    [Key]
    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string MainCode { get; set; } = null!;

    [Column("SUB_NAME")]
    [StringLength(80)]
    [Unicode(false)]
    public string? SubName { get; set; }

    [Column("CUST_SALES_NO")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CustSalesNo { get; set; }

    [Column("KEY_CODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? KeyCode { get; set; }

    [Column("COST_CENTER")]
    public int? CostCenter { get; set; }

    [Column("ITEM_UNIQUE")]
    [StringLength(19)]
    [Unicode(false)]
    public string? ItemUnique { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

  //  public ICollection<AccSafeCash>? AccSafeCashes { get; set; }

}
