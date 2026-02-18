using Application.Features.Clients.GetAllClient.Dtos;
using Application.Features.Clients.GetAllClient.Queries;
using Core.Dtos.ResponsesDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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
    public async Task<ResultDto<IEnumerable<ClientResponseDto>>> GetAll() 
        => await _mediator.Send(new GetAllClientQuery());
}
