using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class CustCodeEditAddDTO
    {
        public string CustCode1 { get; set; }

        [DisplayName("Customer_Name_AR")]
        public string? CustName { get; set; }
        
        [DisplayName("Customer_Name_EN")]
        public string? CustNameE { get; set; }

        [DisplayName("Customer in Accounts")]
        public string? SubCode { get; set; }
        public string MainCode { get; set; }

        [DisplayName("Responsible Person")]
        public string? ContactPerson { get; set; }

        [DisplayName("Spending Side")]
        public int? CustDisc4 { get; set; }

        [DisplayName("Under Tax")]
        public string? TaxType { get; set; }

        [DisplayName("Telephone Number")]
        public string? CustPhone1 { get; set; }

        [DisplayName("Mobile number")]
        public string? CustPhone2 { get; set; }

        [DisplayName("Fax Number")]
        public string? CustFax { get; set; }
        
        [DisplayName("Pricing Type")]
        public int? AdjectiveCode { get; set; }

        [DisplayName("Notes")]
        public string? Remarks { get; set; }

        [DisplayName("The Email")]
        public string? Email { get; set; }

        [DisplayName("Pricing List")]
        public int? CatalogId { get; set; }

        public List<AccSubCodeDTO>? SubCodes { get; set; }
        public List<PriceListViewDTO>? PricesLists { get; set; }
        public List<AdjectiveCustDTO>? PriceTypes { get; set; }
    }
}
