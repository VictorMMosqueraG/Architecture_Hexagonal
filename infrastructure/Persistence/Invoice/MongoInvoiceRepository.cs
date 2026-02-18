namespace Infrastructure.Persistence.MongoDB.Invoices;

using Core.Entities;
using Core.Interfaces.Repositories;
using global::MongoDB.Driver;
using Infrastructure.Persistence.MongoDB.Base;
using Microsoft.Extensions.Configuration;

public class MongoInvoiceRepository : MongoBaseRepository<InvoiceDocument>, IInvoiceRepository
{
    public MongoInvoiceRepository(IConfiguration config)
        : base(config, "invoices") { }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        var docs = await Collection.Find(_ => true).ToListAsync();
        return docs.Select(MapToDomain);
    }

    public async Task<IEnumerable<Invoice>> GetByClientIdAsync(string clientId)
    {
        var filter = Builders<InvoiceDocument>.Filter.Eq(x => x.ClientId, clientId);
        var docs   = await Collection.Find(filter).ToListAsync();
        return docs.Select(MapToDomain);
    }

    public async Task<IEnumerable<Invoice>> GetByStatusAsync(string status)
    {
        var filter = Builders<InvoiceDocument>.Filter.Eq(x => x.Status, status);
        var docs   = await Collection.Find(filter).ToListAsync();
        return docs.Select(MapToDomain);
    }

    public async Task<Invoice?> GetByIdAsync(string id)
    {
        var filter = Builders<InvoiceDocument>.Filter.Eq(x => x.Id, id);
        var doc    = await Collection.Find(filter).FirstOrDefaultAsync();
        return doc is null ? null : MapToDomain(doc);
    }

    public async Task<Invoice> CreateAsync(Invoice invoice)
    {
        var doc = new InvoiceDocument
        {
            ClientId      = invoice.ClientId,
            InvoiceNumber = invoice.InvoiceNumber,
            Amount        = invoice.Amount,
            DueDate       = invoice.DueDate,
            Status        = invoice.Status,
            Description   = invoice.Description,
            CreatedAt     = invoice.CreatedAt,
            UpdatedAt     = invoice.UpdatedAt
        };

        await Collection.InsertOneAsync(doc);

        invoice.Id = doc.Id;
        return invoice;
    }

    public async Task UpdateStatusAsync(string invoiceId, string newStatus)
    {
        var filter = Builders<InvoiceDocument>.Filter.Eq(x => x.Id, invoiceId);
        var update = Builders<InvoiceDocument>.Update
            .Set(x => x.Status,    newStatus)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        await Collection.UpdateOneAsync(filter, update);
    }

    public async Task<Invoice?> GetByInvoiceNumberAsync(string invoiceNumber)  
    {
        var filter = Builders<InvoiceDocument>.Filter.Eq(x => x.InvoiceNumber, invoiceNumber);
        var doc    = await Collection.Find(filter).FirstOrDefaultAsync();
        return doc is null ? null : MapToDomain(doc);
    }

    private static Invoice MapToDomain(InvoiceDocument doc) => new()
    {
        Id            = doc.Id,
        ClientId      = doc.ClientId,
        InvoiceNumber = doc.InvoiceNumber,
        Amount        = doc.Amount,
        DueDate       = doc.DueDate,
        Status        = doc.Status,
        Description   = doc.Description,
        CreatedAt     = doc.CreatedAt,
        UpdatedAt     = doc.UpdatedAt
    };
}