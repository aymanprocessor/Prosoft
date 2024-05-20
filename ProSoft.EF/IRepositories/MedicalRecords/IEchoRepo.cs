using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.MedicalRecords
{
    public interface IEchoRepo: IRepository<Echo, int>
    {
        Task<List<EchoDTO>> GetAllByPatIdAsync(int patId);
        Task<EchoDTO> GetEchoByIdAsync(int serialId);
        Task AddEchoAsync(int patId, EchoDTO echoDTO);
        Task EditEchoAsync(int serialId, EchoDTO echoDTO);
    }
}
