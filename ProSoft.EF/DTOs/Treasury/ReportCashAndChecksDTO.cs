using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Treasury
{
    public class ReportCashAndChecksDTO
    {
      public int? SafeCode { get; set; }
      public string? SafeNames { get; set; }
      public List<UserDTO>? userDTOs { get; set; }
      public List<BranchDTO>? branchDTOs { get; set; }
    }
}
