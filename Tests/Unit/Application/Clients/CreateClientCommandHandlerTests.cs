namespace Tests.Unit.Application.Clients.CreateClient;

using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using FluentAssertions;
using global::Application.Features.Clients.CreateClient.Command;
using Moq;
using Tests.Builders;
using Tests.Stubs.Clients;
using Xunit;

public class CreateClientCommandHandlerTests
{
    private readonly ClientRepositoryStub _repositoryStub = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly CreateClientCommandHandler _handler;

    public CreateClientCommandHandlerTests()
    {
        _handler = new CreateClientCommandHandler(
            _repositoryStub.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task Handle_WhenEmailDoesNotExist_ShouldCreateClientSuccessfully()
    {
        var command = BuildCommand();
        var client = ClientBuilder.Default();

        _repositoryStub.EmailNotExists(command.Email).CreateSucceeds(client);
        _mapperMock.Setup(m => m.Map<Client>(command)).Returns(client);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Results.Should().BeEquivalentTo(client);
        _repositoryStub.VerifyCreateCalledOnce();
    }

    [Fact]
    public async Task Handle_WhenEmailDoesNotExist_ShouldReturnSuccessMessage()
    {
        var command = BuildCommand();
        var client = ClientBuilder.Default();

        _repositoryStub.EmailNotExists(command.Email).CreateSucceeds(client);
        _mapperMock.Setup(m => m.Map<Client>(command)).Returns(client);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Message.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Handle_WhenEmailAlreadyExists_ShouldThrowConflictException()
    {
        var command = BuildCommand();
        _repositoryStub.EmailExists(command.Email);

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>();
    }

    [Fact]
    public async Task Handle_WhenEmailAlreadyExists_ShouldNotCallCreateAsync()
    {
        var command = BuildCommand();
        _repositoryStub.EmailExists(command.Email);

        var act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<ConflictException>();

        _repositoryStub.VerifyCreateNeverCalled();
    }

    [Fact]
    public async Task Handle_WhenRepositoryFails_ShouldPropagateException()
    {
        var command = BuildCommand();
        var client = ClientBuilder.Default();

        _repositoryStub.EmailNotExists(command.Email).CreateFails("DB Error");
        _mapperMock.Setup(m => m.Map<Client>(command)).Returns(client);

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("DB Error");
    }

    private static CreateClientCommand BuildCommand(
        string name = "Carlos Mendoza",
        string email = "carlos@example.com",
        string documentNumber = "123456789",
        string? phone = "3001234567") =>
        new()
        {
            Name = name,
            Email = email,
            DocumentNumber = documentNumber,
            Phone = phone
        };
}