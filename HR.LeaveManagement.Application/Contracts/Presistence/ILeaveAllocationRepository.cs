﻿using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Presistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
        Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails();
        Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);
        Task<bool> AllocationExists(string userId, int leaveTypeId, int period);
        Task AddAllocations(List<LeaveAllocation> leaveAllocations);
        Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId); 
    }
}
