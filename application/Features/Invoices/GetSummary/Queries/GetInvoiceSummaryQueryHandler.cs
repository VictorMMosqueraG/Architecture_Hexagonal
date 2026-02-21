namespace Application.Features.Invoices.GetSummary.Queries;

using Application.Features.Invoices.GetSummary.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Messages;
using MediatR;

/// <summary>
/// Handler que procesa <see cref="GetInvoicesSummaryQuery"/>.
/// Carga todas las facturas y construye un resumen agrupado por estado y por cliente.
/// </summary>
public class GetInvoicesSummaryQueryHandler(
    IInvoiceRepository invoiceRepository
) : IRequestHandler<GetInvoicesSummaryQuery, ResultDto<InvoiceSummaryDto>>
{
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

    /// <summary>
    /// Obtiene el resumen consolidado de todas las facturas.
    /// </summary>
    /// <param name="request">Query con parámetros heredados de paginación (no aplicados en el resumen).</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Resultado con el resumen de facturas agrupado por estado y cliente.</returns>
    public async Task<ResultDto<InvoiceSummaryDto>> Handle(
        GetInvoicesSummaryQuery request,
        CancellationToken cancellationToken)
    {
        var (invoicesData, _) = await _invoiceRepository.GetAllAsync(
            page:     1,
            pageSize: int.MaxValue,
            sort:     null,
            order:    null
        );

        var invoices = invoicesData.ToList();
        var summary = await BuildData(invoices);

        var response = ResultDto<InvoiceSummaryDto>.Success(summary);
        response.Message = Message.GetAllData;

        return response;
    }

    /// <summary>
    /// Construye el resumen agrupando las facturas por estado y por cliente.
    /// </summary>
    /// <param name="invoicesData">Lista completa de facturas a procesar.</param>
    /// <returns>DTO con el resumen consolidado.</returns>
    private async Task<InvoiceSummaryDto> BuildData(List<Invoice> invoicesData)
    {
        var invoices = invoicesData.ToList();

        var byStatus = invoices
            .GroupBy(i => i.Status)
            .Select(g => new StatusGroupDto(g.Key, g.Count(), g.Sum(i => i.Amount)));

        var byClient = invoices
            .GroupBy(i => i.ClientId)
            .Select(g => new ClientGroupDto(g.Key, g.Count(), g.Sum(i => i.Amount)));

        return new InvoiceSummaryDto(
            invoices.Count,
            invoices.Sum(i => i.Amount),
            byStatus,
            byClient
        );
    }
}