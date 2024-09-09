using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class UserSideEditAddDTO
    {
        [DisplayName("The User")]
        public int UserId { get; set; }
        public int OrgUserId { get; set; }

        [DisplayName("Side Name")]
        public int SideId { get; set; }
        [DisplayName("Organization Sections")]
        public int RegionId { get; set; }
        public int OrgRegionId { get; set; }
        public int BranchId { get; set; }
        [DisplayName("User Group")]
        public int UserGroupId { get; set; }
        [DisplayName("User Page")]
        public int Flag { get; set; }
        [DisplayName("System Sections")]
        public int EisSectionTypeId { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> UserGroups { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Regions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> EisSectionTypes { get; set; } = Enumerable.Empty<SelectListItem>();
        


    }

}
