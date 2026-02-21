namespace Application.Features.Invoices.UpdateStatusInvoice.Command;

using Core.Dtos.ResponsesDto;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Messages;
using MediatR;

/// <summary>
/// Handler que procesa <see cref="UpdateInvoiceStatusCommand"/>.
/// Verifica que la factura exista y actualiza su estado en la base de datos.
/// </summary>
public class UpdateInvoiceStatusCommandHandler(
    IInvoiceRepository invoiceRepository
) : IRequestHandler<UpdateInvoiceStatusCommand, ResultDto<bool>>
{
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

    /// <summary>
    /// Ejecuta la actualización del estado de la factura.
    /// </summary>
    /// <param name="request">Comando con el ID de la factura y el nuevo estado.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Resultado con <c>true</c> si la actualización fue exitosa.</returns>
    /// <exception cref="NotFoundException">Se lanza si no existe una factura con el ID proporcionado.</exception>
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