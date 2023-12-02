using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Presistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper)
        {
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationCommandValidator(_leaveAllocationRepository, _leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new BadRequestException("Invalid Leave Allocation Request", validationResult);

            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

            if (leaveAllocation is null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            _mapper.Map(request, leaveAllocation);
            await _leaveAllocationRepository.UpdateAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}
