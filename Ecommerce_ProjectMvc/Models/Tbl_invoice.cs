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

    public partial class Tbl_invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_invoice()
        {
            this.Tbl_order = new HashSet<Tbl_order>();
        }
        [Key]
        public int Invoice_id { get; set; }
        public Nullable<int> In_fk_user { get; set; }
        public Nullable<System.DateTime> In_date { get; set; }
        public Nullable<double> In_totalbill { get; set; }
    
        public virtual Tbl_user Tbl_user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_order> Tbl_order { get; set; }
    }
}
