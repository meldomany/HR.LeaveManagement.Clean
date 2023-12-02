﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Presistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper,
            IEmailSender emailSender,
            IAppLogger<UpdateLeaveRequestCommandHandler> logger)
        {
            this._leaveRequestRepository = leaveRequestRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
            this._emailSender = emailSender;
            this._logger = logger;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);

            var validator = new UpdateLeaveRequestValidator(_leaveRequestRepository, _leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new BadRequestException("Invalid Leave Request", validationResult);

            _mapper.Map(request, leaveRequest);
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                    $"has been updated successfully.",
                    Subject = "Leave Request Submitted"
                };
                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }
            return Unit.Value;
        }
    }
}
