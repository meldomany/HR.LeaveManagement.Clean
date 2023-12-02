using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Presistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class GetAllLeaveAllocationListQueryHandler : IRequestHandler<GetAllLeaveAllocationListQuery, IReadOnlyList<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public GetAllLeaveAllocationListQueryHandler(ILeaveAllocationRepository leaveAllocationRepository,
            IMapper mapper)
        {
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._mapper = mapper;
        }

        public async Task<IReadOnlyList<LeaveAllocationDto>> Handle(GetAllLeaveAllocationListQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
            var leaveAllocationsDto = _mapper.Map<IReadOnlyList<LeaveAllocationDto>>(leaveAllocations);
            return leaveAllocationsDto;
        }
    }
}
