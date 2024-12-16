using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Stocks
{
    [Table("STENT_DES")]
    public partial class StentDes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Column("STENT_ID")]
        public int? StentId { get; set; }

        [Column("STENT_DESC")]
        [StringLength(100)]
        [Unicode(false)]
        public string? StentDesc { get; set; }

        [Column("STENT_TYPE")]
        public int? StentType { get; set; }
    }
}
