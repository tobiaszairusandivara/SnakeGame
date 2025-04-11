using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SnakeApi.Models
{
    public class Score
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("score")]
        public int ScoreValue { get; set; }
    }
}