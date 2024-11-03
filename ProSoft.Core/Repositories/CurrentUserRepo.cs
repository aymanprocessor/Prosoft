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

        //public int UserId => int.Parse( _httpContextAccessor.HttpContext?.User?.FindFirstValue("User_Code"));
        public string UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        public string BranchName => _httpContextAccessor.HttpContext?.User?.FindFirstValue("U_Branch_Name");
        //public int BranchId =>int.Parse( _httpContextAccessor.HttpContext?.User?.FindFirstValue("U_Branch_Id"));
        //public int Year =>int.Parse( _httpContextAccessor.HttpContext?.User?.FindFirstValue("F_Year"));

        public int UserId
        {
            get
            {
                var userCode = _httpContextAccessor.HttpContext?.User?.FindFirstValue("User_Code");
                return int.TryParse(userCode, out var userId) ? userId : default;
            }
        }

        public int BranchId
        {
            get
            {
                var branchIdValue = _httpContextAccessor.HttpContext?.User?.FindFirstValue("U_Branch_Id");
                return int.TryParse(branchIdValue, out var branchId) ? branchId : default;
            }
        }

        public int Year
        {
            get
            {
                var yearValue = _httpContextAccessor.HttpContext?.User?.FindFirstValue("F_Year");
                return int.TryParse(yearValue, out var year) ? year : default;
            }
        }
    }
}
