namespace Application.Features.Reminders.ProcessReminder.Helpers;

using Core.Constants;

internal static class ReminderTransitions
{
    internal static readonly Dictionary<string, (string ReminderType, string NewStatus)> Map = new()
    {
        [InvoiceStatus.FirstReminder]  = (ReminderType.FirstReminder,  InvoiceStatus.SecondReminder),
        [InvoiceStatus.SecondReminder] = (ReminderType.SecondReminder, InvoiceStatus.Disabled)
    };
}