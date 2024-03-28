using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class ClinicTransViewDTO
    {
        public int CheckId { get; set; }
        public int Counter { get; set; }
        public int ItmServFlag { get; set; }
        public string SubName { get; set; }
        public int SClinicId { get; set; }
        public string ServDesc { get; set; }
        public string DrDesc { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PatientValue { get; set; }
        public decimal ExtraVal { get; set; }
        public decimal ExtraVal2 { get; set; }
        public decimal CompValue { get; set; }
        public decimal DiscountVal { get; set; }
        public int ApprovalPeriod { get; set; }
        public int CheckIdCancel { get; set; }
      
    }
}
