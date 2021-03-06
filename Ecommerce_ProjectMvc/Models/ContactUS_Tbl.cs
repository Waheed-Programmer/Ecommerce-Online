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

    public partial class ContactUS_Tbl
    {
        public int ContactId { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        [StringLength(50)]
        [EmailAddress]
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Subject Required")]
        public string Subject { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Message Required")]
        public string SMS { get; set; }
    }
}
