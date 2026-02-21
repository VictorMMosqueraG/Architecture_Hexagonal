namespace Application.Features.Invoices.GetAllInvoice.Queries;

using Application.Features.Invoices.GetAllInvoice.Dtos;
using Core.Dtos.PaginationsDto;
using Core.Dtos.ResponsesDto;
using MediatR;

/// <summary>
/// Query para obtener todas las facturas con soporte de paginación, ordenamiento y dirección.
/// Hereda los parámetros de <see cref="PaginationDto"/>.
/// </summary>
public class GetAllInvoiceQuery : PaginationDto, IRequest<PaginatedResultDto<IEnumerable<InvoiceResponseDto>>>;