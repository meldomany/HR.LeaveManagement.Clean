using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Presistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(e => e.NumberOfDays)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

            RuleFor(e => e.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("{PorpertyName} must be after {ComparisonValue}.");

            RuleFor(e => e.Id)
                .MustAsync(LeaveAllocationExist).WithMessage("{PropertyName} must be present.");

            RuleFor(e => e.LeaveTypeId)
                .MustAsync(LeaveTypeExist).WithMessage("{PropertyName} must be exist.");

            this._leaveAllocationRepository = leaveAllocationRepository;
            this._leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveAllocationExist(int leaveAllocationId, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(leaveAllocationId);
            return leaveAllocation != null;
        }

        private async Task<bool> LeaveTypeExist(int leaveTypeId, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(leaveTypeId);
            return leaveType != null;
        }
    }
}
