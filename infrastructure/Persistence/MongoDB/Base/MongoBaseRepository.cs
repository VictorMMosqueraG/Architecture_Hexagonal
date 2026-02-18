namespace Infrastructure.Persistence.MongoDB.Base;

using Core.Messages;
using global::MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

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

    protected async Task<(IEnumerable<TDocument> Data, long Total)> GetPaginatedAsync(
        FilterDefinition<TDocument> filter,
        int page,
        int pageSize,
        string? sortField,
        string? order)
    {
        var total = await Collection.CountDocumentsAsync(filter);

        string field = ConvertToPascalCase(sortField ?? "CreatedAt");

        var sort = order?.ToLower() == "desc" 
            ? Builders<TDocument>.Sort.Descending(field) 
            : Builders<TDocument>.Sort.Ascending(field);

        var data = await Collection.Find(filter)
            .Sort(sort)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return (data, total);
    }

    private static string ConvertToPascalCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return "CreatedAt";
        
        return Regex.Replace(input, "(?:^|_)([a-z])", m => m.Groups[1].Value.ToUpper());
    }
}