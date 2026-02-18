namespace Application.Features.Clients.GetAllClient.Queries;

using Application.Features.Clients.GetAllClient.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Interfaces.Repositories;
using Core.Messages;
using MediatR;

public class GetAllClientQueryHandler(
    IClientRepository clientRepository
) : IRequestHandler<GetAllClientQuery, ResultDto<IEnumerable<ClientResponseDto>>>
{
    private readonly IClientRepository _clientRepository = clientRepository;


    public async Task<ResultDto<IEnumerable<ClientResponseDto>>> Handle(
        GetAllClientQuery request, 
        CancellationToken cancellationToken
    ){
        var clients = await _clientRepository.GetAllAsync();


        var foundClients =clients.Select(c => new ClientResponseDto(
            c.Id,
            c.Name,
            c.Email,
            c.DocumentNumber,
            c.Phone,
            c.Status,
            c.CreatedAt
        ));

        var response = ResultDto<IEnumerable<ClientResponseDto>>.Success(foundClients);
        response.Message = Message.GetAllData;

        return response;
    }
}