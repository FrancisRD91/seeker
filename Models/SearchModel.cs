using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace seeker.Models
{
	[BsonIgnoreExtraElements]
	public class UrlModel
	{
        [BsonElement("_id")]
        public Object SearchId  { get; set; }

        [BsonElement("word")]
        public string Word  { get; set; }

        [BsonElement("count")]
        public int Count  { get; set; }

        [BsonElement("url_id")]
        public Object URLId { get; set; }
	}
}