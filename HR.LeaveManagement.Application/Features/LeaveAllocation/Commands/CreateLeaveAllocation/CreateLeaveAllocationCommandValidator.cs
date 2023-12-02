using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Presistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {

            RuleFor(e => e.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeExist).WithMessage("{PropertyName} does not exist.");

            _leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveTypeExist(int leaveTypeId, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(leaveTypeId);
            return leaveType != null;
        }
    }
}
