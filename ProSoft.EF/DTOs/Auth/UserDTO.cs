using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Auth
{
    public class UserDTO
    {
        public int UserCode { get; set; }
        
        public string UserName { get; set; }
        public string Name { get; set; }
        public List<RoleDTO> Roles { get; set;}
    }
}
