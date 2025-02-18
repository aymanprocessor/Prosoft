using Microsoft.AspNetCore.Identity;

namespace ProSoft.EF.Models
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public virtual IdentityRole Role { get; set; } // Navigation property
    }
}
