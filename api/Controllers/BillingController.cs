namespace Api.Controllers;

using Application.Features.Reminders.ProcessReminder.Command;
using Application.Features.Reminders.ProcessReminder.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/billing")]
public class BillingController(
    IMediator mediator
) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Proceso para enviar la notificación,
    /// y actualizar su estado
    /// </summary>
    [HttpPost("process-reminders")]
    [ProducesResponseType(typeof(ResultDto<ProcessRemindersResponseDto>), StatusCodes.Status200OK)]
    public async Task<ResultDto<ProcessRemindersResponseDto>> ProcessReminders()
        => await _mediator.Send(new ProcessRemindersCommand());
}


