using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Custom_Classes
{
    // Transaction is from GeneralCode Table
    public class TransactionDTOComparer : IEqualityComparer<PermissionDefViewDTO>
    {
        public bool Equals(PermissionDefViewDTO? x, PermissionDefViewDTO? y)
        {
            return x.GId == y.GId;
        }

        public int GetHashCode([DisallowNull] PermissionDefViewDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
