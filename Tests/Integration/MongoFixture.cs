namespace Tests.Integration;

using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Testcontainers.MongoDb;
using Xunit;

public class MongoFixture : IAsyncLifetime
{
    private readonly MongoDbContainer _container = new MongoDbBuilder()
        .WithImage("mongo:7.0")
        .Build();

    public IConfiguration Configuration { get; private set; } = null!;
    public IMongoDatabase Database      { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        Configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["MongoDB:ConnectionString"] = _container.GetConnectionString(),
                ["MongoDB:DatabaseName"]     = "billing_test"
            })
            .Build();

        var client = new MongoClient(_container.GetConnectionString());
        Database   = client.GetDatabase("billing_test");
    }

    public async Task DropCollectionAsync(string collectionName) =>
        await Database.DropCollectionAsync(collectionName);

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
        await _container.DisposeAsync();
    }
}