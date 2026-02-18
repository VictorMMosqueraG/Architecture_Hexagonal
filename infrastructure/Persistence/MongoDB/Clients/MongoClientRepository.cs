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

    public async Task<(IEnumerable<Client> Data, long Total)> GetAllAsync(
        int page,
        int pageSize,
        string? sort,
        string? order
    ){
        var filter = Builders<ClientDocument>.Filter.Empty;

        var (docs, total) = await GetPaginatedAsync(filter, page, pageSize, sort, order);

        return (docs.Select(MapToDomain), total);
    }

    public async Task<Client?> GetByIdAsync(string id)
    {
        var filter = Builders<ClientDocument>.Filter.Eq(x => x.Id, id);
        var doc = await Collection.Find(filter).FirstOrDefaultAsync();
        return doc is null ? null : MapToDomain(doc);
    }

    public async Task<Client?> GetByEmailAsync(string email)
    {
        var filter = Builders<ClientDocument>.Filter.Eq(x => x.Email, email);
        var doc = await Collection.Find(filter).FirstOrDefaultAsync();
        return doc is null ? null : MapToDomain(doc);
    }

    public async Task<Client> CreateAsync(Client client)
    {
        var doc = new ClientDocument
        {
            Name = client.Name,
            Email = client.Email,
            DocumentNumber = client.DocumentNumber,
            Phone = client.Phone,
            Status = client.Status,
            CreatedAt = client.CreatedAt,
            UpdatedAt = client.UpdatedAt
        };

        await Collection.InsertOneAsync(doc);

        client.Id = doc.Id;
        return client;
    }

    private static Client MapToDomain(ClientDocument doc) => new()
    {
        Id = doc.Id,
        Name = doc.Name,
        Email = doc.Email,
        DocumentNumber = doc.DocumentNumber,
        Phone = doc.Phone,
        Status = doc.Status,
        CreatedAt = doc.CreatedAt,
        UpdatedAt = doc.UpdatedAt
    };
}