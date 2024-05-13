using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Accounts;

namespace ProSoft.EF.Models.Treasury;

[Table("SAFE_NAME")]
public partial class SafeName
{
    [Key]
    [Column("SAFE_CODE")]
    public int SafeCode { get; set; }

    [Column("SAFE_NAMES")]
    [StringLength(100)]
    [Unicode(false)]
    public string? SafeNames { get; set; }

    [Column("FLAG1")]
    public int? Flag1 { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("POST_ACCOUNT")]
    public int? PostAccount { get; set; }

    [Column("JE_DOC_TYP")]
    [StringLength(5)]
    [Unicode(false)]
    public string? JeDocTyp { get; set; }

    [Column("USER_CD")]
    public int? UserCd { get; set; }

    [Column("USER_MODIFY_CD")]
    public int? UserModifyCd { get; set; }

    [Column("ENTRY_DATE", TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [Column("MODIFY_DATE", TypeName = "datetime")]
    public DateTime? ModifyDate { get; set; }
    public ICollection<AccSafeCash>? AccSafeCashes { get; set; }

}
