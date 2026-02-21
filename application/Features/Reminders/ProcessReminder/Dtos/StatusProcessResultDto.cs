namespace Application.Features.Reminders.ProcessReminder.Dtos;

/// <summary>Resultado interno del procesamiento de facturas agrupadas por estado.</summary>
/// <param name="Processed">Cantidad de facturas procesadas exitosamente.</param>
/// <param name="Errors">Lista de mensajes de error de las facturas que fallaron.</param>
internal record StatusProcessResult(
    int Processed,
    List<string> Errors
);