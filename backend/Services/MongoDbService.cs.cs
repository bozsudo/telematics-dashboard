using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDbService
{
    private readonly IMongoCollection<BsonDocument> _collection;

    public MongoDbService()
    {
        var client = new MongoClient("mongodb://mongodb:27017");
        var database = client.GetDatabase("telematics");
        _collection = database.GetCollection<BsonDocument>("telemetry");
    }

    public void InsertTelemetryData(BsonDocument data)
    {
        _collection.InsertOne(data);
    }
}
