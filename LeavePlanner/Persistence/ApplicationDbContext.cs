using LeavePlanner.Core.EntityTypeConfigurations.BussinessConfigurations;
using LeavePlanner.Core.EntityTypeConfigurations.CodexConfigurations;
using LeavePlanner.Core.EntityTypeConfigurations.UserTypeConfigurations;
using LeavePlanner.Core.Models.Bussiness;
using LeavePlanner.Core.Models.Codex;
using LeavePlanner.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace LeavePlanner.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>,
        ApplicationUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        // Identity Models

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        // Audit Models
        // ... context code ...
        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }

        // Business models
        public DbSet<LeaveStatus> LeaveStatuses { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveReplacementUser> LeaveReplacementUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
                // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
                ((ApplicationDbContext)context).AuditEntries.AddRange(audit.Entries);

            AuditManager.DefaultConfiguration.ExcludeDataAnnotation();
            AuditManager.DefaultConfiguration.DataAnnotationDisplayName();

            // Identity models configuration
            builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
            builder.ApplyConfiguration(new LeaveStatusConfiguration());
            builder.ApplyConfiguration(new LeaveConfiguration());
            builder.ApplyConfiguration(new LeaveReplacementUserConfiguration());
        }
    }
}
