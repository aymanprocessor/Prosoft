using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProSoft.EF.DTOs.Medical.Analysis
{
    public class LabUnitDTO
    {
        [DisplayName("Unit Code")]
        public short LabUnitCode { get; set; }

        [DisplayName("Unit Name")]
        public string? LabUnitName { get; set; }
    }
}
