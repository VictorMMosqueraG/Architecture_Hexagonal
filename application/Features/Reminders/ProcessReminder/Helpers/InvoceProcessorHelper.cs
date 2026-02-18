namespace Application.Features.Reminders.ProcessReminder.Helpers;

using Application.Features.Reminders.ProcessReminder.Dtos;
using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Messages;

internal class InvoiceProcessor(
    IClientRepository      clientRepository,
    IEmailService          emailService,
    IReminderLogRepository reminderLogRepository
)
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IEmailService _emailService = emailService;
    private readonly IReminderLogRepository _reminderLogRepository = reminderLogRepository;

    internal async Task<InvoiceProcessResult> ProcessAsync(
        Invoice invoice,
        string  reminderType,
        string  newStatus
    ){
        var emailSentTo  = string.Empty;
        var success      = false;
        string? errorMessage = null;

        try
        {
            var client = await _clientRepository.GetByIdAsync(invoice.ClientId)
                ?? throw new NotFoundException(Message.NotFoundEntity("Cliente", invoice.ClientId));

            emailSentTo = client.Email;

            await SendEmailAsync(reminderType, invoice, client.Email, client.Name);
            success = true;
        }
        catch (Exception ex)
        {
            errorMessage = $"Factura {invoice.InvoiceNumber}: {ex.Message}";
        }
        finally
        {
            var log = ReminderLogBuilder.Build(invoice, reminderType, newStatus, emailSentTo, success, errorMessage);
            await _reminderLogRepository.CreateAsync(log);
        }

        return new InvoiceProcessResult(success, errorMessage);
    }

    private Task SendEmailAsync(
        string  reminderType,
        Invoice invoice,
        string  clientEmail,
        string  clientName) => reminderType switch
    {
        ReminderType.FirstReminder  => _emailService.SendFirstReminderAsync(invoice, clientEmail, clientName),
        ReminderType.SecondReminder => _emailService.SendSecondReminderAsync(invoice, clientEmail, clientName),
        _ => throw new ArgumentException(Message.InvalidSupportData(reminderType))
    };
}