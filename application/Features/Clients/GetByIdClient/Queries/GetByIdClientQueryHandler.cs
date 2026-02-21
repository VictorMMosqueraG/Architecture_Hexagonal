namespace Application.Features.Clients.GetByIdClient.Queries;

using Application.Features.Clients.GetByIdClient.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Messages;
using MediatR;

/// <summary>
/// Handler que procesa <see cref="GetByIdClientQuery"/>.
/// Busca el cliente por ID y lanza <see cref="NotFoundException"/> si no existe.
/// </summary>
public class GetClientByIdQueryHandler(
    IClientRepository clientRepository
) : IRequestHandler<GetByIdClientQuery, ResultDto<ClientByIdResponseDto>>
{
    private readonly IClientRepository _clientRepository = clientRepository;

    /// <summary>
    /// Obtiene un cliente por su identificador único.
    /// </summary>
    /// <param name="request">Query con el ID del cliente a buscar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Resultado con los datos del cliente encontrado.</returns>
    /// <exception cref="NotFoundException">Se lanza si no existe un cliente con el ID proporcionado.</exception>
    public async Task<ResultDto<ClientByIdResponseDto>> Handle(
        GetByIdClientQuery request,
        CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException($"Cliente {request.Id} no encontrado");

        var found = new ClientByIdResponseDto(
            client.Id,
            client.Name,
            client.Email,
            client.DocumentNumber,
            client.Phone,
            client.Status,
            client.CreatedAt
        );

        var response = ResultDto<ClientByIdResponseDto>.Success(found);
        response.Message = Message.GetAllData;

        return response;
    }
}