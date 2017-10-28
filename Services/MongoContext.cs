using MongoDB.Driver;
namespace seeker.Services
{
    public class MongoContext
	{
        public readonly IMongoDatabase db;
        public MongoContext(string connectionStr)
        {
            var mongoClient = new MongoClient(connectionStr);
            db = mongoClient.GetDatabase("seeker_db");
        }
	}
}