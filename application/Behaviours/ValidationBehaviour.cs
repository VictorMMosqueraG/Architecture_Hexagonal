namespace Application.Behaviours
{
    using CoreException = Core.Exceptions;
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;

    /// <summary>
    /// Pipeline behavior que ejecuta las validaciones de FluentValidation
    /// antes de que el request llegue al handler.
    /// </summary>
    /// <typeparam name="TRequest">Tipo del request MediatR.</typeparam>
    /// <typeparam name="TResponse">Tipo de la respuesta del handler.</typeparam>
    public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Ejecuta todos los validadores registrados para el request.
        /// Si hay errores de validación, lanza una <see cref="CoreException.ValidationException"/>
        /// antes de continuar con el siguiente behavior o handler.
        /// </summary>
        /// <param name="request">El request a validar.</param>
        /// <param name="next">Delegado que invoca el siguiente paso del pipeline.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>La respuesta del handler si la validación es exitosa.</returns>
        /// <exception cref="CoreException.ValidationException">
        /// Se lanza cuando uno o más validadores encuentran errores.
        /// </exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                ValidationContext<TRequest> context = new(request);
                ValidationResult[] validationResults = await Task.WhenAll(
                    validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));
                List<ValidationFailure> failures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failures.Any())
                {
                    var errors = failures
                        .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                        .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

                    throw new CoreException.ValidationException("One or more validation errors have occurred.", errors);
                }
            }

            return await next();
        }
    }
}