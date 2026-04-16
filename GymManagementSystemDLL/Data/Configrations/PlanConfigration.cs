using GymManagementSystemDAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Data.Configrations
{
    internal class PlanConfigration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(X => X.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(50);
            builder.Property(X => X.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(200);
            builder.Property(X => X.Price)
            .HasPrecision(18, 2);
            builder.ToTable(Tb=>
            {
                 Tb.HasCheckConstraint("CK_Plan_DurationDays", "DurationDays Between 1 and 365");
            }
            );

        }
    }
}
