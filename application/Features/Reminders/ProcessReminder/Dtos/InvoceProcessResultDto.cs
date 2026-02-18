namespace Application.Features.Reminders.ProcessReminder.Dtos;

internal record InvoiceProcessResult(
    bool    Success,
    string? ErrorMessage
);