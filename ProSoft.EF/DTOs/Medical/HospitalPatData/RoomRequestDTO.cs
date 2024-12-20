using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class RoomRequestDTO
    {
        public int Id { get; set; }
        public int DegreeId { get; set; }
        public decimal PatientPrice { get; set; }
        public decimal RelativetPrice { get; set; }
        public int Branch { get; set; }
        public int RoomOnOff {  get; set; }


    }
}
