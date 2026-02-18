namespace Application.Features.Clients.CreateClient.Command;

using Application.Features.Clients.CreateClient.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Entities;
using MediatR;

public class CreateClientCommand : CreateClientRequestDto, IRequest<ResultDto<Client>>;