namespace Tests.Integration.Clients;

using Core.Constants;
using FluentAssertions;
using Tests.Builders;
using Tests.Integration.Helpers;
using Xunit;

[Collection("Mongo")]
public class MongoClientRepositoryIntegrationTests : IAsyncLifetime
{
    private readonly ClientDatabaseHelper _db;

    public MongoClientRepositoryIntegrationTests(MongoFixture fixture)
    {
        _db = new ClientDatabaseHelper(fixture);
    }

    public async Task InitializeAsync() => await _db.CleanAsync();
    public Task DisposeAsync()          => Task.CompletedTask;


    [Fact]
    public async Task CreateAsync_ShouldPersistClientInDatabase()
    {
        var client = ClientBuilder.Default();

        var created = await _db.Repository.CreateAsync(client);

        created.Should().NotBeNull();
        created.Id.Should().NotBeNullOrEmpty();
        created.Name.Should().Be(client.Name);
        created.Email.Should().Be(client.Email);
    }

    [Fact]
    public async Task CreateAsync_ShouldAssignMongoObjectId()
    {
        var created = await _db.Repository.CreateAsync(ClientBuilder.Default());

        created.Id.Should().HaveLength(24);
    }

    [Fact]
    public async Task CreateAsync_ShouldPersistAllFields()
    {
        var builder = new ClientBuilder()
            .WithName("Victor Mosquera")
            .WithEmail("victor@example.com")
            .WithDocumentNumber("987654321")
            .WithPhone("3009876543")
            .WithStatus(ClientStatus.Active);

        var created = await _db.SeedOneAsync(builder);

        created.Name.Should().Be("Victor Mosquera");
        created.Email.Should().Be("victor@example.com");
        created.DocumentNumber.Should().Be("987654321");
        created.Phone.Should().Be("3009876543");
        created.Status.Should().Be(ClientStatus.Active);
    }


    [Fact]
    public async Task GetByIdAsync_WhenClientExists_ShouldReturnClient()
    {
        var seeded = await _db.SeedOneAsync();

        var found = await _db.Repository.GetByIdAsync(seeded.Id);

        found.Should().NotBeNull();
        found!.Id.Should().Be(seeded.Id);
        found.Email.Should().Be(seeded.Email);
    }

    [Fact]
    public async Task GetByIdAsync_WhenClientNotExists_ShouldReturnNull()
    {
        var found = await _db.Repository.GetByIdAsync("000000000000000000000000");

        found.Should().BeNull();
    }


    [Fact]
    public async Task GetByEmailAsync_WhenEmailExists_ShouldReturnClient()
    {
        var seeded = await _db.SeedOneAsync(new ClientBuilder().WithEmail("test@example.com"));

        var found = await _db.Repository.GetByEmailAsync("test@example.com");

        found.Should().NotBeNull();
        found!.Id.Should().Be(seeded.Id);
    }

    [Fact]
    public async Task GetByEmailAsync_WhenEmailNotExists_ShouldReturnNull()
    {
        var found = await _db.Repository.GetByEmailAsync("noexiste@example.com");

        found.Should().BeNull();
    }

    [Fact]
    public async Task GetByEmailAsync_ShouldBeCaseSensitive()
    {
        await _db.SeedOneAsync(new ClientBuilder().WithEmail("Test@Example.com"));

        var found = await _db.Repository.GetByEmailAsync("test@example.com");

        found.Should().BeNull();
    }


    [Fact]
    public async Task GetAllAsync_ShouldReturnAllClients()
    {
        await _db.SeedManyAsync(3);

        var (data, total) = await _db.Repository.GetAllAsync(1, 10, null, null);

        data.Should().HaveCount(3);
        total.Should().Be(3);
    }

    [Fact]
    public async Task GetAllAsync_ShouldRespectPagination()
    {
        await _db.SeedManyAsync(5);

        var (page1, total) = await _db.Repository.GetAllAsync(1, 2, null, null);
        var (page2, _)     = await _db.Repository.GetAllAsync(2, 2, null, null);

        total.Should().Be(5);
        page1.Should().HaveCount(2);
        page2.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetAllAsync_WhenEmpty_ShouldReturnEmptyList()
    {
        var (data, total) = await _db.Repository.GetAllAsync(1, 10, null, null);

        data.Should().BeEmpty();
        total.Should().Be(0);
    }

    [Fact]
    public async Task GetAllAsync_ShouldSortByNameAscending()
    {
        await _db.SeedWithNamesAsync("Zara", "Ana", "Mario");

        var (data, _) = await _db.Repository.GetAllAsync(1, 10, "name", "asc");

        data.Select(c => c.Name).Should().BeInAscendingOrder();
    }

    [Fact]
    public async Task GetAllAsync_ShouldSortByNameDescending()
    {
        await _db.SeedWithNamesAsync("Ana", "Zara");

        var (data, _) = await _db.Repository.GetAllAsync(1, 10, "name", "desc");

        data.Select(c => c.Name).Should().BeInDescendingOrder();
    }
}