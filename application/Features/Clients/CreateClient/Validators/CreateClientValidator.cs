namespace Application.Features.Clients.CreateClient.Validator;

using Application.Features.Clients.CreateClient.Command;
using FluentValidation;

/// <summary>
/// Validador de FluentValidation para <see cref="CreateClientCommand"/>.
/// Se ejecuta automáticamente a través del <c>ValidationBehaviour</c> del pipeline de MediatR.
/// </summary>
public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    /// <summary>
    /// Define las reglas de validación para cada campo del comando.
    /// </summary>
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del cliente es requerido.")
            .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es requerido.")
            .EmailAddress().WithMessage("El formato del correo electrónico no es válido.")
            .MaximumLength(100).WithMessage("El correo no puede exceder los 100 caracteres.");

        RuleFor(x => x.DocumentNumber)
            .NotEmpty().WithMessage("El número de documento es requerido.")
            .MinimumLength(5).WithMessage("El documento debe tener al menos 5 caracteres.")
            .Matches(@"^[a-zA-Z0-9]*$").WithMessage("El documento solo puede contener letras y números.");

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("El teléfono no puede exceder los 20 caracteres.")
            .Matches(@"^\+?[0-9]*$").WithMessage("El formato del teléfono no es válido.")
            .When(x => !string.IsNullOrEmpty(x.Phone));
    }
}