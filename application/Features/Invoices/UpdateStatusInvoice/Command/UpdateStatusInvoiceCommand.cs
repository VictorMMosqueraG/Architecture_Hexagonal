namespace Application.Features.Invoices.UpdateStatusInvoice.Command;

using Application.Features.Invoices.UpdateStatusInvoice.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;
public class UpdateInvoiceStatusCommand: UpdateStatusRequestDto, IRequest<ResultDto<bool>>;