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
        
        public int GroupId { get; set; }

     
        public string GroupName { get; set; }
        public int? BranchId { get; set; }
        public int? Replcate { get; set; }

        public int CompGroupOnOff { get; set; }
    }
}
