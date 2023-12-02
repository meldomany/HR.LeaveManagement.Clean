using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Presistence;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared
{
    public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(e => e.StartDate)
                .LessThan(e => e.EndDate)
                .WithMessage("{PropertyName} must be before {ComparisonValue}.");

            RuleFor(e => e.EndDate)
                .GreaterThan(e => e.StartDate)
                .WithMessage("{PropertyName} must be after {ComparisonValue}.");

            RuleFor(e => e.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeExsit)
                .WithMessage("{PropertyName} does not exist.");
            
            this._leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveTypeExsit(int leaveTypeId, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(leaveTypeId);
            return leaveType != null;
        }
    }
}
