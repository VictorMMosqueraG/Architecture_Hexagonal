namespace Infrastructure.Persistence.MongoDB.Invoices;

using global::MongoDB.Bson;
using global::MongoDB.Bson.Serialization.Attributes;

public class InvoiceDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("clientId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ClientId { get; set; } = string.Empty;

    [BsonElement("invoiceNumber")]
    public string InvoiceNumber { get; set; } = string.Empty;

    [BsonElement("amount")]
    public decimal Amount { get; set; }

    [BsonElement("dueDate")]
    public DateTime DueDate { get; set; }

    [BsonElement("status")]
    public string Status { get; set; } = string.Empty;

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}