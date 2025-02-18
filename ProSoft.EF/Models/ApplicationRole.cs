using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationRoleClaim> Claims { get; set; }
    }
}
