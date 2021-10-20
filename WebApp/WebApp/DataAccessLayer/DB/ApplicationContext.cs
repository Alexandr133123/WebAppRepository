using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApp.DataAccessLayer.Model;
using Microsoft.Extensions.Configuration;


namespace WebApp.DataAccessLayer.DB
{
    public partial class ApplicationContext : DbContext
    {
      
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public  DbSet<Category> Categories { get; set; }
        public  DbSet<Product> Products { get; set; }
        public  DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.PkCategoryId)
                    .HasName("PK__Category__590505A0EDFEDF35");

                entity.ToTable("Category");

                entity.Property(e => e.PkCategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("PK_CategoryId");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FkParentCategoryId).HasColumnName("FK_ParentCategoryId");

                entity.HasOne(d => d.FkParentCategory)
                    .WithMany(p => p.InverseFkParentCategory)
                    .HasForeignKey(d => d.FkParentCategoryId)
                    .HasConstraintName("FK__Category__FK_Par__412EB0B6");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.PkProductId)
                    .HasName("PK__Product__9E5454F308228BB3");

                entity.ToTable("Product");

                entity.Property(e => e.PkProductId).HasColumnName("PK_ProductId");

                entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => new { e.PfkProductId, e.PfkCategoryId })
                    .HasName("PK__ProductC__B6B57AB37699672B");

                entity.ToTable("ProductCategory");

                entity.Property(e => e.PfkProductId).HasColumnName("PFK_ProductId");

                entity.Property(e => e.PfkCategoryId).HasColumnName("PFK_CategoryId");

                entity.HasOne(d => d.PfkCategory)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.PfkCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductCa__PFK_C__4BAC3F29");

                entity.HasOne(d => d.PfkProduct)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.PfkProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductCa__PFK_P__4AB81AF0");
            });


            OnModelCreatingPartial(modelBuilder);
        }

       partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
