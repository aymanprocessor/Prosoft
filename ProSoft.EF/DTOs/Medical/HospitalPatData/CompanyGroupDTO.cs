using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class CompanyGroupDTO
    {
        //[DisplayName("Code")]
        //[Required(ErrorMessage = "The field is required")]
        public int GroupId { get; set; }

        //[DisplayName("CompanyGroup Name")]
        //[Required(ErrorMessage = "The field is required")]
        public string GroupName { get; set; }
        public int? BranchId { get; set; }
        public int? Replcate { get; set; }

        //[DisplayName("Activiation")]
        //[Required(ErrorMessage = "The field is required")]
        public int CompGroupOnOff { get; set; }
    }
}
