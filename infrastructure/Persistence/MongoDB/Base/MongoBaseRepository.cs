namespace Infrastructure.Persistence.MongoDB.Base;

using Core.Messages;
using global::MongoDB.Driver;
using Microsoft.Extensions.Configuration;

public abstract class MongoBaseRepository<TDocument>
{
    protected readonly IMongoCollection<TDocument> Collection;

    protected MongoBaseRepository(IConfiguration config, string collectionName)
    {
        var connectionString = config["MongoDB:ConnectionString"]
            ?? throw new ArgumentNullException(Message.ErrorInizialiteMongoDB);

        var databaseName = config["MongoDB:DatabaseName"];

        var client   = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);

        Collection = database.GetCollection<TDocument>(collectionName);
    }
}


