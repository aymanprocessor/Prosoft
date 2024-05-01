using ProSoft.EF.DTOs.Calculus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Shared
{
    public class EisPostingEditAddDTO
    {
        public int PostId { get; set; }

        [DisplayName("The Name")]
        [Required(ErrorMessage = "The field is required")]
        public string ModuleN { get; set; }
        
        [DisplayName("Main Code")]
        [Required(ErrorMessage = "The field is required")]
        public string MainCode { get; set; }
        
        [DisplayName("Sub Code")]
        public string? SubCode { get; set; }

        public int? BranchId { get; set; }
        public List<AccMainCodeDTO>? MainCodes { get; set; }
        public List<AccSubCodeDTO>? SubCodes { get; set; }
    }
}
