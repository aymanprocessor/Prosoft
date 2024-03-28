using ProSoft.EF.DTOs.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IAnalysisDetailRepo
    {
        Task<List<AnalysiDtlViewDTO>> GetAnalysisDtlByClinicTransAsync(int visitId);
        Task<AnalysisDtlEditDTO> GetAnalysisDtlByIdAsync(int id);
        Task EditAnalysisDtl(int id, AnalysisDtlEditDTO analysisDtlEditDTO);
        Task<AnalysisPrintDTO> PrintAnalysisDtlAsync(int clinicTransID);
    }
}
