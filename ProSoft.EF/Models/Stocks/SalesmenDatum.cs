using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Stocks
{
    [Table("SALESMEN_DATA")]
    public class SalesmenDatum
    {
        [Key]
        [Column("SALES_MAN_ID")]
        public int SalesManId { get; set; }

        [Column("SM_NO")]
        public int? SmNo { get; set; }

        [Column("SM_NAME")]
        [StringLength(50)]
        [Unicode(false)]
        public string? SmName { get; set; }

        [Column("SM_ADDRESS")]
        [StringLength(100)]
        [Unicode(false)]
        public string? SmAddress { get; set; }

        [Column("SM_TEL")]
        [StringLength(20)]
        [Unicode(false)]
        public string? SmTel { get; set; }

        [Column("SM_MOBIL")]
        [StringLength(20)]
        [Unicode(false)]
        public string? SmMobil { get; set; }

        [Column("E_MAIL")]
        [StringLength(50)]
        [Unicode(false)]
        public string? EMail { get; set; }

        [Column("SM_ACTIVE")]
        [StringLength(2)]
        [Unicode(false)]
        public string? SmActive { get; set; }

        [Column("TARGET", TypeName = "decimal(10, 2)")]
        public decimal? Target { get; set; }

        [Column("SALARY", TypeName = "decimal(10, 3)")]
        public decimal? Salary { get; set; }

        [Column("FLAG")]
        public int? Flag { get; set; }

        [Column("USER_CODE")]
        public int? UserCode { get; set; }
    }
}
