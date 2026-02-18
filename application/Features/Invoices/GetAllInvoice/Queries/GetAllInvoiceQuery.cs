namespace Application.Features.Invoices.GetAllInvoice.Queries;

using Application.Features.Invoices.GetAllInvoice.Dtos;
using Core.Dtos.PaginationsDto;
using Core.Dtos.ResponsesDto;
using MediatR;
public class GetAllInvoiceQuery : PaginationDto, IRequest<PaginatedResultDto<IEnumerable<InvoiceResponseDto>>>;