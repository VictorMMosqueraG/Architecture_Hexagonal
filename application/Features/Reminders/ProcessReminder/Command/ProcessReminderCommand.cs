namespace Application.Features.Reminders.ProcessReminder.Command;

using Application.Features.Reminders.ProcessReminder.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;

public record ProcessRemindersCommand() : IRequest<ResultDto<ProcessRemindersResponseDto>>;