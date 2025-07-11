﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.ExpenseData
{
    public class ExpenseDataAddEditDTO
    {
    
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float ExpenseValue { get; set; }
    }
}
