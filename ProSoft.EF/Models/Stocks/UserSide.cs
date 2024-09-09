using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Stocks
{
    [Table("USER_SIDE")]
    public partial class UserSide
    {
        [Key]
        [Column("USER_CODE")]
        public int UserId { get; set; }
        [Key]
        [Column("SIDE_ID")]
        public int SideId { get; set; }
        [Key]
        [Column("DEPT_ID")]
        public int RegionId { get; set; }
        [Key]
        [Column("BRANCH_ID")]
        public int BranchId { get; set; }

        [Column("USER_G_ID")]
        public int UserGroupId { get; set; }
        [Column("FLAG1")]
        public int Flag { get; set; }
        [Column("EIS_SYS_ID")]
        public int EisSectionTypeId { get; set; }

        
        public Side Sides { get; set; } = default!;
       
        public AppUser Users { get; set; } = default!;
     
        public Branch Branchs { get; set; } = default!;
      
        public UsersGroup UserGroups { get; set; } = default!;
       
        public Region Regions { get; set; } = default!;
      
        public EisSectionType EisSectionTypes { get; set; } = default!;
    }
}
