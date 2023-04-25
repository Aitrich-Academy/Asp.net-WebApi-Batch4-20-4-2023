using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Models
{
    public partial class CategoryModel : DbContext
    {
        public CategoryModel()
            : base("name=CategoryModel1")
        {
        }

        public virtual DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.createdBy)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.createdDate)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.lastModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.lastModifiedDate)
                .IsUnicode(false);
        }
    }
}
