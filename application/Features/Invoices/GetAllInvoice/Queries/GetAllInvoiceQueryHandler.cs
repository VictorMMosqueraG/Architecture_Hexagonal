namespace Application.Features.Invoices.GetAllInvoice.Queries;

using Application.Features.Invoices.GetAllInvoice.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Interfaces.Repositories;
using Core.Messages;
using MediatR;

/// <summary>
/// Handler que procesa <see cref="GetAllInvoiceQuery"/>.
/// Recupera las facturas paginadas desde el repositorio y las proyecta a <see cref="InvoiceResponseDto"/>.
/// </summary>
public class GetAllInvoicesQueryHandler(
    IInvoiceRepository invoiceRepository
) : IRequestHandler<GetAllInvoiceQuery, PaginatedResultDto<IEnumerable<InvoiceResponseDto>>>
{
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

    /// <summary>
    /// Obtiene la lista paginada de facturas.
    /// </summary>
    /// <param name="request">Query con los parámetros de paginación y ordenamiento.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Resultado paginado con la colección de facturas y metadata de paginación.</returns>
    public async Task<PaginatedResultDto<IEnumerable<InvoiceResponseDto>>> Handle(
        GetAllInvoiceQuery request,
        CancellationToken cancellationToken)
    {
        var (invoices, total) = await _invoiceRepository.GetAllAsync(
            request.Page,
            request.PageSize,
            request.Sort,
            request.Order
        );

        var foundInvoice = invoices.Select(i => new InvoiceResponseDto(
            i.Id,
            i.ClientId,
            i.InvoiceNumber,
            i.Amount,
            i.DueDate,
            i.Status,
            i.Description,
            i.CreatedAt,
            i.UpdatedAt
        ));

        var response = PaginatedResultDto<IEnumerable<InvoiceResponseDto>>
            .Success((int)total, request.Page, request.PageSize, foundInvoice);
        response.Message = Message.GetAllData;

        return response;
    }
}