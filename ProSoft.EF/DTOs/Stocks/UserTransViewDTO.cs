using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Shared;

namespace ProSoft.EF.DTOs.Stocks
{
    public class UserTransViewDTO
    {
        public int UserCode { get; set; }
        public string UserName { get; set; }
        public List<PermissionDefViewDTO> Permissions {  get; set; }
    }
}
