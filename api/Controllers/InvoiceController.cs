namespace Api.Features.Invoices;

using Application.Features.Invoices.CreateInvoice.Command;
using Application.Features.Invoices.GetAllInvoice.Dtos;
using Application.Features.Invoices.GetAllInvoice.Queries;
using Core.Dtos.ResponsesDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/invoices")]
public class InvoicesController(
    IMediator mediator
) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>Crear factura</summary>
    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
     => Created(nameof(CreateInvoice), await _mediator.Send(command));

    /// <summary>
    /// Listar todos los invoices
    /// </summary>
    [HttpGet]
    public async Task<PaginatedResultDto<IEnumerable<InvoiceResponseDto>>> GetAll([FromQuery] GetAllInvoiceQuery query)
        => await _mediator.Send(query);
    
}