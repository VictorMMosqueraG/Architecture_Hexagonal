namespace Tests.Integration.Helpers;

using Core.Entities;
using Infrastructure.Persistence.MongoDB.Clients;
using Tests.Builders;

public class ClientDatabaseHelper
{
    private readonly MongoClientRepository _repository;
    private readonly MongoFixture          _fixture;

    private const string CollectionName = "Clients";

    public ClientDatabaseHelper(MongoFixture fixture)
    {
        _fixture    = fixture;
        _repository = new MongoClientRepository(fixture.Configuration);
    }

    public MongoClientRepository Repository => _repository;


    public async Task CleanAsync() =>
        await _fixture.DropCollectionAsync(CollectionName);


    public async Task<Client> SeedOneAsync(ClientBuilder? builder = null)
    {
        var client = (builder ?? new ClientBuilder()).Build();
        return await _repository.CreateAsync(client);
    }

    public async Task<List<Client>> SeedManyAsync(int count)
    {
        var created = new List<Client>();
        for (var i = 1; i <= count; i++)
            created.Add(await _repository.CreateAsync(
                new ClientBuilder()
                    .WithEmail($"client{i}@example.com")
                    .Build()
            ));
        return created;
    }

    public async Task<List<Client>> SeedWithNamesAsync(params string[] names)
    {
        var created = new List<Client>();
        foreach (var name in names)
            created.Add(await _repository.CreateAsync(
                new ClientBuilder()
                    .WithName(name)
                    .WithEmail($"{name.ToLower()}@example.com")
                    .Build()
            ));
        return created;
    }
}