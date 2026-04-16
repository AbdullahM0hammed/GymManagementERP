using GymManagementSystemDAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Data.Configrations
{
    internal class GymUserConfigration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(X => X.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);
            builder.Property(X => X.Email)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);
            builder.Property(X => X.phone)
                .HasColumnType("nvarchar")
                .HasMaxLength(11);
            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("EmailHasCheckConstraint", "Email like '_%@_%._%'");
                Tb.HasCheckConstraint("PhoneHasCheckConstraint", "phone like '01%' and phone Not Like '%[^0-9]%'");
            });
            builder.HasIndex(X => X.Email).IsUnique();
            builder.HasIndex(X => X.phone).IsUnique();

            builder.OwnsOne(X => X.Address, AddressBuilder =>
            {
                AddressBuilder.Property(X => X.City)
                .HasColumnType("nvarchar")
                .HasMaxLength(30);
                AddressBuilder.Property(X => X.streeet)
                .HasColumnType("nvarchar")
                .HasMaxLength(30);
                AddressBuilder.Property(X => X.BuildingNumber)
                .HasColumnName("Buildingnumber");
            });
        }
    }
}
