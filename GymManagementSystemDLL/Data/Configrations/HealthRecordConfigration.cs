using GymManagementSystemDAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GymManagementSystemDAL.Data.Configrations
{
    internal class HealthRecordConfigration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members")
                .HasKey(m => m.Id)
                ;
            builder.HasOne<Member>()
                .WithOne(X => X.HealthRecord)
                .HasForeignKey<HealthRecord>(X => X.Id);
            builder.Ignore(X=> X.CreatedAT);
            builder.Ignore(X => X.UpdatedAT);


        }
    }
}
