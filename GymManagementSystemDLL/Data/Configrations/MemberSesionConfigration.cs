using GymManagementSystemDAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Data.Configrations
{
    internal class MemberSesionConfigration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
                builder.HasKey(x => new { x.MemberId, x.SessionId });
                builder.Ignore(x => x.MemberId);
            builder.Property(X => X.CreatedAT)
                .HasColumnName("BokingDate")
                .HasDefaultValueSql("GETDATE()")
                ;
        }
    }
}
