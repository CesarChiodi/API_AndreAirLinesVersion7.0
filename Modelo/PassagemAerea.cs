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
    public class PassagemAerea
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public DateTime DataCadastro { get; set; }

        public decimal PromocaoPorcentagem { get; set; }

        public Voo Voo { get; set; }
        public Passageiro Passageiro { get; set; }

        public decimal ValorPassagem { get; set; }

        public Classe Classe { get; set; }

        public PrecoBase PrecoBase { get; set; }

        public string LoginUsuario { get; set; }

        public decimal CalculaPreco()
        {
            return ValorPassagem = (PrecoBase.Valor + Classe.ValorClasse) * (PromocaoPorcentagem / 100);
        }
    }
}
