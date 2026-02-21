namespace Application.Features.Clients.CreateClient.Command;

using Application.Features.Clients.CreateClient.Dtos;
using Core.Dtos.ResponsesDto;
using Core.Entities;
using MediatR;

/// <summary>
/// Comando para crear un nuevo cliente.
/// Hereda los campos de <see cref="CreateClientRequestDto"/> y actúa como request de MediatR.
/// </summary>
public class CreateClientCommand : CreateClientRequestDto, IRequest<ResultDto<Client>>;