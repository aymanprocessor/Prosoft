using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class BedRequestDTO
    {
        public int Id { get; set; }
        public string BedCodeSys { get; set; } = string.Empty;

        public int DegreeId { get; set; }
        public int RoomId { get; set; }
        public decimal PatientPrice { get; set; }
        public decimal RelativetPrice { get; set; }
        public int Branch { get; set; }
        public int BedOnOff { get; set; }
        public int BookId { get; set; }
    }
}
