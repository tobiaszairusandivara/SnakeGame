using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SnakeApi.Models.DTOs
{
    public class Score
    {
        [BsonId]//Designarda como clave principal
        [BsonRepresentation(BsonType.ObjectId)] //permite que se le pase un string en lugar de una estructura BsonRep.
        public string Id { get; set; }

        [BsonElement("username")]// El valor del atributo representa el nombre de propiedad en la colección de MongoDB
        public string Username { get; set; }

        [BsonElement("scoreValue")]
        public int ScoreValue { get; set; }
    }
}