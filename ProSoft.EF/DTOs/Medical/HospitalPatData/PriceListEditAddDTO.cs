﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class PriceListEditAddDTO
    {
        [DisplayName("Code")]
        public int? PLId { get; set; }

        [DisplayName("List Name")]
        public string? PlDesc { get; set; }

        [DisplayName("List Type")]
        public int? Flag1 { get; set; }

        [DisplayName("Date of List")]
        public DateTime? PLDate { get; set; }

        [DisplayName("Financial Year")]
        public int? Year { get; set; }

        [DisplayName("Check Add To Company")]
        public int? CheckAddToCompany { get; set; }

    }
}
