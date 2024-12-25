using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DrDiscountRequestDTO
    {
        
        public int BranchId {  get; set; }
        public required int DrId {  get; set; }
        public decimal DrPercentage { get; set; } = 0;
        public decimal DrPercentageContract { get; set; } = 0;
        public int FlagDisc {  get; set; }
    }
}
