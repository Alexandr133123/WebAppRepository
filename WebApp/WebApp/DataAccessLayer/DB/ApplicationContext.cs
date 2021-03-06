using Microsoft.EntityFrameworkCore;
using WebApp.DataAccessLayer.Model;
using System.Collections.Generic;
using WebApp.PresentationLayer.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApp.DataAccessLayer.DB
{
    public partial class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }


        public DbSet<Users> User { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable(nameof(Users));
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.Login)
                .IsUnique();
                entity.HasIndex(e => e.Password)
                .IsUnique();
            });
            modelBuilder.Entity<ProductGroupByLastModified>(entity => {
                entity.HasNoKey();
            });
            modelBuilder.Entity<ProductGroupByCategory>(entity => {
                entity.HasNoKey();
            });
            modelBuilder.Entity<ProductGroupByPrice>(entity => {
                entity.HasNoKey();
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable(nameof(Category));
                entity.HasKey(e => e.PK_CategoryId);
                entity.HasOne(d => d.FK_ParentCategory)
                    .WithMany(p => p.InverseFkParentCategory)
                    .HasForeignKey(d => d.FK_ParentCategoryId);

                entity
                .HasMany(d => d.Products)
                .WithMany(p => p.Categories)
                .UsingEntity<Dictionary<string, object>>("ProductCategory",
                    j => j.HasOne<Product>().WithMany().HasForeignKey("PFK_ProductId"),
                    j => j.HasOne<Category>().WithMany().HasForeignKey("PFK_CategoryId"),
                    j => j.ToTable("ProductCategory"));
            });
            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable(nameof(ProductImage));
                entity.HasKey(e => e.PFK_ImageId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable(nameof(Product));
                entity.HasKey(e => e.PK_ProductId);

                entity
                .HasOne(p => p.Image)
                .WithOne(i => i.Product)
                .HasForeignKey<ProductImage>(i => i.PFK_ImageId);
                
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
