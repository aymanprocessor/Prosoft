using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class AnalysisPrintDTO
    {
       public string patName {  get; set; }
       public int patId {  get; set; }
       public double age {  get; set; }
       public DateTime admissonDate {  get; set; }
       public string drName {  get; set; }
       public int drId {  get; set; }
       public string compName {  get; set; }
       public string compNameDetail {  get; set; }
       public string analysisName {  get; set; }
       public List<AnalysiDtlViewDTO> analysiDtlViewDTOs { get; set; }

    }
}
