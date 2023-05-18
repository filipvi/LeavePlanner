using LeavePlanner.Core.Models.Codex;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeavePlanner.Core.EntityTypeConfigurations.CodexConfigurations
{
    public class LeaveStatusConfiguration : IEntityTypeConfiguration<LeaveStatus>
    {
        public void Configure(EntityTypeBuilder<LeaveStatus> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.ToTable("LeaveStatus", "codex");
        }
    }
}
