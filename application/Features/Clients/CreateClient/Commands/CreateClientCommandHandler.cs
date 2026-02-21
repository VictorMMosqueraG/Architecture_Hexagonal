namespace Application.Features.Clients.CreateClient.Command;

using AutoMapper;
using Core.Dtos.ResponsesDto;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Messages;
using MediatR;

/// <summary>
/// Handler que procesa el comando <see cref="CreateClientCommand"/>.
/// Valida que no exista un cliente con el mismo email y lo persiste en la base de datos.
/// </summary>
public class CreateClientCommandHandler(
    IClientRepository clientRepository,
    IMapper mapper
) : IRequestHandler<CreateClientCommand, ResultDto<Client>>
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IMapper _mapper = mapper;
    private const string Entity = "Cliente";
    private const string Email = "Email";

    /// <summary>
    /// Ejecuta la lógica de creación del cliente.
    /// </summary>
    /// <param name="request">Comando con los datos del cliente a crear.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Resultado con el cliente creado y un mensaje de éxito.</returns>
    /// <exception cref="ConflictException">
    /// Se lanza si ya existe un cliente registrado con el mismo email.
    /// </exception>
    public async Task<ResultDto<Client>> Handle(
        CreateClientCommand request,
        CancellationToken cancellationToken
    ){
        var existing = await _clientRepository.GetByEmailAsync(request.Email);

        if (existing is not null)
            throw new ConflictException(Message.AlreadyExist(Entity, Email));

        var client = _mapper.Map<Client>(request);
        var created = await _clientRepository.CreateAsync(client);

        var response = ResultDto<Client>.Success(created);
        response.Message = Message.EntityCreateSuccess(Entity);

        return response;
    }
}