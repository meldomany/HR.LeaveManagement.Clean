using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Presistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly Mapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, Mapper mapper)
        {
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Any())
                throw new BadRequestException("Invalid Leave Type", validation);


            var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

            await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

            return leaveTypeToCreate.Id;
        }
    }
}
