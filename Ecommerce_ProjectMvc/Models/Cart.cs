using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce_ProjectMvc.Models
{
    public class Cart
    {
        public int productid { get; set; }

        public string productname { get; set; }
        public float price { get; set; }
        [Required(ErrorMessage = " Select Qty ")]
        public int qty { get; set; }
        public float bill { get; set; }

    }
}