namespace Application.Features.Invoices.GetByClientInvoice.Queries;

using Application.Features.Invoices.GetByClientInvoice.Dtos;
using Core.Dtos.ResponsesDto;

public class GetInvoicesByClientQuery : ParamDto, MediatR.IRequest<ResultDto<IEnumerable<InvoicByClientResponseDto>>>;