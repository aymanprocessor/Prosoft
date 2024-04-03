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
    public class EisSectionTypeDTO
    {
        [DisplayName("Code")]
        public int SecCode { get; set; }

        [DisplayName("System Section")]
        public string SecName { get; set; }
    }
}
