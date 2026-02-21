namespace Application.Features.Invoices.CreateInvoice.Validator;

using Application.Features.Invoices.CreateInvoice.Command;
using FluentValidation;

/// <summary>
/// Validador de FluentValidation para <see cref="CreateInvoiceCommand"/>.
/// Se ejecuta automáticamente a través del <c>ValidationBehaviour</c> del pipeline de MediatR.
/// </summary>
public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    /// <summary>Define las reglas de validación para cada campo del comando.</summary>
    public CreateInvoiceCommandValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("El ID del cliente es requerido.")
            .Matches("^[0-9a-fA-F]{24}$").WithMessage("El formato del ID del cliente no es válido (UUID).");

        RuleFor(x => x.InvoiceNumber)
            .NotEmpty().WithMessage("El número de factura es requerido.")
            .Matches(@"^INV-[0-9]{4}-[0-9]{4}$")
            .WithMessage("El formato de factura debe ser INV-YYYY-NNNN (ej: INV-2026-0001).");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El monto de la factura debe ser mayor a cero.");

        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("La fecha de vencimiento es requerida.")
            .Must(BeAFutureDate).WithMessage("La fecha de vencimiento no puede ser una fecha pasada.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");
    }

    /// <summary>Verifica que la fecha no sea anterior a la fecha actual UTC.</summary>
    /// <param name="date">Fecha a validar.</param>
    /// <returns><c>true</c> si la fecha es hoy o futura; <c>false</c> si es pasada.</returns>
    private bool BeAFutureDate(DateTime date)
    {
        return date.Date >= DateTime.UtcNow.Date;
    }
}