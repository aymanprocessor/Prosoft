using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Shared
{
    public class PermissionDefEditAddDTO
    {
        [DisplayName("Code")]
        public int GId { get; set; }

        [DisplayName("Unique Code")]
        [Required(ErrorMessage = "The field is required")]
        public int UniqueType { get; set; }

        [DisplayName("System Description")]
        [Required(ErrorMessage = "The field is required")]
        public string GDesc { get; set; }

        [DisplayName("User Description")]
        [Required(ErrorMessage = "The field is required")]
        public string TransDescUser { get; set; }

        [DisplayName("Depends On")]
        public int? TransType { get; set; }

        [DisplayName("Is Active")]
        [Required(ErrorMessage = "The field is required")]
        public int ShowHide { get; set; }

        [DisplayName("Sell Or Buy")]
        [Required(ErrorMessage = "The field is required")]
        public int AddSub { get; set; }

        [DisplayName("Permission Type")]
        [Required(ErrorMessage = "The field is required")]
        public int FormType { get; set; }

        public List<PermissionDefViewDTO>? Permissions { get; set; }
    }
}
