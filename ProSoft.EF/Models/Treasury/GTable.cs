using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Treasury
{
    [Table("G_TABLE")]

    public class GTable
    {
        [Key]
        [Column("G_CODE")]
        public int GCode { get; set; }

        [Column("G_DESC")]
        [StringLength(100)]
        [Unicode(false)]
        public string? GDesc { get; set; }

        [Column("FLAG")]
        public int? Flag { get; set; }

        [Column("G_TYPE")]
        public int? GType { get; set; }

        [Column("G_VALUE", TypeName = "decimal(11, 2)")]
        public decimal? GValue { get; set; }

        public ICollection<AccSafeCash>? AccSafeCashes { get; set; }

    }
}
