using LeavePlanner.Core.Interfaces;
using LeavePlanner.Core.Models.Bussiness;
using LeavePlanner.Core.Models.Codex;
using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Models.Enums;
using LeavePlanner.Utilities.Security;
using Microsoft.AspNetCore.Identity;

namespace LeavePlanner.Persistence
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DataSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedData()
        {
            // SEED ROLES
            if (!_roleManager.Roles.Any())
            {
                ApplicationRole admin = new ApplicationRole
                {
                    Name = UserRoles.Admin
                };
                _roleManager.CreateAsync(admin).Wait();

                ApplicationRole employee = new ApplicationRole
                {
                    Name = UserRoles.Employee
                };
                _roleManager.CreateAsync(employee).Wait();
            }

            // SEED USERS
            if (!_userManager.Users.Any())
            {
                ApplicationUser admin = new ApplicationUser
                {
                    UserName = "admin@admin.hr",
                    Email = "admin@admin.hr",
                    FirstName = "Admin",
                    LastName = "Admin"
                };
                IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Password.1!").Result;
                if (resultAdmin.Succeeded)
                {
                    _userManager.AddToRoleAsync(admin, UserRoles.Admin).Wait();
                }

                ApplicationUser emp1 = new ApplicationUser
                {
                    UserName = "employee1@employee1.hr",
                    Email = "employee1@employee1.hr",
                    FirstName = "Employee 1",
                    LastName = "Employee 1",
                    LeaveDaysPerYear = 20
                };
                _ = _userManager.CreateAsync(emp1, "Password.1!").Result;
                if (resultAdmin.Succeeded)
                {
                    _userManager.AddToRoleAsync(emp1, UserRoles.Employee).Wait();
                }

                ApplicationUser emp2 = new ApplicationUser
                {
                    UserName = "employee2@employee2.hr",
                    Email = "employee2@employee2.hr",
                    FirstName = "Employee 2",
                    LastName = "Employee 2",
                    LeaveDaysPerYear = 25
                };
                _ = _userManager.CreateAsync(emp2, "Password.1!").Result;
                if (resultAdmin.Succeeded)
                {
                    _userManager.AddToRoleAsync(emp2, UserRoles.Employee).Wait();
                }

                ApplicationUser emp3 = new ApplicationUser
                {
                    UserName = "employee3@employee3.hr",
                    Email = "employee3@employee3.hr",
                    FirstName = "Employee 3",
                    LastName = "Employee 3",
                    LeaveDaysPerYear = 28
                };
                _ = _userManager.CreateAsync(emp3, "Password.1!").Result;
                if (resultAdmin.Succeeded)
                {
                    _userManager.AddToRoleAsync(emp3, UserRoles.Employee).Wait();
                }
            }

            if (!_context.LeaveStatuses.Any())
            {
                var leaveStatuses = new List<LeaveStatus>
                {
                    new()
                    {
                        Name = "Pending"
                    },
                    new()
                    {
                        Name = "Declined"
                    },
                    new()
                    {
                        Name = "Approved"
                    }
                };

                _context.LeaveStatuses.AddRange(leaveStatuses);
                _context.SaveChanges();
            }

            if (!_context.Leaves.Any())
            {
                var leaves = new List<Leave>
                {
                    new()
                    {
                        WorkingDaysUsed = 5,
                        ApplicationUserId = 2,
                        StartDate = new DateTime(2023, 05, 01),
                        EndDate = new DateTime(2023, 05, 07),
                        StatusId = (int)LeaveStatusEnums.Approved,
                        ReplacementUsers = new List<LeaveReplacementUser>
                        {
                            new()
                            {
                                ApplicationUserId = 2
                            },
                            new()
                            {
                                ApplicationUserId = 3
                            },
                        }
                    },
                    new()
                    {
                        WorkingDaysUsed = 2,
                        ApplicationUserId = 3,
                        StartDate = new DateTime(2023, 05, 01),
                        EndDate = new DateTime(2023, 05, 03),
                        StatusId = (int)LeaveStatusEnums.Approved,
                        ReplacementUsers = new List<LeaveReplacementUser>
                        {
                            new()
                            {
                                ApplicationUserId = 3
                            },
                            new()
                            {
                                ApplicationUserId = 4
                            },
                        }
                    },
                    new()
                    {
                        WorkingDaysUsed = 2,
                        ApplicationUserId = 3,
                        StartDate = new DateTime(2023, 05, 01),
                        EndDate = new DateTime(2023, 05, 03),
                        StatusId = (int)LeaveStatusEnums.Pending,
                        ReplacementUsers = new List<LeaveReplacementUser>
                        {
                            new()
                            {
                                ApplicationUserId = 1
                            },
                            new()
                            {
                                ApplicationUserId = 2
                            },
                        }
                    }
                };

                _context.Leaves.AddRange(leaves);
                _context.SaveChanges();
            }
        }
    }
}
