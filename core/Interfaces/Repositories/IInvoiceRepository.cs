namespace Core.Interfaces.Repositories;

using Core.Entities;
public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetAllAsync();
    Task<IEnumerable<Invoice>> GetByClientIdAsync(string clientId);
    Task<IEnumerable<Invoice>> GetByStatusAsync(string status);
    Task<Invoice?> GetByIdAsync(string id);
    Task<Invoice> CreateAsync(Invoice invoice);
    Task UpdateStatusAsync(string invoiceId, string newStatus);
    Task<Invoice?> GetByInvoiceNumberAsync(string invoiceNumber); 
}
