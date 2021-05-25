using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce_ProjectMvc.Models
{
    public class MultiModel
    {
       
        public Tbl_invoice My_Tbl_invoice { get; set; }
        public  Tbl_order My_Tbl_order { get; set; }
    }
}