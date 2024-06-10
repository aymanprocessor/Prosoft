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
    [Table("SYSTEM_TABLE")]
    public class SystemTable
    {
        [Key]
        [Column("SYS_ID")]
        public int SysId { get; set; }

        [Column("SYS_VALUE")]
        public int? SysValue { get; set; }

        [Column("SYS_DEC")]
        [StringLength(100)]
        [Unicode(false)]
        public string? SysDec { get; set; }

        [Column("SYS_TYPE")]
        public int? SysType { get; set; }
    }
}
