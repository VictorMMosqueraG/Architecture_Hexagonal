namespace Application.Features.Reminders.ProcessReminder.Dtos;

/// <summary>Resultado interno del procesamiento de una factura individual.</summary>
/// <param name="Success">Indica si el procesamiento fue exitoso.</param>
/// <param name="ErrorMessage">Mensaje de error en caso de fallo; <c>null</c> si fue exitoso.</param>
internal record InvoiceProcessResult(
    bool    Success,
    string? ErrorMessage
);