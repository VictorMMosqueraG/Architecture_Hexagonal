namespace Application.Features.Clients.GetAllClient.Queries;

using Application.Features.Clients.GetAllClient.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Interfaces.Repositories;
using Core.Messages;
using MediatR;

/// <summary>
/// Handler que procesa <see cref="GetAllClientQuery"/>.
/// Recupera los clientes paginados desde el repositorio y los proyecta a <see cref="ClientResponseDto"/>.
/// </summary>
public class GetAllClientQueryHandler(
    IClientRepository clientRepository
) : IRequestHandler<GetAllClientQuery, PaginatedResultDto<IEnumerable<ClientResponseDto>>>
{
    private readonly IClientRepository _clientRepository = clientRepository;

    /// <summary>
    /// Obtiene la lista paginada de clientes.
    /// </summary>
    /// <param name="request">Query con los parámetros de paginación y ordenamiento.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Resultado paginado con la colección de clientes y metadata de paginación.</returns>
    public async Task<PaginatedResultDto<IEnumerable<ClientResponseDto>>> Handle(
        GetAllClientQuery request,
        CancellationToken cancellationToken
    ){
        var (clients, total) = await _clientRepository.GetAllAsync(
            request.Page,
            request.PageSize,
            request.Sort,
            request.Order
        );

        var foundClients = clients.Select(c => new ClientResponseDto(
            c.Id,
            c.Name,
            c.Email,
            c.DocumentNumber,
            c.Phone,
            c.Status,
            c.CreatedAt
        ));

        var response = PaginatedResultDto<IEnumerable<ClientResponseDto>>
            .Success((int)total, request.Page, request.PageSize, foundClients);
        response.Message = Message.GetAllData;

        return response;
    }
}