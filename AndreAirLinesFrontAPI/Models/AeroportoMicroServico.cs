

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AndreAirLinesFrontAPI.Models
{
    public class AeroportoMicroServico
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdAeroporto { get; set; } 

        public string Iata { get; set; }

        public string Nome { get; set; }

        public EnderecoMongo EnderecoAeroporto { get; set; }

        public string LoginUsuario { get; set; }
    }
}
