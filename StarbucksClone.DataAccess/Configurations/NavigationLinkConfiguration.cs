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
    internal class NavigationLinkConfiguration : EntityConfiguration<NavigationLink>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<NavigationLink> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.LinkHref).IsRequired().HasMaxLength(30);

            builder.HasOne(x => x.LinkPosition).WithMany(x => x.NavigationLinks).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
