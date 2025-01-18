using ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;
using System;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData;

public interface IReceiptInquiryRepo
{
    IEnumerable<ReceiptInquiryReportDTO> GetReceiptInquiryReport(int arg_inv_n, int arg_year, int arg_br);

}
