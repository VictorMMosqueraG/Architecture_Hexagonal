namespace Api.Features.Invoices;

using Application.Features.Invoices.CreateInvoice.Command;
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
    
}