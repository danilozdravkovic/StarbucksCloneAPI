using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarbuckClone.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.DataAccess.Configurations
{
    internal class ProductConfiguration : EntityConfiguration<Product>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.ImageSrc).IsRequired();
   
            builder.Property(x => x.Name)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.HasIndex(x => x.Name)
                   .IsUnique();

            builder.HasMany(x => x.Sizes).WithMany(x => x.Products).UsingEntity<Dictionary<string, object>>(
                "ProductsSizes",
                 j => j.HasOne<Size>().WithMany().HasForeignKey("SizeId"),
                 j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId")
                );

            builder.HasMany(x=>x.CustomAddIns).WithMany(x=>x.CustomProducts).UsingEntity<Dictionary<string, object>>(
                "ProductsCustomAddIns",
                 j => j.HasOne<AddIn>().WithMany().HasForeignKey("AddInId"),
                 j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId")
                ).Property<int?>("Pump");

            builder.HasMany(x => x.IncludedAddIns).WithMany(x => x.IncludedProducts).UsingEntity<Dictionary<string, object>>(
                "ProductsIncludedAddIns",
                 j => j.HasOne<AddIn>().WithMany().HasForeignKey("AddInId"),
                 j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId")
            ).Property<int?>("Pump");

            builder.HasMany(x => x.OrderLines).WithOne(o => o.Product).OnDelete(DeleteBehavior.Restrict);
           
        }
    }
}
