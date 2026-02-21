namespace Application.Features.Reminders.ProcessReminder.Dtos;

/// <summary>Resumen del resultado del proceso de envío de recordatorios.</summary>
/// <param name="TotalProcessed">Total de facturas procesadas exitosamente.</param>
/// <param name="UpgradedToSecondReminder">Facturas que pasaron de primer a segundo recordatorio.</param>
/// <param name="UpgradedToDisabled">Facturas que fueron deshabilitadas tras el segundo recordatorio.</param>
/// <param name="Errors">Lista de errores ocurridos durante el proceso.</param>
public record ProcessRemindersResponseDto(
    int TotalProcessed,
    int UpgradedToSecondReminder,
    int UpgradedToDisabled,
    List<string> Errors
);