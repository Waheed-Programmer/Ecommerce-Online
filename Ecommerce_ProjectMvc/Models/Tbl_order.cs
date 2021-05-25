//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce_ProjectMvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Tbl_order
    {
        [Key]
        public int Order_id { get; set; }
        public Nullable<int> O_fk_pro { get; set; }
        public Nullable<int> O_fk_invoice { get; set; }
        public Nullable<System.DateTime> O_date { get; set; }
        
        [Required(ErrorMessage = "Qty Required")]
        public Nullable<int> O_qty { get; set; }
        public Nullable<double> O_bill { get; set; }
        public Nullable<int> O_unitprice { get; set; }
    
        public virtual Tbl_invoice Tbl_invoice { get; set; }
        public virtual Tbl_product Tbl_product { get; set; }
    }
}