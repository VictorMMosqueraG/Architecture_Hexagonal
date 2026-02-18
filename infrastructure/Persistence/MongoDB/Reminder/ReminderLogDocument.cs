namespace Infrastructure.Persistence.MongoDB.Reminder;

using global::MongoDB.Bson;
using global::MongoDB.Bson.Serialization.Attributes;

public class ReminderLogDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("invoiceId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string InvoiceId { get; set; } = string.Empty;

    [BsonElement("clientId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ClientId { get; set; } = string.Empty;

    [BsonElement("reminderType")]
    public string ReminderType { get; set; } = string.Empty;

    [BsonElement("sentAt")]
    public DateTime SentAt { get; set; }

    [BsonElement("statusBefore")]
    public string StatusBefore { get; set; } = string.Empty;

    [BsonElement("statusAfter")]
    public string StatusAfter { get; set; } = string.Empty;

    [BsonElement("emailSentTo")]
    public string EmailSentTo { get; set; } = string.Empty;

    [BsonElement("success")]
    public bool Success { get; set; }

    [BsonElement("errorMessage")]
    public string? ErrorMessage { get; set; }
}