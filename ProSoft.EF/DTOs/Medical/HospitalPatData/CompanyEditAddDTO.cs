using ProSoft.EF.DTOs.Stocks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class CompanyEditAddDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int CompId { get; set; }

        public int? GroupId { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "The field is required")]
        public string CompName { get; set; }

        [DisplayName("List ID")]
        [Required(ErrorMessage = "The field is required")]
        public int PLId { get; set; }

        [DisplayName("Tax Number")]
        [Required(ErrorMessage = "The field is required")]
        public string TaxNo { get; set; }

        [DisplayName("Sub")]
        [Required(ErrorMessage = "The field is required")]
        public string SubCode { get; set; }

        [DisplayName("Main")]
        [Required(ErrorMessage = "The field is required")]
        public string MainCode { get; set; }

        [DisplayName("%")]
        [Required(ErrorMessage = "The field is required")]
        public decimal StampPer { get; set; }

        [DisplayName("Maximum")]
        [Required(ErrorMessage = "The field is required")]
        public decimal Stamp { get; set; }

        [DisplayName("%")]
        [Required(ErrorMessage = "The field is required")]
        public decimal TaxPer { get; set; }

        [DisplayName("Stock Type")]
        [Required(ErrorMessage = "The field is required")]
        public int KindStore { get; set; }

        [DisplayName("Insurance Company Level")]
        [Required(ErrorMessage = "The field is required")]
        public string EInvMainFlg { get; set; }

        [DisplayName("Company Address")]
        [Required(ErrorMessage = "The field is required")]
        public string ComAdd { get; set; }

        [DisplayName("Telephone Number")]
        [Required(ErrorMessage = "The field is required")]
        public string CompTelephone { get; set; }

        [DisplayName("Fax")]
        [Required(ErrorMessage = "The field is required")]
        public string CompFax { get; set; }

        [DisplayName("Medical Manager")]
        [Required(ErrorMessage = "The field is required")]
        public string MedicalManager { get; set; }

        [DisplayName("Insurance Period")]
        [Required(ErrorMessage = "The field is required")]
        public string InsurancePeriod { get; set; }


        [DisplayName("Activation")]
        [Required(ErrorMessage = "The field is required")]
        public int CompanyOnOff { get; set; }
        //list
        public List<PriceListViewDTO>? priceLists { get; set; }
        public List<KindStoreDTO>? kindStores { get; set; }

    }
}
