using GymManagementSystemDAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GymManagementSystemDAL.Data.Configrations
{
    internal class MemberConfigration : GymUserConfigration<Member>,IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
         builder.Property(X => X.CreatedAT)
        .HasColumnName("JoinedDate")
        .HasDefaultValueSql("GETDATE()");
         base.Configure(builder);
        }
    }
}
