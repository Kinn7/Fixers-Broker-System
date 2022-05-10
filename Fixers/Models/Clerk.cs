namespace Fixers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Clerk")]
    public partial class Clerk
    {
        public int id { get; set; }
        [Display(Name = "User name")]
        [StringLength(50)]
        [Required(ErrorMessage = "UserName Required")]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password Required")]
        [StringLength(150)]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
