using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Shared;

[Table("GENERAL_CODE")]
public partial class GeneralCode
{
    [Key]
    [Column("G_ID")]
    public int GId { get; set; }

    [Column("G_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? GDesc { get; set; }

    [Column("G_TYPE")]
    [StringLength(2)]
    [Unicode(false)]
    public string GType { get; set; } = null!;

    [Column("TRANS_TYPE")]
    public int? TransType { get; set; }

    [Column("SHOW_HIDE")]
    public int? ShowHide { get; set; }

    [Column("ADD_SUB")]
    public int? AddSub { get; set; }

    [Column("DW_MASTER")]
    [StringLength(50)]
    [Unicode(false)]
    public string? DwMaster { get; set; }

    [Column("DW_DTL")]
    [StringLength(50)]
    [Unicode(false)]
    public string? DwDtl { get; set; }

    [Column("TRANS_DESC_USER")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TransDescUser { get; set; }

    [Column("UNIQUE_TYPE")]
    public int? UniqueType { get; set; }

    [Column("APP_TYPE")]
    public int? AppType { get; set; }

    [Column("CRM_FLAG")]
    public int? CrmFlag { get; set; }

    [Column("CUSTOMER_ID")]
    public int? CustomerId { get; set; }

    [Column("TRANS_SORT")]
    public int? TransSort { get; set; }

    [Column("DEPT_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? DeptName { get; set; }

    [Column("FORM_TYPE")]
    public int? FormType { get; set; }
}
