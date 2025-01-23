namespace ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;

public class ReceiptInquiryReportDTO
{
    public string? ExDate { get; set; }
    public string? ExTime { get; set; }
    public int? ExInvoiceNo { get; set; }
    public int? PatId { get; set; }
    public string? PatName { get; set; }
    public int? Deal { get; set; }
    public string? ServiceDesc { get; set; }
    public int? ItmServFlag { get; set; }
    public int? UserCodeCreate { get; set; }
    public int? UserCodeModify { get; set; }
}
