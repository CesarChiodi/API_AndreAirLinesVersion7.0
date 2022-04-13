using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Modelo
{
    public class Aeroporto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdAeroporto { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Iata { get; set; }

        public string Nome { get; set; }

        public Endereco EnderecoAeroporto { get; set; }

        public string LoginUsuario { get; set; }
    }
}