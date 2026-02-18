namespace Application.Features.Reminders.ProcessReminder.Command;

using Application.Features.Reminders.ProcessReminder.Dtos;
using Application.Features.Reminders.ProcessReminder.Helpers;
using Core.Dtos.ResponsesDto;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Messages;
using MediatR;

public class ProcessRemindersCommandHandler(
    IInvoiceRepository     invoiceRepository,
    IClientRepository      clientRepository,
    IEmailService          emailService,
    IReminderLogRepository reminderLogRepository
) : IRequestHandler<ProcessRemindersCommand, ResultDto<ProcessRemindersResponseDto>>
{
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
    private readonly InvoiceProcessor   _processor         = new(clientRepository, emailService, reminderLogRepository);

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