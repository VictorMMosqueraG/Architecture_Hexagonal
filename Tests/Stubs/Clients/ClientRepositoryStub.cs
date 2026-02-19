namespace Tests.Stubs.Clients;

using Core.Entities;
using Core.Interfaces.Repositories;
using Moq;
using Tests.Builders;

public class ClientRepositoryStub
{
    public Mock<IClientRepository> Mock { get; } = new();
    public IClientRepository Object => Mock.Object;


    public ClientRepositoryStub EmailExists(string email, Client? client = null)
    {
        Mock.Setup(r => r.GetByEmailAsync(email))
            .ReturnsAsync(client ?? ClientBuilder.Default());
        return this;
    }

    public ClientRepositoryStub EmailNotExists(string email)
    {
        Mock.Setup(r => r.GetByEmailAsync(email))
            .ReturnsAsync((Client?)null);
        return this;
    }


    public ClientRepositoryStub IdExists(string id, Client? client = null)
    {
        Mock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(client ?? new ClientBuilder().WithId(id).Build());
        return this;
    }

    public ClientRepositoryStub IdNotExists(string id)
    {
        Mock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync((Client?)null);
        return this;
    }

    public ClientRepositoryStub CreateSucceeds(Client? returns = null)
    {
        Mock.Setup(r => r.CreateAsync(It.IsAny<Client>()))
            .ReturnsAsync(returns ?? ClientBuilder.Default());
        return this;
    }

    public ClientRepositoryStub CreateFails(string message = "DB Error")
    {
        Mock.Setup(r => r.CreateAsync(It.IsAny<Client>()))
            .ThrowsAsync(new Exception(message));
        return this;
    }


    public ClientRepositoryStub GetAllReturns(IEnumerable<Client> clients, long total)
    {
        Mock.Setup(r => r.GetAllAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string?>(),
                It.IsAny<string?>()))
            .ReturnsAsync((clients, total));
        return this;
    }

    public ClientRepositoryStub GetAllWithParamsReturns(
        int page,
        int pageSize,
        string? sort,
        string? order,
        IEnumerable<Client> clients,
        long total)
    {
        Mock.Setup(r => r.GetAllAsync(page, pageSize, sort, order))
            .ReturnsAsync((clients, total));
        return this;
    }

    public ClientRepositoryStub GetAllReturnsEmpty()
    {
        Mock.Setup(r => r.GetAllAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string?>(),
                It.IsAny<string?>()))
            .ReturnsAsync((Enumerable.Empty<Client>(), 0L));
        return this;
    }


    public void VerifyCreateCalledOnce() =>
        Mock.Verify(r => r.CreateAsync(It.IsAny<Client>()), Times.Once);

    public void VerifyCreateNeverCalled() =>
        Mock.Verify(r => r.CreateAsync(It.IsAny<Client>()), Times.Never);

    public void VerifyGetByIdCalledOnceWith(string id) =>
        Mock.Verify(r => r.GetByIdAsync(id), Times.Once);

    public void VerifyGetAllCalledWith(int page, int pageSize, string? sort, string? order) =>
        Mock.Verify(r => r.GetAllAsync(page, pageSize, sort, order), Times.Once);
}