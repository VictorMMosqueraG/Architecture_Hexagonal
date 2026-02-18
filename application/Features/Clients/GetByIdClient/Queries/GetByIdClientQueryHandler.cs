namespace Application.Features.Clients.GetByIdClient.Queries;

using Application.Features.Clients.GetByIdClient.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Messages;
using MediatR;

public class GetClientByIdQueryHandler(
    IClientRepository clientRepository
) : IRequestHandler<GetByIdClientQuery, ResultDto<ClientByIdResponseDto>>
{
    private readonly IClientRepository _clientRepository = clientRepository;

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