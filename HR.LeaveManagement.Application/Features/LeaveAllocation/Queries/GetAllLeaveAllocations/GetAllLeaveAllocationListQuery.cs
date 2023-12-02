using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class GetAllLeaveAllocationListQuery : IRequest<IReadOnlyList<LeaveAllocationDto>>
    {
    }
}
