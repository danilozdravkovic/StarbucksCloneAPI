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
    internal abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}
