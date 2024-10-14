using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProSoft.EF.Models;


namespace ProSoft.EF.DbContext
{
    public class OracleDbContext : IdentityDbContext<AppUser>
    {
    }
}
