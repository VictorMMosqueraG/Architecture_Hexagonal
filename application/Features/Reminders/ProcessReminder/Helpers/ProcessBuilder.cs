namespace Application.Features.Reminders.ProcessReminder.Helpers;

using Application.Features.Reminders.ProcessReminder.Dtos;

internal static class ProcessResultBuilder
{
    internal static ProcessRemindersResponseDto Build(StatusProcessResult[] results) => new(
        TotalProcessed:           results.Sum(r => r.Processed),
        UpgradedToSecondReminder: results[0].Processed,
        UpgradedToDisabled:       results[1].Processed,
        Errors:                   results.SelectMany(r => r.Errors).ToList()
    );
}