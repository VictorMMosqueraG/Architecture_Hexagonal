namespace Tests.Unit.Application.Clients.GetByIdClient;

using Core.Exceptions;
using FluentAssertions;
using global::Application.Features.Clients.GetByIdClient.Queries;
using Tests.Builders;
using Tests.Stubs.Clients;
using Xunit;

public class GetByIdClientQueryHandlerTests
{
    private readonly ClientRepositoryStub _repositoryStub = new();
    private readonly GetClientByIdQueryHandler _handler;

    public GetByIdClientQueryHandlerTests()
    {
        _handler = new GetClientByIdQueryHandler(_repositoryStub.Object);
    }

    [Fact]
    public async Task Handle_WhenClientExists_ShouldReturnClient()
    {
        var client = ClientBuilder.Default();
        _repositoryStub.IdExists(client.Id, client);

        var result = await _handler.Handle(new GetByIdClientQuery { Id = client.Id }, CancellationToken.None);

        result.Should().NotBeNull();
        result.Results!.Id.Should().Be(client.Id);
    }

    [Fact]
    public async Task Handle_WhenClientExists_ShouldMapAllFieldsCorrectly()
    {
        var client = ClientBuilder.Default();
        _repositoryStub.IdExists(client.Id, client);

        var result = await _handler.Handle(new GetByIdClientQuery { Id = client.Id }, CancellationToken.None);

        result.Results!.Id.Should().Be(client.Id);
        result.Results.Name.Should().Be(client.Name);
        result.Results.Email.Should().Be(client.Email);
        result.Results.DocumentNumber.Should().Be(client.DocumentNumber);
        result.Results.Phone.Should().Be(client.Phone);
        result.Results.Status.Should().Be(client.Status);
        result.Results.CreatedAt.Should().Be(client.CreatedAt);
    }

    [Fact]
    public async Task Handle_WhenClientExists_ShouldReturnSuccessMessage()
    {
        var client = ClientBuilder.Default();
        _repositoryStub.IdExists(client.Id, client);

        var result = await _handler.Handle(new GetByIdClientQuery { Id = client.Id }, CancellationToken.None);

        result.Message.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Handle_WhenClientNotFound_ShouldThrowNotFoundException()
    {
        _repositoryStub.IdNotExists("non-existent-id");

        var act = async () => await _handler.Handle(
            new GetByIdClientQuery { Id = "non-existent-id" },
            CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_WhenClientNotFound_ShouldIncludeIdInMessage()
    {
        const string id = "non-existent-id";
        _repositoryStub.IdNotExists(id);

        var act = async () => await _handler.Handle(
            new GetByIdClientQuery { Id = id },
            CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{id}*");
    }

    [Fact]
    public async Task Handle_ShouldCallRepositoryWithCorrectId()
    {
        const string id = "6994d1917bd3c08095284d0c";
        var client = new ClientBuilder().WithId(id).Build();
        _repositoryStub.IdExists(id, client);

        await _handler.Handle(new GetByIdClientQuery { Id = id }, CancellationToken.None);

        _repositoryStub.VerifyGetByIdCalledOnceWith(id);
    }
}