using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Stocks
{
    [Table("USERS_GROUP")]

    public class UsersGroup
    {
        [Key]
        [Column("G_ID")]
        public int G_ID { get; set; }
        [Column("G_NAME")]

        public string G_NAME { get; set; }
        [Column("FLAG")]

        public bool FLAG { get; set; }
    }
}
