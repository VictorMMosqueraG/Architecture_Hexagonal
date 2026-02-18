namespace Core.Interfaces.Repositories;

using Core.Entities;
public interface IReminderLogRepository
{
    Task CreateAsync(ReminderLog log);
}