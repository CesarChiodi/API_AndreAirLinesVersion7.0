using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Modelo
{
    public class Log
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string IdLog { get; set; }
        public Usuario Usuario { get; set; }
        public string EntidadeAntes  { get; set; }
        public string EntidadeDepois { get; set; }
        public string Operacao { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime Data { get; set; }

    }
}
