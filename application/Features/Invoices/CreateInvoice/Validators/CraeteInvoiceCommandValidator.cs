namespace Application.Features.Invoices.CreateInvoice.Validator;

using Application.Features.Invoices.CreateInvoice.Command;
using FluentValidation;
using System;

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
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

    private bool BeAFutureDate(DateTime date)
    {
        return date.Date >= DateTime.UtcNow.Date;
    }
}