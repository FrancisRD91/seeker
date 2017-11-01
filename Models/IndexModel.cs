using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace seeker.Models
{
    [BsonIgnoreExtraElements]
    public class IndexModel
    {

        [BsonId]
        public ObjectId URLId { get; set; }

        [BsonElement("url")]
        public string URL { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("words")]
        public String[] Words { get; set; }
    }
}