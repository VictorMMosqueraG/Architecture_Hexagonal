namespace Application.Features.Invoices.CreateInvoice.Command;

using Application.Features.Invoices.CreateInvoice.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Entities;
using MediatR;

public class CreateInvoiceCommand : CreateInvoiceRequestDto, IRequest<ResultDto<Invoice>>;