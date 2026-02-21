namespace Application.Features.Invoices.CreateInvoice.Command;

using MediatR;
using Core.Dtos.ResponsesDto;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Exceptions;
using Core.Messages;
using AutoMapper;

/// <summary>
/// Handler que procesa <see cref="CreateInvoiceCommand"/>.
/// Valida que el cliente exista y que el número de factura no esté duplicado antes de persistir.
/// </summary>
public class CreateInvoiceCommandHandler(
    IInvoiceRepository invoiceRepository,
    IClientRepository clientRepository,
    IMapper mapper
) : IRequestHandler<CreateInvoiceCommand, ResultDto<Invoice>>
{
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
    private readonly IClientRepository  _clientRepository  = clientRepository;
    private readonly IMapper _mapper = mapper;

    private const string Entity = "Invoice";
    private const string Client = "Cliente";

    /// <summary>
    /// Ejecuta la lógica de creación de la factura.
    /// </summary>
    /// <param name="request">Comando con los datos de la factura a crear.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Resultado con la factura creada y un mensaje de éxito.</returns>
    /// <exception cref="NotFoundException">Se lanza si el cliente con el ID proporcionado no existe.</exception>
    /// <exception cref="ConflictException">Se lanza si ya existe una factura con el mismo número.</exception>
    public async Task<ResultDto<Invoice>> Handle(
        CreateInvoiceCommand request,
        CancellationToken cancellationToken
    ){
        await ValidCustomer(request.ClientId);
        await ValidInvoiceNumner(request.InvoiceNumber);

        var invoice = _mapper.Map<Invoice>(request);
        var created = await _invoiceRepository.CreateAsync(invoice);

        var response = ResultDto<Invoice>.Success(created);
        response.Message = Message.EntityCreateSuccess(Entity);

        return response;
    }

    /// <summary>Verifica que el cliente exista en la base de datos.</summary>
    /// <exception cref="NotFoundException">Si el cliente no existe.</exception>
    private async Task ValidCustomer(string clientId)
    {
        var foundClient = await _clientRepository.GetByIdAsync(clientId);

        if (foundClient == null)
            throw new NotFoundException(Message.NotFoundEntity(Client, clientId));
    }

    /// <summary>Verifica que el número de factura no esté ya registrado.</summary>
    /// <exception cref="ConflictException">Si el número de factura ya existe.</exception>
    private async Task ValidInvoiceNumner(string invoiceNumber)
    {
        var validInvoiceNumber = await _invoiceRepository.GetByInvoiceNumberAsync(invoiceNumber);

        if (validInvoiceNumber != null)
            throw new ConflictException(Message.AlreadyExist(Entity, invoiceNumber));
    }
}