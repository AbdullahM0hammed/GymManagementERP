using GymManagementSystemDAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Data.Configrations
{
    internal class TrainerConfigration :GymUserConfigration<Trainer> ,  IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(X => X.CreatedAT)
                .HasColumnName("HierDate")
                .HasDefaultValueSql("GETDATE()");
            base.Configure(builder);
        }
    }
}
