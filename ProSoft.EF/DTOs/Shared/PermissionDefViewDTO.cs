using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Shared
{
    public class PermissionDefViewDTO
    {
        public int GId { get; set; }
        public string GDesc { get; set; }
        public string TransDescUser { get; set; }
        public int TransType { get; set; }
        public int ShowHide { get; set;}
        public int AddSub { get; set; }
        public int FormType { get; set; }
        public int UniqueType { get; set; }
        public string PermissionDepended { get; set; }
        public string TransactionType { get; set; }
    }
}
