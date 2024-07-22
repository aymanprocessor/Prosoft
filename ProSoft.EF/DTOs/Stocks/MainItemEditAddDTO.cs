using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Accounts;

namespace ProSoft.EF.DTOs.Stocks
{
    public class MainItemEditAddDTO
    {
        public int MainId { get; set; }

        [DisplayName("Group Code")]
        [Required(ErrorMessage = "The field is required")]
        public string MainCode { get; set; }

        [DisplayName("Group Name")]
        [Required(ErrorMessage = "The field is required")]
        public string MainName { get; set; }
        public string? MainNameAll { get; set; }
        public int? CurrentLevel { get; set; }

        [DisplayName("Item Counter")]
        [Required(ErrorMessage = "The field is required")]
        public int BatchCounter { get; set; }

        [DisplayName("Service Percentage")]
        public decimal? ServicePrc { get; set; }

        [DisplayName("Sort")]
        public int? MainSort { get; set; }

        [DisplayName("Price Policy")]
        public int? PolicyPrice { get; set; }

        [DisplayName("Last Level")]
        [Required(ErrorMessage = "The field is required")]
        public int LastSub { get; set; }

        [DisplayName("Cost Center")]
        [Required(ErrorMessage = "The field is required")]
        public int Replcate { get; set; }
        public int? BranchId { get; set; }
        public int? UserCode { get; set; }
        public string? ParentCode { get; set; }
        public int? Flag1 { get; set; }
        public string? ParentName { get; set; }

        public List<CostCenterViewDTO>? CostCenters { get; set; }
    }
}
