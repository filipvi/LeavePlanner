using LeavePlanner.Core.Models.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeavePlanner.Core.EntityTypeConfigurations
{
    public class TestTableConfiguration : IEntityTypeConfiguration<TestTable>
    {
        public void Configure(EntityTypeBuilder<TestTable> builder)
        {
            builder.Property(x => x.CultureCategory).IsRequired();
            builder.Property(x => x.CultureName).IsRequired();
        }
    }
}
