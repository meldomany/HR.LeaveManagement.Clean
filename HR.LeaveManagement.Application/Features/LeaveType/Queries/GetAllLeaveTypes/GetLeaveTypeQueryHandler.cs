using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Presistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypeQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly Mapper _mapper;
        private readonly IAppLogger<GetLeaveTypeQueryHandler> _logger;

        public GetLeaveTypeQueryHandler(ILeaveTypeRepository leaveTypeRepository, 
            Mapper mapper,
            IAppLogger<GetLeaveTypeQueryHandler> logger)
        {
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _leaveTypeRepository.GetAsync();

            var leaveTypesDto = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            _logger.LogInformation("Leave types were retrived successfully.");

            return leaveTypesDto;
        }
    }
}
