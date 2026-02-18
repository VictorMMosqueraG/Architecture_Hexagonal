namespace Application.Features.Invoices.UpdateStatusInvoice.Validators;

using Application.Features.Invoices.UpdateStatusInvoice.Command;
using Core.Constants;
using FluentValidation;
using System.Linq;

public class UpdateInvoiceStatusCommandValidator : AbstractValidator<UpdateInvoiceStatusCommand>
{
    public UpdateInvoiceStatusCommandValidator()
    {
        RuleFor(x => x.NewStatus)
            .NotEmpty().WithMessage("El nuevo estado es requerido.")
            .Must(BeAValidStatus).WithMessage(x => 
                $"El estado '{x.NewStatus}' no es válido. " +
                $"Valores permitidos: {GetAllowedStatuses()}");
    }

    private bool BeAValidStatus(string status)
    {
        var allowedStatuses = new[]
        {
            InvoiceStatus.Pending,
            InvoiceStatus.FirstReminder,
            InvoiceStatus.SecondReminder,
            InvoiceStatus.Disabled,
            InvoiceStatus.Paid
        };

        return allowedStatuses.Contains(status);
    }

    private string GetAllowedStatuses()
    {
        return string.Join(", ", 
            InvoiceStatus.Pending, 
            InvoiceStatus.FirstReminder, 
            InvoiceStatus.SecondReminder, 
            InvoiceStatus.Disabled, 
            InvoiceStatus.Paid);
    }
}