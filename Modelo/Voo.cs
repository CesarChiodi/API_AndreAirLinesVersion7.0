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
    public class Root
    {
        public string DataCadastro { get; set; }
        public Voo Voo { get; set; }
    }

    public class Voo
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public DateTime HorarioEmbarque { get; set; }

        public DateTime HorarioDesembarque { get; set; }

        public Aeronave Destino { get; set; }
       
        public Aeronave Origem { get; set; }

        public Aeronave Aeronave { get; set; }

        public string LoginUsuario { get; set; }

    }
}
