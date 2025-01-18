// using System;
// using System.Linq;
// using ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;
// using ProSoft.EF.Models.Medical.HospitalPatData;
// using ProSoft.EF.Models.Stocks;

// namespace ProSoft.Core.Repositories.Medical.HospitalPatData.Reports;

// public class ReceiptInquiryRepo
// {
//     public IEnumerable<ReceiptInquiryReportDTO> GetReceiptInquiryReport(int arg_inv_n, int arg_year, int arg_br)
//     {



//         // Example parameters
//         // string arg_inv_n = "INV123";
//         // int arg_year = 2023;
//         // int arg_br = 1;

//         // Example data sources (replace with your actual data sources)
//         var clinicTransList = new List<ClinicTran>();
//         var subItemList = new List<SubItem>();
//         var subClinicList = new List<SubClinic>();
//         var serviceClinicList = new List<ServiceClinic>();
//         var patList = new List<Pat>();
//         var depositList = new List<Deposit>();
//         var patAdmissionList = new List<PatAdmission>();

//         // First query (clinic_trans and related tables)
//         var query1 = from ct in clinicTransList
//                      join si in subItemList
//                          on new { x1=ct.BranchId, x2=ct.ItemMaster } equals new { x1=si.BranchId, x2=si.ItemCode } into siGroup
//                      from si in siGroup.DefaultIfEmpty() // Left join
//                      join sc in subClinicList
//                          on new { x1=ct.SClinicId, x2=ct.BranchId } equals new { x1=(int?)sc.SClinicId, x2=sc.BranchId } into scGroup
//                      from sc in scGroup.DefaultIfEmpty() // Left join
//                      join svc in serviceClinicList
//                          on new { ct.BranchId, ct.ClinicId, ct.SClinicId, x4=ct.ServId } equals new { svc.BranchId, svc.ClinicId, svc.SClinicId, x4=(int?)svc.ServId } into svcGroup
//                      from svc in svcGroup.DefaultIfEmpty() // Left join
//                      join p in patList
//                          on new { x1=ct.PatId, x2=ct.BranchId } equals new {x1= (int?)p.PatId,x2= (int?)p.BranchId } // Inner join
//                      where ct.ExInvoiceNo == arg_inv_n && ct.ExYear == arg_year && ct.BranchId == arg_br
//                      select new
//                      {
//                          ct.ExDate,
//                          ITM_SERV_FLAG = ct.ItmServFlag,
//                          ct.ExInvoiceNo,
//                          ct.CompId,
//                          ct.ItemMaster,
//                          ct.UserCodeModify,
//                          ct.UserCodeCreate,
//                          ct.CheckIdCancel,
//                          ct.ClinicId,
//                          ct.SClinicId,
//                          ct.ServId,
//                          ct.Flag,
//                          SubClinicDesc = sc != null ? sc.SClinicDesc : null,
//                          ServDesc = svc != null ? svc.ServDesc : null,
//                          SubName = si != null ? si.SubName : null,
//                          ct.PatAdDate,
//                          ct.PatId,
//                          p.PatName,
//                          ct.Deal
//                      };

//         // Second query (DEPOSIT and related tables)
//         var query2 = from d in depositList
//                      join pa in patAdmissionList
//                          on new { x1=d.MasterId, x2=d.FYear, x3=d.BranchId, x4=d.ExchangeType } equals new { x1=(int?)pa.MasterId, x2=(int?)pa.ExYear, x3=(int?)pa.BranchId, x4=(int?)pa.ExchangeType } // Inner join
//                      join p in patList
//                          on new {x1= pa.PatId, x2=pa.BranchId } equals new {x1= (int?) p.PatId, x2= (decimal?)p.BranchId } // Inner join
//                      where d.SafeDocNo == arg_inv_n && d.FYear == arg_year && d.BranchId == arg_br
//                      select new
//                      {
//                           d.DpsDate,
//                          ITM_SERV_FLAG = 0,
//                           d.SafeDocNo,
//                           pa.CompId,
//                          ItemMaster = "0",
//                          UserCodeModify = d.UserCode,
//                          UserCodeCreate = d.UserCode,
//                          CheckIdCancel = 1,
//                          ClinicId = 0,
//                          SClinicId = 0,
//                          ServId = 0,
//                          Flag = 1,
//                          SubClinicDesc = "دفعة مقدمة",
//                          ServDesc = "دفعة مقدمة",
//                          SubName = "دفعة مقدمة",
//                          PatAdDate = d.DpsDate,
//                           pa.PatId,
//                           p.PatName,
//                           pa.Deal
//                      };

//         // Combine both queries using UNION ALL
//         var finalResult = query1.Concat(query2).ToList();
        
//         return ;

//     }
// }
