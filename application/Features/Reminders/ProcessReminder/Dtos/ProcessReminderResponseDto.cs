namespace Application.Features.Reminders.ProcessReminder.Dtos;

public record ProcessRemindersResponseDto(
    int TotalProcessed,
    int UpgradedToSecondReminder,
    int UpgradedToDisabled,
    List<string> Errors
);