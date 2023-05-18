using LeavePlanner.Core.Interfaces;
using LeavePlanner.Core.Models.Bussiness;
using LeavePlanner.Models.Enums;
using LeavePlanner.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LeavePlanner.Persistence.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Leave>> GetEmployeeLeavesAsync(int? employeeId)
        {
            if (employeeId.HasValue)
            {
                return await _context.Leaves
                                     .Include(x => x.Status)
                                     .Include(x => x.ApplicationUser)
                                     .Where(x => !x.IsDeleted && x.ApplicationUserId == employeeId)
                                     .ToListAsync();
            }
            else
            {
                return await _context.Leaves
                                     .Where(x => !x.IsDeleted)
                                     .Include(x => x.Status)
                                     .Include(x => x.ApplicationUser)
                                                    .ToListAsync();
            }

        }

        public async Task<Leave> GetLeaveAsync(int id)
        {
            return await _context.Leaves
                .Include(x => x.ApplicationUser)
                .Include(x => x.Status)
                .Include(x => x.ReplacementUsers).ThenInclude(y => y.ApplicationUser)
                                 .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task PendingAsync(int id)
        {
            var leave = await GetLeaveAsync(id);

            if (leave.StatusId == (int)LeaveStatusEnums.Approved)
            {
                var employee =
                    await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == leave.ApplicationUserId);
                employee.RemainingLeaveDaysInYear += leave.WorkingDaysUsed;
            }

            leave.StatusId = (int)LeaveStatusEnums.Pending;
        }

        public async Task CreateAsync(Leave leave)
        {
            await _context.Leaves.AddAsync(leave);
        }

        public async Task ApproveAsync(int id)
        {
            var leave = await GetLeaveAsync(id);

            if (leave.ApplicationUser.RemainingLeaveDaysInYear.HasValue && leave.ApplicationUser.RemainingLeaveDaysInYear.Value >= leave.WorkingDaysUsed)
            {
                leave.ApplicationUser.RemainingLeaveDaysInYear -= leave.WorkingDaysUsed;
                leave.StatusId = (int)LeaveStatusEnums.Approved;
            }
            else
            {
                throw new ChangesNotAllowedException("No available leave days for employee!");
            }
        }

        public async Task DeclineAsync(int id)
        {
            var leave = await GetLeaveAsync(id);

            if (leave.StatusId == (int)LeaveStatusEnums.Approved)
            {
                var employee = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == leave.ApplicationUserId);
                employee.RemainingLeaveDaysInYear += leave.WorkingDaysUsed;
            }

            leave.StatusId = (int)LeaveStatusEnums.Declined;
        }

        public async Task DeleteAsync(int id)
        {
            var leave = await GetLeaveAsync(id);
            leave.IsDeleted = true;
        }
    }
}
