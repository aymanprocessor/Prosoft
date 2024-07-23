using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Shared
{
    [Table("ACC_MONTH")]
    public class AccMonth
    {
        [Key]
        [Column("CUR_MONTH")]
        public int? CurMonth { get; set; }

        [Column("DESC_MONTH")]
        [StringLength(15)]
        [Unicode(false)]
        public string? DescMonth { get; set; }
    }
}
