namespace Application.Features.Invoices.CreateInvoice.Command;

using Application.Features.Invoices.CreateInvoice.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Entities;
using MediatR;

/// <summary>
/// Comando para crear una nueva factura.
/// Hereda los campos de <see cref="CreateInvoiceRequestDto"/> y actúa como request de MediatR.
/// </summary>
public class CreateInvoiceCommand : CreateInvoiceRequestDto, IRequest<ResultDto<Invoice>>;