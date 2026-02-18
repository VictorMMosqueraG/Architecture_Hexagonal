namespace Application.Features.Reminders.ProcessReminder.Helpers;

using Core.Entities;

internal static class ReminderLogBuilder
{
    internal static ReminderLog Build(
        Invoice invoice,
        string  reminderType,
        string  newStatus,
        string  emailSentTo,
        bool    success,
        string? errorMessage) => new()
    {
        InvoiceId    = invoice.Id,
        ClientId     = invoice.ClientId,
        ReminderType = reminderType,
        SentAt       = DateTime.UtcNow,
        StatusBefore = invoice.Status,
        StatusAfter  = success ? newStatus : invoice.Status,
        EmailSentTo  = emailSentTo,
        Success      = success,
        ErrorMessage = errorMessage ?? string.Empty
    };
}