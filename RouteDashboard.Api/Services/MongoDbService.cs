
using MongoDB.Bson;
using MongoDB.Driver;

namespace RouteDashboard.Api.Services;

public class MongoDbService
{
    private readonly IMongoDatabase _database;

    public MongoDbService(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDb:ConnectionString"];
        var databaseName = configuration["MongoDb:DatabaseName"];

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                "A Connection String do MongoDB não foi configurada.");

        if (string.IsNullOrWhiteSpace(databaseName))
            throw new InvalidOperationException(
                "O nome do banco de dados não foi configurado.");

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    public IMongoCollection<BsonDocument> Users =>
        GetCollection<BsonDocument>("Users");

    public IMongoCollection<BsonDocument> Preferences =>
        GetCollection<BsonDocument>("Preferences");

    public async Task PingAsync()
    {
        await _database.RunCommandAsync<BsonDocument>(
            new BsonDocument("ping", 1));
    }
}
