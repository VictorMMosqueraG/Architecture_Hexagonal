namespace Core.Entities;

public class ReminderLog
{
    public string Id { get; set; } = string.Empty;
    public string InvoiceId { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ReminderType { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
    public string StatusBefore { get; set; } = string.Empty;
    public string StatusAfter { get; set; } = string.Empty;
    public string EmailSentTo { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}