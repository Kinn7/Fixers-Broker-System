namespace Fixers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            Employers = new HashSet<Employer>();
            Professionals = new HashSet<Professional>();
        }

        public int id { get; set; }

        [StringLength(100)]
        public string kebele_id { get; set; }

        [StringLength(15)]
        public string sub_city { get; set; }

        public int? woreda { get; set; }

        [StringLength(10)]
        public string house_no { get; set; }

        [StringLength(20)]
        public string phone_no { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employer> Employers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Professional> Professionals { get; set; }
    }
}
