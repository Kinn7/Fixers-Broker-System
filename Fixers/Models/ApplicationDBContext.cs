using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Fixers.Models
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
            : base("name=ApplicationDBContext")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Clerk> Clerks { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<HireStatus> HireStatus { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<Professional> Professionals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.kebele_id)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.sub_city)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.house_no)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.phone_no)
                .IsUnicode(false);

            modelBuilder.Entity<Clerk>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Clerk>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Employer>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employer>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Employer>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employer>()
                .HasMany(e => e.HireStatus)
                .WithRequired(e => e.Employer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Profession>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Professional>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Professional>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Professional>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Professional>()
                .HasMany(e => e.HireStatus)
                .WithRequired(e => e.Professional)
                .WillCascadeOnDelete(false);
        }
    }
}
