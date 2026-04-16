using GymManagementSystemDAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Data.Configrations
{
    internal class SessionConfigration : IEntityTypeConfiguration<Sessoin>
    {
        public void Configure(EntityTypeBuilder<Sessoin> builder)
        {
            builder.ToTable(Tb=>
            {
                Tb.HasCheckConstraint("CK_Session_Capacity", "capacity Between 1 and 25");
                Tb.HasCheckConstraint("CK_Session_Dates", "StartDate < EndDate");
            }
            );
            builder.HasOne(s => s.Category)
                .WithMany(c => c.Sessoins)
                .HasForeignKey(s => s.CategoryId);

            builder .HasOne(s => s.Trainer)
                .WithMany(t => t.Sessoins)
                .HasForeignKey(s => s.TrainerId);
        }
    }
}
