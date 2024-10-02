using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Stocks
{
    [Table("EXPENSE_DATA")]
    public class ExpenseData
    {
        [Key]
        [Column("EXP_CODE")]
        public int Id {  get; set; }
        [Column("EXP_NAME")]
        [StringLength(50)]
        public string Name {  get; set; }
        [Column("EXP_VALUE",TypeName ="decimal(10,3)")]

        public float ExpenseValue {  get; set; }
    }
}
