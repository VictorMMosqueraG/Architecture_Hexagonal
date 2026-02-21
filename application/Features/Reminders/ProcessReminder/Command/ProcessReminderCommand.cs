namespace Application.Features.Reminders.ProcessReminder.Command;

using Application.Features.Reminders.ProcessReminder.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;

/// <summary>
/// Comando para procesar y enviar recordatorios de pago a clientes con facturas vencidas.
/// No requiere parámetros de entrada.
/// </summary>
public record ProcessRemindersCommand() : IRequest<ResultDto<ProcessRemindersResponseDto>>;