namespace Application.Features.Reminders.ProcessReminder.Command;

using Application.Features.Reminders.ProcessReminder.Dtos;
using Application.Features.Reminders.ProcessReminder.Helpers;
using Core.Dtos.ResponsesDto;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Messages;
using MediatR;

/// <summary>
/// Handler que procesa <see cref="ProcessRemindersCommand"/>.
/// Itera sobre las transiciones de estado definidas en <see cref="ReminderTransitions"/>,
/// envía recordatorios por email y actualiza el estado de cada factura.
/// </summary>
public class ProcessRemindersCommandHandler(
    IInvoiceRepository     invoiceRepository,
    IClientRepository      clientRepository,
    IEmailService          emailService,
    IReminderLogRepository reminderLogRepository
) : IRequestHandler<ProcessRemindersCommand, ResultDto<ProcessRemindersResponseDto>>
{
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
    private readonly InvoiceProcessor   _processor         = new(clientRepository, emailService, reminderLogRepository);

    /// <summary>
    /// Ejecuta el proceso de recordatorios para todos los estados configurados en paralelo.
    /// </summary>
    /// <param name="request">Comando sin parámetros.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Resultado con el resumen de facturas procesadas y errores ocurridos.</returns>
    public async Task<ResultDto<ProcessRemindersResponseDto>> Handle(
        ProcessRemindersCommand request,
        CancellationToken cancellationToken)
    {
        var results = await Task.WhenAll(
            ReminderTransitions.Map.Select(t =>
                ProcessByStatusAsync(t.Key, t.Value.ReminderType, t.Value.NewStatus)
            )
        );

        var response = ResultDto<ProcessRemindersResponseDto>.Success(ProcessResultBuilder.Build(results));
        response.Message = Message.EmailSentValid;

        return response;
    }

    /// <summary>
    /// Procesa todas las facturas de un estado dado, enviando el recordatorio correspondiente
    /// y actualizando su estado al siguiente en la cadena.
    /// </summary>
    /// <param name="currentStatus">Estado actual de las facturas a procesar.</param>
    /// <param name="reminderType">Tipo de recordatorio a enviar (ej: FirstReminder, SecondReminder).</param>
    /// <param name="newStatus">Nuevo estado a asignar tras el procesamiento.</param>
    /// <returns>Resultado con el conteo de éxitos y lista de errores.</returns>
    private async Task<StatusProcessResult> ProcessByStatusAsync(
        string currentStatus,
        string reminderType,
        string newStatus)
    {
        var invoices = await _invoiceRepository.GetByStatusAsync(currentStatus);

        var results = await Task.WhenAll(
            invoices.Select(invoice => _processor.ProcessAsync(invoice, reminderType, newStatus))
        );

        return new StatusProcessResult(
            Processed: results.Count(r => r.Success),
            Errors:    results.Where(r => !r.Success)
                              .Select(r => r.ErrorMessage!)
                              .ToList()
        );
    }
}