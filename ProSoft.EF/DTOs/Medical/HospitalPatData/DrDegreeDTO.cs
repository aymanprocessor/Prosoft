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
    public class DrDegreeDTO
    {
        [DisplayName("code")]        
        public int DegreeId { get; set; }

        [DisplayName("Degree Name")]
        public string DegreeDesc { get; set; }
        public int? BranchId { get; set; }

    }
}
