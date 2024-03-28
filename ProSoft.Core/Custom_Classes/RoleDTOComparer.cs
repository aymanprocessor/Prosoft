using ProSoft.EF.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Custom_Classes
{
    public class RoleDTOComparer : IEqualityComparer<RoleDTO>
    {
        public bool Equals(RoleDTO? x, RoleDTO? y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] RoleDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
