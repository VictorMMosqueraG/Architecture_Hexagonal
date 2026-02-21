namespace Application.Features.Invoices.GetByClientInvoice.Queries;

using System.Collections;
using Application.Features.Invoices.GetByClientInvoice.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Messages;
using MediatR;

/// <summary>
/// Handler que procesa <see cref="GetInvoicesByClientQuery"/>.
/// Valida que el cliente exista y retorna todas sus facturas.
/// </summary>
public class GetInvoicesByClientQueryHandler(
    IInvoiceRepository invoiceRepository,
    IClientRepository clientRepository
) : IRequestHandler<GetInvoicesByClientQuery, ResultDto<IEnumerable<InvoicByClientResponseDto>>>
{
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
    private readonly IClientRepository  _clientRepository  = clientRepository;
    private static string EntityClient = "Client";

    /// <summary>
    /// Obtiene todas las facturas de un cliente por su ID.
    /// </summary>
    /// <param name="request">Query con el ID del cliente a consultar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Resultado con la colección de facturas del cliente.</returns>
    /// <exception cref="NotFoundException">Se lanza si el cliente con el ID proporcionado no existe.</exception>
    public async Task<ResultDto<IEnumerable<InvoicByClientResponseDto>>> Handle(
        GetInvoicesByClientQuery request,
        CancellationToken cancellationToken)
    {
        var foundClient = await _clientRepository.GetByIdAsync(request.ClientId);

        if (foundClient == null)
            throw new NotFoundException(Message.NotFoundEntity(EntityClient, request.ClientId));

        var invoices = await _invoiceRepository.GetByClientIdAsync(request.ClientId);

        var foundData = invoices.Select(i => new InvoicByClientResponseDto(
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

        var response = ResultDto<IEnumerable<InvoicByClientResponseDto>>.Success(foundData);
        response.Message = Message.GetAllData;

        return response;
    }
}