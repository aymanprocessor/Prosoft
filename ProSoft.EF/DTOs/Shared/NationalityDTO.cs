using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProSoft.EF.DTOs.Shared
{
    public class NationalityDTO
    {
        [DisplayName("Id")]
        public int NationalityId { get; set; }

        [DisplayName("Nationality Name EN")]
        public string NationalityDesc { get; set; }

        [DisplayName("Nationality Name AR")]
        public string NationalityDescE { get; set; }


    }
}
