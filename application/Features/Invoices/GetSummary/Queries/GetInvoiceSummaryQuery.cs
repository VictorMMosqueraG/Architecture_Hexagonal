namespace Application.Features.Invoices.GetSummary.Queries;

using Application.Features.Invoices.GetSummary.Dtos;
using Core.Dtos.PaginationsDto;
using Core.Dtos.ResponsesDto;
using MediatR;

public class GetInvoicesSummaryQuery : PaginationDto, IRequest<ResultDto<InvoiceSummaryDto>>;