using LeavePlanner.Core.Models.Bussiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeavePlanner.Core.EntityTypeConfigurations.BussinessConfigurations
{
    public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.HasOne(r => r.Status)
                .WithMany()
                .HasForeignKey(s => s.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ssi => ssi.ReplacementUsers)
                .WithOne(ss => ss.Leave)
                .HasForeignKey(ss => ss.LeaveId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Leave", "dbo");
        }
    }
}
