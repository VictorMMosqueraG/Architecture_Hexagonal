namespace Api.Features.Invoices;

using Application.Features.Invoices.CreateInvoice.Command;
using Application.Features.Invoices.GetAllInvoice.Dtos;
using Application.Features.Invoices.GetAllInvoice.Queries;
using Application.Features.Invoices.GetSummary.Dtos;
using Application.Features.Invoices.GetSummary.Queries;
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


    /// <summary>
    /// Listar todas los invoices,
    /// a forma de resumen
    /// </summary
    [HttpGet("summary")]
    public async Task<ResultDto<InvoiceSummaryDto>> GetSummary()
        => await _mediator.Send(new GetInvoicesSummaryQuery());
    
}