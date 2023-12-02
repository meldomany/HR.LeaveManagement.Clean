﻿using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommand : BaseLeaveRequestDto, IRequest<Unit>
    {
        public int Id { get; set; }
        public string? RequestComments { get; set; }
        public bool Cancelled { get; set; }
    }
}
