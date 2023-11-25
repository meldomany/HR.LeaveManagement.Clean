using HR.LeaveManagement.Application.Contracts.Presistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> leaveAllocations)
        {
            await _context.LeaveAllocations.AddRangeAsync(leaveAllocations);
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(e => e.EmployeeId == userId
                    && e.LeaveTypeId == leaveTypeId
                    && e.Period == period);
        }

        public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Include(e => e.LeaveType).ToListAsync();

            return leaveAllocations;
        }

        public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Where(e => e.EmployeeId == userId)
                .Include(e => e.LeaveType).ToListAsync();

            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocation = await _context.LeaveAllocations
                .Include(e => e.LeaveType)
                .FirstOrDefaultAsync(e => e.Id == id);

            return leaveAllocation;
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            var leaveAllocations = await _context.LeaveAllocations.FirstOrDefaultAsync(e => e.EmployeeId == userId
                && e.LeaveTypeId == leaveTypeId);

            return leaveAllocations;
        }
    }
}
