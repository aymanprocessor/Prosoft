using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IRoomCodeRepo : IRepository<RoomCode, int>
    {
        Task<int> GetNewIdAsync();
        Task<List<RoomCode>> GetRoomsByDegreeId(int degreeId);
    }
}
