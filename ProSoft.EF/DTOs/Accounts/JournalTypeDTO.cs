using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class JournalTypeDTO
    {
        public int JournalCode { get; set; }

        public string JournalName { get; set; }

        public string? CounterFlag { get; set; }

        public string? JournalType1 { get; set; }

        public string? JournalInOut { get; set; }
    }
}
