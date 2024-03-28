using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models
{
    public class AppUser : IdentityUser
    {
        [Column("USER_CODE")]
        public int UserCode { get; set; }

        [Column("USER_CONTROLER")]
        public int? UserControler { get; set; }

        //[Unicode(false)]
        [Column("PASS_WORD")]
        public string? PassWord { get; set; }

        //[Unicode(false)]
        [Column("PASS_CONFIRM")]
        public string? PassConfirm { get; set; }

        [Column("USER_G_ID")]
        public int? UserGId { get; set; }

        [Column("USER_LANG")]
        public int? UserLang { get; set; }

        [Column("SIDE_ID")]
        public double? SideId { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("USER_TYPE")]
        public int? UserType { get; set; }

        [Column("F_YEAR")]
        public int? FYear { get; set; }

        [Column("DISCOUNT_ID")]
        public double? DiscountId { get; set; }
    }
}
