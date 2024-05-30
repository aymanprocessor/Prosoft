using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Shared
{
    [Table("EIS_USER_OBJECT")]
    public class EisUserObject
    {
        [Key]
        [Column("EIS_USER_OBJ_ID")]
        public int EisUserObjId { get; set; }

        [Column("USER_ID")]
        public int? UserId { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("OBJECT_NAME")]
        [StringLength(150)]
        [Unicode(false)]
        public string? ObjectName { get; set; }

        [Column("DW_MASTER")]
        [StringLength(150)]
        [Unicode(false)]
        public string? DwMaster { get; set; }

        [Column("DW_DETAIL")]
        [StringLength(150)]
        [Unicode(false)]
        public string? DwDetail { get; set; }

        [Column("DEFULT_ID")]
        public int? DefultId { get; set; }

        [Column("W_DESC")]
        [StringLength(150)]
        [Unicode(false)]
        public string? WDesc { get; set; }

        [Column("U_INS")]
        public int? UIns { get; set; }

        [Column("U_DEL")]
        public int? UDel { get; set; }

        [Column("U_UPD")]
        public int? UUpd { get; set; }

        [Column("COLUMN_NAM")]
        [StringLength(50)]
        [Unicode(false)]
        public string? ColumnNam { get; set; }
    }
}
