namespace Tests.Unit.Application.Clients.GetAllClient;

using Core.Entities;
using FluentAssertions;
using global::Application.Features.Clients.GetAllClient.Queries;
using Tests.Builders;
using Tests.Stubs.Clients;
using Xunit;

public class GetAllClientQueryHandlerTests
{
    private readonly ClientRepositoryStub _repositoryStub = new();
    private readonly GetAllClientQueryHandler _handler;

    public GetAllClientQueryHandlerTests()
    {
        _handler = new GetAllClientQueryHandler(_repositoryStub.Object);
    }

    [Fact]
    public async Task Handle_WhenClientsExist_ShouldReturnPaginatedResult()
    {
        _repositoryStub.GetAllReturns(BuildClientList(), total: 3);

        var result = await _handler.Handle(BuildQuery(), CancellationToken.None);

        result.Should().NotBeNull();
        result.Results.Should().HaveCount(3);
    }

    [Fact]
    public async Task Handle_WhenClientsExist_ShouldMapFieldsCorrectly()
    {
        var client = ClientBuilder.Default();
        _repositoryStub.GetAllReturns(new List<Client> { client }, total: 1);

        var result = await _handler.Handle(BuildQuery(), CancellationToken.None);

        var first = result.Results!.First();
        first.Id.Should().Be(client.Id);
        first.Name.Should().Be(client.Name);
        first.Email.Should().Be(client.Email);
        first.DocumentNumber.Should().Be(client.DocumentNumber);
        first.Status.Should().Be(client.Status);
    }

    [Fact]
    public async Task Handle_WhenNoClientsExist_ShouldReturnEmptyList()
    {
        _repositoryStub.GetAllReturnsEmpty();

        var result = await _handler.Handle(BuildQuery(), CancellationToken.None);

        result.Results.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_ShouldPassPaginationParametersToRepository()
    {
        var query = BuildQuery(page: 2, pageSize: 5, sort: "name", order: "asc");
        _repositoryStub.GetAllWithParamsReturns(2, 5, "name", "asc", Enumerable.Empty<Client>(), 0);

        await _handler.Handle(query, CancellationToken.None);

        _repositoryStub.VerifyGetAllCalledWith(2, 5, "name", "asc");
    }

    [Fact]
    public async Task Handle_ShouldReturnCorrectTotalCount()
    {
        _repositoryStub.GetAllReturns(BuildClientList(), total: 10);

        var result = await _handler.Handle(BuildQuery(), CancellationToken.None);

        result.Total.Should().Be(10);
    }

    private static GetAllClientQuery BuildQuery(
        int page = 1,
        int pageSize = 10,
        string? sort = null,
        string? order = null) =>
        new()
        {
            Page = page,
            PageSize = pageSize,
            Sort = sort,
            Order = order
        };

    private static List<Client> BuildClientList() =>
    [
        new ClientBuilder().WithId("1").WithEmail("carlos@example.com").Build(),
        new ClientBuilder().WithId("2").WithEmail("laura@example.com").Build(),
        new ClientBuilder().WithId("3").WithEmail("tech@example.com").Build()
    ];
}