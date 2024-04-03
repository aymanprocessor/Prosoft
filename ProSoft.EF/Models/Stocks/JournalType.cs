using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[Table("JOURNAL_TYPE")]
public partial class JournalType
{
    [Key]
    [Column("JOURNAL_CODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string JournalCode { get; set; } = null!;

    [Column("JOURNAL_NAME")]
    [StringLength(40)]
    [Unicode(false)]
    public string? JournalName { get; set; }

    [Column("COUNTER_FLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CounterFlag { get; set; }

    [Column("JOURNAL_TYPE")]
    [StringLength(3)]
    [Unicode(false)]
    public string? JournalType1 { get; set; }

    [Column("JOURNAL_IN_OUT")]
    [StringLength(5)]
    [Unicode(false)]
    public string? JournalInOut { get; set; }
}
