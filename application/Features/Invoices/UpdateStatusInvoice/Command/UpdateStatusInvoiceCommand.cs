namespace Application.Features.Invoices.UpdateStatusInvoice.Command;

using Application.Features.Invoices.UpdateStatusInvoice.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;

/// <summary>
/// Comando para actualizar el estado de una factura existente.
/// Hereda los campos de <see cref="UpdateStatusRequestDto"/> y actúa como request de MediatR.
/// </summary>
public class UpdateInvoiceStatusCommand : UpdateStatusRequestDto, IRequest<ResultDto<bool>>;