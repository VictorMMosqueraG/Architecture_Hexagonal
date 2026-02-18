namespace Core.Interfaces.Repositories;

using Core.Entities;
public interface IClientRepository
{
    Task<(IEnumerable<Client> Data, long Total)> GetAllAsync(
        int page, 
        int pageSize, 
        string? sort, 
        string? order);
    Task<Client?> GetByIdAsync(string id);
    Task<Client?> GetByEmailAsync(string email);
    Task<Client> CreateAsync(Client client);
}