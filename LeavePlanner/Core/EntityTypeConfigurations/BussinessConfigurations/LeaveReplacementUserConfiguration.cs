using LeavePlanner.Core.Models.Bussiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeavePlanner.Core.EntityTypeConfigurations.BussinessConfigurations;

public class LeaveReplacementUserConfiguration : IEntityTypeConfiguration<LeaveReplacementUser>
{
    public void Configure(EntityTypeBuilder<LeaveReplacementUser> builder)
    {
        builder.ToTable("LeaveReplacementUser", "dbo");
    }
}