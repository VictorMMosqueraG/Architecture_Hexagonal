namespace Core.Interfaces.Repositories;

using Core.Entities;
public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();

    Task<Client> CreateAsync(Client client);
}