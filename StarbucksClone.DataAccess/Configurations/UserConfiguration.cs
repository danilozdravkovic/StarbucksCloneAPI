using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarbuckClone.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.DataAccess.Configurations
{
    internal class UserConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email)
                   .HasMaxLength(60)
                   .IsRequired();
            builder.HasIndex(x => x.Email)
                   .IsUnique();

            builder.Property(x => x.Username)
                   .HasMaxLength(20)
                   .IsRequired();
            builder.HasIndex(x => x.Username)
                   .IsUnique();

            builder.Property(x => x.FirstName)
                   .HasMaxLength(20);

            builder.Property(x => x.LastName)
                   .HasMaxLength(50);

            builder.HasIndex(x => new { x.FirstName, x.LastName, x.Email, x.Username });

            builder.Property(x => x.Password)
                   .IsRequired()
            .HasMaxLength(120);

            builder.HasMany(x => x.Orders).WithOne(o => o.User).OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
