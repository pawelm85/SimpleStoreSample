﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleStoreSample.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="Name")]
        public string ProductName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Product description")]
        [DataType( DataType.MultilineText)]
        public string Description { get; set; }
        
        public string ImagePath { get; set; }

        [Display(Name ="Price")]
        public decimal? UnitPrice { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}