namespace Fixers.Models
{
    using Fixers.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Professional")]
    public partial class Professional
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Professional()
        {
            HireStatus = new HashSet<HireStatus>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Required")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [Range(100,5000,ErrorMessage = "Balance must be between 500 and 5000")]
        public double? Balance { get; set; }
        [Required]
        public ProfessionalStatus Status { get; set; }

        public double? Fee { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "The {0} Must be at least {2} characters long.")]
        [DataType(DataType.Password)]        
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password don not match")]
        public string ConfirmPassword { get; set; }
        public byte[] Picture { get; set; }
        
        public int? AddressID { get; set; }
        public int? ProfessionID { get; set; }

        public virtual Address Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HireStatus> HireStatus { get; set; }

        public virtual Profession Profession { get; set; }
    }
}
