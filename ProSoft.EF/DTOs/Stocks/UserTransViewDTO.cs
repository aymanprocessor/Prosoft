using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Shared;
using System.ComponentModel;

namespace ProSoft.EF.DTOs.Stocks
{
    public class UserTransViewDTO
    {
        public int GId { get; set; }
        public string GDesc { get; set; }
        public string TransactionType { get; set; }
        public int TransFlag { get; set; }
        public int UeIns { get; set; }
        public int UeSav { get; set; }
        public int UeDel { get; set; }
    }
}
