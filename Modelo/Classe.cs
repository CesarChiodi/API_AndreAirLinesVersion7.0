using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Modelo
{
    public class Classe
    {
        public decimal ValorClasse { get; set; }

        public string Descricao { get; set; }
    }
}
