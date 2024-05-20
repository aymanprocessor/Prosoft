using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.MedicalRecords
{
    public interface IEcgAndEchoRepo: IRepository<EcgAndEcho, int>
    {
        Task<List<EcgAndEchoDTO>> GetAllByPatIdAsync(int patId);
        Task<EcgAndEchoDTO> GetEcgAndEchoByIdAsync(int serialId);
        Task AddEcgAndEchoAsync(int patId, EcgAndEchoDTO ecgAndEchoDTO);
        Task EditEcgAndEchoAsync(int serialId, EcgAndEchoDTO ecgAndEchoDTO);
    }
}
