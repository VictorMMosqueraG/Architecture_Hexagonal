namespace Infrastructure.Persistence.MongoDB.Reminder;

using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Persistence.MongoDB.Base;
using Microsoft.Extensions.Configuration;

public class MongoReminderLogRepository : MongoBaseRepository<ReminderLogDocument>, IReminderLogRepository
{
    public MongoReminderLogRepository(IConfiguration config)
        : base(config, "reminders_log") { }

    public async Task CreateAsync(ReminderLog log)
    {
        var doc = new ReminderLogDocument
        {
            InvoiceId    = log.InvoiceId,
            ClientId     = log.ClientId,
            ReminderType = log.ReminderType,
            SentAt       = log.SentAt,
            StatusBefore = log.StatusBefore,
            StatusAfter  = log.StatusAfter,
            EmailSentTo  = log.EmailSentTo,
            Success      = log.Success,
            ErrorMessage = log.ErrorMessage ?? string.Empty
        };

        await Collection.InsertOneAsync(doc);
    }
}


