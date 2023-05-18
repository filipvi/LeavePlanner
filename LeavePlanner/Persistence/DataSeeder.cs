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

                ApplicationUser user1 = new ApplicationUser
                {
                    UserName = "employee@employee.hr",
                    Email = "employee@employee.hr",
                    FirstName = "Employee 1",
                    LastName = "Employee",
                    LeaveDaysPerYear = 20,
                    RemainingLeaveDaysInYear = 15
                };
                _ = _userManager.CreateAsync(user1, "Password.1!").Result;
                if (resultAdmin.Succeeded)
                {
                    _userManager.AddToRoleAsync(user1, UserRoles.Employee).Wait();
                }


                ApplicationUser user2 = new ApplicationUser
                {
                    UserName = "employee2@employee2.hr",
                    Email = "employee2@employee2.hr",
                    FirstName = "Employee 2",
                    LastName = "Employee 2",
                    LeaveDaysPerYear = 25,
                    RemainingLeaveDaysInYear = 23
                };
                _ = _userManager.CreateAsync(user2, "Password.1!").Result;
                if (resultAdmin.Succeeded)
                {
                    _userManager.AddToRoleAsync(user2, UserRoles.Employee).Wait();
                }

                ApplicationUser user3 = new ApplicationUser
                {
                    UserName = "employee3@employee3.hr",
                    Email = "employee3@employee3.hr",
                    FirstName = "Employee 3",
                    LastName = "Employee 3",
                    LeaveDaysPerYear = 22,
                    RemainingLeaveDaysInYear = 22
                };
                _ = _userManager.CreateAsync(user3, "Password.1!").Result;
                if (resultAdmin.Succeeded)
                {
                    _userManager.AddToRoleAsync(user3, UserRoles.Employee).Wait();
                }
            }

            if (!_context.LeaveStatuses.Any())
            {
                var leaveStatuses = new List<LeaveStatus>
                {
                    new LeaveStatus()
                    {
                        Name = "Pending"
                    },
                    new LeaveStatus()
                    {
                        Name = "Declined"
                    },
                    new LeaveStatus()
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
                    new Leave
                    {
                        WorkingDaysUsed = 5,
                        ApplicationUserId = 2,
                        StartDate = new DateTime(2023, 05, 01),
                        EndDate = new DateTime(2023, 05, 07),
                        StatusId = (int)LeaveStatusEnums.Approved,
                        ReplacementUsers = new List<LeaveReplacementUser>
                        {
                            new LeaveReplacementUser
                            {
                                ApplicationUserId = 2
                            },
                            new LeaveReplacementUser
                            {
                                ApplicationUserId = 3
                            },
                        }
                    },
                    new Leave
                    {
                        WorkingDaysUsed = 2,
                        ApplicationUserId = 3,
                        StartDate = new DateTime(2023, 05, 01),
                        EndDate = new DateTime(2023, 05, 03),
                        StatusId = (int)LeaveStatusEnums.Approved,
                        ReplacementUsers = new List<LeaveReplacementUser>
                        {
                            new LeaveReplacementUser
                            {
                                ApplicationUserId = 3
                            },
                            new LeaveReplacementUser
                            {
                                ApplicationUserId = 4
                            },
                        }
                    },
                    new Leave
                    {
                        WorkingDaysUsed = 2,
                        ApplicationUserId = 3,
                        StartDate = new DateTime(2023, 05, 01),
                        EndDate = new DateTime(2023, 05, 03),
                        StatusId = (int)LeaveStatusEnums.Pending,
                        ReplacementUsers = new List<LeaveReplacementUser>
                        {
                            new LeaveReplacementUser
                            {
                                ApplicationUserId = 1
                            },
                            new LeaveReplacementUser
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
