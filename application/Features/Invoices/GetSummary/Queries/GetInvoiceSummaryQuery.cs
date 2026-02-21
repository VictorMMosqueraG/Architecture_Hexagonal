namespace Application.Features.Invoices.GetSummary.Queries;

using Application.Features.Invoices.GetSummary.Dtos;
using Core.Dtos.PaginationsDto;
using Core.Dtos.ResponsesDto;
using MediatR;

/// <summary>
/// Query para obtener el resumen consolidado de facturas agrupado por estado y por cliente.
/// Hereda los parámetros de <see cref="PaginationDto"/>.
/// </summary>
public class GetInvoicesSummaryQuery : PaginationDto, IRequest<ResultDto<InvoiceSummaryDto>>;