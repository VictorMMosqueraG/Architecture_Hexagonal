namespace Core.Interfaces.Repositories;

using Core.Entities;
public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(string id);
    Task<Client?> GetByEmailAsync(string email);
    Task<Client> CreateAsync(Client client);
}