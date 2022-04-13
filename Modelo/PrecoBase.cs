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
    public class PrecoBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public decimal Valor { get; set; }

        public DateTime DataInclusao { get; set; }

        public Aeronave Destino { get; set; }

        public Aeronave Origem { get; set; }

        public string LoginUsuario { get; set; }
    }
}
