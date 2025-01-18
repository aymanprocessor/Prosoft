namespace ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;

public class ReceiptInquiryReportDTO
{
    public DateTime? ExDate { get; set; }
    public int? ExInvoiceNo { get; set; }
    public int? PatId { get; set; }
    public string? PatName { get; set; }
    public int? Deal { get; set; }
    public int? ItmServFlag { get; set; }
    public int? UserCodeCreate { get; set; }
    public int? UserCodeModify { get; set; }
}
