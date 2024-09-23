using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId => int.Parse( _httpContextAccessor.HttpContext?.User?.FindFirstValue("User_Code"));
        public string UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        public string BranchName => _httpContextAccessor.HttpContext?.User?.FindFirstValue("U_Branch_Name");
        public int BranchId =>int.Parse( _httpContextAccessor.HttpContext?.User?.FindFirstValue("U_Branch_Id"));
        public int Year =>int.Parse( _httpContextAccessor.HttpContext?.User?.FindFirstValue("F_Year"));
    }
}
