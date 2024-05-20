using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.MedicalRecords
{
    public interface ICoronaryRepo: IRepository<CoronaryAngiographyReport, int>
    {
        Task<List<CoronaryDTO>> GetAllByPatIdAsync(int patId);
        Task<CoronaryDTO> GetCoronaryByIdAsync(int serialId);
        Task AddCoronaryAsync(int patId, CoronaryDTO coronaryDTO);
        Task EditCoronaryAsync(int serialId, CoronaryDTO coronaryDTO);
    }
}
