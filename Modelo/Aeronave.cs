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
    public class Aeronave
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdAeronave { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Registro { get; set; }

        public string NomeAeronave { get; set; }

        public int CapacidadeAeronave { get; set; }

        public string LoginUsuario { get; set; }
    }
}