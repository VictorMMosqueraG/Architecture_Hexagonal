namespace Application.Features.Invoices.UpdateStatusInvoice.Command;

using Core.Dtos.ResponsesDto;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Messages;
using MediatR;

public class UpdateInvoiceStatusCommandHandler(
    IInvoiceRepository invoiceRepository
) : IRequestHandler<UpdateInvoiceStatusCommand, ResultDto<bool>>
{
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

    public async Task<ResultDto<bool>> Handle(
        UpdateInvoiceStatusCommand request,
        CancellationToken cancellationToken)
    {
        _ = await _invoiceRepository.GetByIdAsync(request.IdInvoice)
            ?? throw new NotFoundException($"Factura {request.IdInvoice} no encontrada");

        await _invoiceRepository.UpdateStatusAsync(request.IdInvoice, request.NewStatus);

        var response = ResultDto<bool>.Success(true);
        response.Message = Message.GetAllData;

        return response;
    }
}