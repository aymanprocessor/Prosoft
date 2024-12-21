using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IBedCodeRepo : IRepository<BedCode, int>
    {
        Task<int> GetNewIdAsync();

        Task<List<BedCode>> GetBedsByDegreeIdAndRoomId(int degreeId, int roomId);
    }
}
