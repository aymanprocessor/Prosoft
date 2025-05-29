using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class ClinicTransRequestDTO
    {
        public DateTime ExDate { get; set; }

        public int CheckId { get; set; }

        public int MasterId { get; set; }

        public int ItmServFlag { get; set; }

        public int ClinicId { get; set; }

        public int SClinicId { get; set; }

        public int ServId { get; set; }

        public int DrSendId { get; set; }

        public int Qty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal ValueService { get; set; }

        public decimal PatientValue { get; set; }

        public decimal CompValue { get; set; }

        public decimal DiscountVal { get; set; }

        public decimal ExtraVal { get; set; }

        public decimal ExtraVal2 { get; set; }

        public int ApprovalPeriod { get; set; }

        public int CheckIdCancel { get; set; }

        public int StockId { get; set; }
    }
}
