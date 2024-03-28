using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.Analysis
{
    public class ItemViewDTO
    {
        [DisplayName("Item Code")]
        public int Itemanalcode { get; set; }

        [DisplayName("Item Name")]
        public string? Itemanalname { get; set; }

        [DisplayName("Sub Item Code")]
        public string Codeanalcode { get; set; }

        [DisplayName("Natural rate for man")]
        public string? Itemanalmalenormal { get; set; }

        [DisplayName("Natural rate for Women")]
        public string? Itemanalfemalenormal { get; set; }

        [DisplayName("Main Code")]
        public string Mainanalcode { get; set; }

        [DisplayName("Sub Code")]
        public string Subanalcode { get; set; }

    }
}
