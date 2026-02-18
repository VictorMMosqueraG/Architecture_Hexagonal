namespace Api.Controllers;

using System.Net.Quic;
using Api.Attributes;
using Application.Features.Clients.CreateClient.Command;
using Application.Features.Clients.GetAllClient.Dtos;
using Application.Features.Clients.GetAllClient.Queries;
using Application.Features.Clients.GetByIdClient.Dtos;
using Application.Features.Clients.GetByIdClient.Queries;
using Core.Dtos.ResponsesDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/clients")]
public class ClientsController(
    IMediator mediator
) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    /// <summary>
    /// Listar todos los clientes
    /// </summary>
    [HttpGet]
    public async Task<PaginatedResultDto<IEnumerable<ClientResponseDto>>> GetAll([FromQuery] GetAllClientQuery query)
        => await _mediator.Send(query);


    /// <summary>
    /// Crea un nuevo cliente
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientCommand command)
     => Created(nameof(CreateClient), await _mediator.Send(command));

    /// <summary>
    /// Busca un cliente por id
    /// </summary>
    [HttpGet("{id}")]
    [RouteParameterMapping("id", "Id")]
    public async Task<ResultDto<ClientByIdResponseDto>> GetById([FromRoute] GetByIdClientQuery query )
        => await _mediator.Send(query);
}
