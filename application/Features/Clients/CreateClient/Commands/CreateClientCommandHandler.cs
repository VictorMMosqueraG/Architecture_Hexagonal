namespace Application.Features.Clients.CreateClient.Command;

using AutoMapper;
using Core.Dtos.ResponsesDto;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Messages;
using MediatR;
public class CreateClientCommandHandler(
    IClientRepository clientRepository,
    IMapper mapper
) : IRequestHandler<CreateClientCommand, ResultDto<Client>>
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IMapper _mapper = mapper;
    private const string Entity = "Cliente";
    private const string Email = "Email";

    public async Task<ResultDto<Client>> Handle(
        CreateClientCommand request, 
        CancellationToken cancellationToken
    ){
        var existing = await _clientRepository.GetByEmailAsync(request.Email);

         if (existing is not null)
            throw new ConflictException(Message.AlreadyExist(Entity,Email));

        var client = _mapper.Map<Client>(request);
        var created = await _clientRepository.CreateAsync(client);

        var response = ResultDto<Client>.Success(created);
        response.Message = Message.EntityCreateSuccess(Entity);

        return response;
    }
}