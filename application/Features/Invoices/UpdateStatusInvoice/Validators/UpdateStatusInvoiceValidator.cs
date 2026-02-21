namespace Application.Features.Invoices.UpdateStatusInvoice.Validators;

using Application.Features.Invoices.UpdateStatusInvoice.Command;
using Core.Constants;
using FluentValidation;

/// <summary>
/// Validador de FluentValidation para <see cref="UpdateInvoiceStatusCommand"/>.
/// Verifica que el nuevo estado sea uno de los valores permitidos por <see cref="InvoiceStatus"/>.
/// </summary>
public class UpdateInvoiceStatusCommandValidator : AbstractValidator<UpdateInvoiceStatusCommand>
{
    /// <summary>Define las reglas de validación para el campo <c>NewStatus</c>.</summary>
    public UpdateInvoiceStatusCommandValidator()
    {
        RuleFor(x => x.NewStatus)
            .NotEmpty().WithMessage("El nuevo estado es requerido.")
            .Must(BeAValidStatus).WithMessage(x =>
                $"El estado '{x.NewStatus}' no es válido. " +
                $"Valores permitidos: {GetAllowedStatuses()}");
    }

    /// <summary>Verifica que el estado sea uno de los valores definidos en <see cref="InvoiceStatus"/>.</summary>
    /// <param name="status">Estado a validar.</param>
    /// <returns><c>true</c> si el estado es válido; <c>false</c> en caso contrario.</returns>
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

    /// <summary>Retorna los estados permitidos como cadena separada por comas.</summary>
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