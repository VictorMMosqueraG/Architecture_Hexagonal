namespace Application.Features.Invoices.GetByClientInvoice.Queries;

using Application.Features.Invoices.GetByClientInvoice.Dtos;
using Core.Dtos.ResponsesDto;

/// <summary>
/// Query para obtener todas las facturas asociadas a un cliente específico.
/// Hereda el parámetro <c>ClientId</c> de <see cref="ParamDto"/>.
/// </summary>
public class GetInvoicesByClientQuery : ParamDto, MediatR.IRequest<ResultDto<IEnumerable<InvoicByClientResponseDto>>>;