namespace Infrastructure.Persistence.MongoDB.Clients;

using Core.Entities;
using Core.Interfaces.Repositories;
using global::MongoDB.Driver;
using Infrastructure.Persistence.MongoDB.Base;
using Microsoft.Extensions.Configuration;

public class MongoClientRepository : MongoBaseRepository<ClientDocument>, IClientRepository
{
    private const string Entity = "Clients";
    public MongoClientRepository(IConfiguration config)
        : base(config, Entity) { }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        var docs = await Collection.Find(_ => true).ToListAsync();
        return docs.Select(MapToDomain);
    }

    public async Task<Client> CreateAsync(Client client)
    {
        var doc = new ClientDocument
        {
            Name           = client.Name,
            Email          = client.Email,
            DocumentNumber = client.DocumentNumber,
            Phone          = client.Phone,
            Status         = client.Status,
            CreatedAt      = client.CreatedAt,
            UpdatedAt      = client.UpdatedAt
        };

        await Collection.InsertOneAsync(doc);

        client.Id = doc.Id;
        return client;
    }

    private static Client MapToDomain(ClientDocument doc) => new()
    {
        Id             = doc.Id,
        Name           = doc.Name,
        Email          = doc.Email,
        DocumentNumber = doc.DocumentNumber,
        Phone          = doc.Phone,
        Status         = doc.Status,
        CreatedAt      = doc.CreatedAt,
        UpdatedAt      = doc.UpdatedAt
    };
}