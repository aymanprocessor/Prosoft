﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class MainItemViewDTO
    {
        public int MainId { get; set; }
        public string MainCode { get; set; }
        public string MainName { get; set; }
        public int BatchCounter { get; set; }
        public int LastSub { get; set; }
        public int Flag1 { get; set; }
        public string ParentCode { get; set; }
    }
}
