using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Shared;

namespace ProSoft.EF.Models.Stocks;
[PrimaryKey("UserId", "Stkcod", "BranchId", "KID")]
[Table("STOCK_EMP_FLAG")]
public partial class StockEmpFlag
{
    
    [Key]
    [Column("USER_ID")]
    public int UserId { get; set; }

    [Key]
    [Column("STKCOD")]
    public int Stkcod { get; set; }
    public Stock Stock { get; set; } = default!;


    [Key]
    [Column("BRANCH_ID")]
    public int BranchId { get; set; }

    public Branch Branch { get; set; } = default!;

    [Key]
    [Column("K_ID")]
    public int KID { get; set; }

    public KindStore KindStore { get; set; } = default!;



}
