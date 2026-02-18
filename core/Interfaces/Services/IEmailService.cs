namespace Core.Interfaces.Services;

using Core.Entities;
public interface IEmailService
{
    Task SendFirstReminderAsync(Invoice invoice, string clientEmail, string clientName);
    Task SendSecondReminderAsync(Invoice invoice, string clientEmail, string clientName);
}