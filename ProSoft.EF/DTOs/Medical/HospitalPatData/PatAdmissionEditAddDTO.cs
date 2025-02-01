using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Stocks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class PatAdmissionEditAddDTO
    {
        public int? BranchId { get; set; }
        public int? ExYear { get; set; }

        public string? PatName { get; set; }
        public int? patId { get; set; }
        public int? MasterId { get; set; }
        public DateTime PatAdDate { get; set; }
        public decimal Deal { get; set; }
        public int CompId { get; set; }
        public int CompIdDtl { get; set; }
        public int BrnachInitial { get; set; }
        public int SendFr { get; set; }
        public int DrCode { get; set; }
        public DateTime? PatDateExit { get; set; }
        public int SendTo { get; set; }
        public decimal PatientValue { get; set; }
        public decimal Prepaid { get; set; }
        public int MainInvNo { get; set; }
        public int SessionNo { get; set; }
        //Lists
        public List<ClassificationViewDTO>? classifications { get; set; }
        public List<DoctorViewDTO>? doctors { get; set; }
        public List<CompanyViewDTO>? companies { get; set; }
        public List<CompanyDtlViewDTO>? companyDtls { get; set; }
        public List<RegionViewDTO>? regions { get; set; }
        //////

    }
}
