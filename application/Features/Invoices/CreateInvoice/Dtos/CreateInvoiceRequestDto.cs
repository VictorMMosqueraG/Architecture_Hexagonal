namespace Application.Features.Invoices.CreateInvoice.Dtos;

public class CreateInvoiceRequestDto
{
    public required string ClientId {get;set;}
    public required string InvoiceNumber {get;set;}
    public decimal Amount {get;set;}
    public DateTime DueDate {get;set;}
    public string? Description {get;set;}
}