using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccSubCodeDTO
    {
        public string SubCode { get; set; }
        public string MainCode { get; set; }
        public string SubName { get; set; }
    }
}
