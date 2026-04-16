using GymManagementSystemDAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Data.Configrations
{
    internal class MembershipConfigration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(X => X.CreatedAT)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()")
                ;
            builder.HasKey(x => new { x.MemberId, x.PlanId });
            builder.Ignore(x => x.MemberId);
        
        }
    }
}
