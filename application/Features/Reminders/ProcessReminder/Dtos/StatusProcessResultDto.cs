namespace Application.Features.Reminders.ProcessReminder.Dtos;

internal record StatusProcessResult(
    int Processed,
    List<string> Errors
);