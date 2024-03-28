using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProSoft.EF.Models.Medical.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.Analysis
{
    public interface ILabUnitRepo : IRepository<LabUnit, short>
    {
        Task<short> GetNewIdAsync();
    }
}
