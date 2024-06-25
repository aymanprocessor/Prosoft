using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Medical.HospitalPatData
{
    [Table("USERS_SECTION")]
    public class UsersSection
    {
        [Key]
        [Column("U_SEC_ID")]
        public int USecId { get; set; }

        [Column("USER_CODE")]
        public int? UserCode { get; set; }

        [Column("CLINIC_ID")]
        public int? ClinicId { get; set; }

        [Column("DEFAULT_ID")]
        public int? DefaultId { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
    }
}
