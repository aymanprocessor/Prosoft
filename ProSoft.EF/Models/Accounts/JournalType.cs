using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.EF.Models.Accounts;

[Table("JOURNAL_TYPE")]
public partial class JournalType
{
    [Key]
    [Column("JOURNAL_CODE")]
    public int JournalCode { get; set; }

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

    [InverseProperty("JornalCodeNavigation")]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public ICollection<AccSafeCash>? AccSafeCashes { get; set; }


}
