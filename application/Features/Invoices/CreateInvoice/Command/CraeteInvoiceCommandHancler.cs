namespace Application.Features.Invoices.CreateInvoice.Command;

using MediatR;
using Core.Dtos.ResponsesDto;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Exceptions;
using Core.Messages;
using AutoMapper;

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

    private async Task ValidCustomer(string clientId)
    {
        var foundClient = await _clientRepository.GetByIdAsync(clientId);

        if(foundClient==null)
            throw new NotFoundException(Message.NotFoundEntity(Client,clientId));
    }

    private async Task ValidInvoiceNumner(string invoiceNumber)
    {
        var validInvoiceNumber = await _invoiceRepository.GetByInvoiceNumberAsync(invoiceNumber);
        
        if(validInvoiceNumber!=null)
            throw new ConflictException(Message.AlreadyExist(Entity,invoiceNumber));
    }
}