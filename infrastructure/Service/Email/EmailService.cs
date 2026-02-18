namespace Infrastructure.Service.Email;

using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

public class EmailService(
    IConfiguration        config
) : IEmailService
{

    private readonly SmtpSettings _settings = new()
    {
        Host      = config["Smtp:Host"]     ?? throw new ArgumentNullException("Smtp:Host"),
        Port      = int.Parse(config["Smtp:Port"]    ?? "587"),
        EnableSsl = bool.Parse(config["Smtp:EnableSsl"] ?? "true"),
        Username  = config["Smtp:Username"] ?? throw new ArgumentNullException("Smtp:Username"),
        Password  = config["Smtp:Password"] ?? throw new ArgumentNullException("Smtp:Password"),
        From      = config["Smtp:From"]     ?? throw new ArgumentNullException("Smtp:From"),
        FromName  = config["Smtp:FromName"] ?? "Sistema de Facturación"
    };

    public async Task SendFirstReminderAsync(Invoice invoice, string clientEmail, string clientName)
    {
        var subject = $"Recordatorio de pago - Factura {invoice.InvoiceNumber}";
        var body    = EmailTemplates.FirstReminder(invoice, clientName);
        await SendAsync(clientEmail, clientName, subject, body);
    }

    public async Task SendSecondReminderAsync(Invoice invoice, string clientEmail, string clientName)
    {
        var subject = $"Último aviso - Factura {invoice.InvoiceNumber}";
        var body    = EmailTemplates.SecondReminder(invoice, clientName);
        await SendAsync(clientEmail, clientName, subject, body);
    }

    private async Task SendAsync(string toEmail, string toName, string subject, string body)
    {
        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl   = _settings.EnableSsl,
            Credentials = new NetworkCredential(_settings.Username, _settings.Password)
        };

        using var message = new MailMessage
        {
            From       = new MailAddress(_settings.From, _settings.FromName),
            Subject    = subject,
            Body       = body,
            IsBodyHtml = true
        };

        message.To.Add(new MailAddress(toEmail, toName));

        await client.SendMailAsync(message);
    }
}