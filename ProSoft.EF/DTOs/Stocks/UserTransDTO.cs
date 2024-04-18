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
    public class UserTransDTO
    {
        public int UsrId { get; set; }
        public int GId { get; set; }
        public List<UserDTO>? Users {  get; set; }
        public List<PermissionDefViewDTO>? Permissions {  get; set; }
    }
}
