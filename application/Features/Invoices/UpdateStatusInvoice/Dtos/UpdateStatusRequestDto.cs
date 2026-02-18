namespace Application.Features.Invoices.UpdateStatusInvoice.Dtos;

public class UpdateStatusRequestDto: UpdateStatusParamDto
{
    public required string NewStatus {get;set;}
}