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
    internal class ProductCategoryConfiguration : EntityConfiguration<ProductCategory>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(x => x.Name)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.HasIndex(x => x.Name)
                   .IsUnique();

            builder.HasMany(x => x.Products)
                   .WithOne(x => x.Category)
                   .HasForeignKey(x=>x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
