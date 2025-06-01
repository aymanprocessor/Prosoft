using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Stocks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class ClinicTransEditAddDTO
    {
        public int CheckId { get; set; }
        public int BranchId { get; set; }
        public int ExYear { get; set; }
        public int ItmServFlag { get; set; }
        public int? ClinicId { get; set; }
        public int? SClinicId { get; set; }
        public int? MainId { get; set; }
        public int? SubId { get; set; }
        public string? ServDesc { get; set; }
        public string? DrDesc { get; set; }
        public int? ServId { get; set; }
        public int? DrCode { get; set; }
        public int? DrSend { get; set; }
        public int ExInvoiceNo { get; set; }
        [Required]
        public DateTime? ExDate { get; set; }
        public int StockCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public decimal PatientValue { get; set; }
        public decimal ExtraVal { get; set; }
        public decimal ExtraVal2 { get; set; }
        public decimal CompValue { get; set; }
        public decimal DiscountVal { get; set; }
        public int ApprovalPeriod { get; set; }
        public int CheckIdCancel { get; set; }
        public decimal? ValueService { get; set; }
        public decimal? DrValPat { get; set; }
        public decimal? HoValPat { get; set; }

        ///optional///
        public int? PatId { get; set; }
        public int? CompId { get; set; }
        public int? CompIdDtl { get; set; }
        public int? SendTo { get; set; }
        public int? SendFr { get; set; }
        public long? MainInvNo { get; set; }
        public int? SessionNo { get; set; }
        public int? Flag { get; set; }
        public int? Counter { get; set; }
        public int? MasterId { get; set; }
        public string? SubCode { get; set; }
        public string? MainCode { get; set; }
        ////////////
        //Lists
        public List<MainClinicViewDTO>? MainClinics { get; set; }
        public List<MainItemViewDTO>? MainItems { get; set; }
        public List<DoctorViewDTO>? Doctors { get; set; }
        public List<SubClinicViewDTO>? SubClinics { get; set; }
        public List<ServiceClinicViewDTO>? ServiceClinics { get; set; }
        public List<SubItemViewDTO>? SubItems { get; set; }
    }
}
