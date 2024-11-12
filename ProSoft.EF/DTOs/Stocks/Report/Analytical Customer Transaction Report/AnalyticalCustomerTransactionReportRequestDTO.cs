namespace ProSoft.EF.DTOs.Stocks.Report.Analytical_Customer_Transaction_Report
{
    public class AnalyticalCustomerTransactionReportRequestDTO
    {
        public string? CustomerId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
