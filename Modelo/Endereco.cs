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
    public class Endereco
    {

        public string Bairro { get; set; }

        public string Localidade { get; set; }

        public string Pais { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string UF { get; set; }

        public int Numero { get; set; }

        public string Complemento { get; set; }

        public string Continente { get; set; }

        public Endereco()
        {
        }

        public Endereco( string bairro, string localidade, string cep, string logradouro, string uf, string pais, int numero, string complemento, string continente)
        {
            Bairro = bairro;
            Localidade = localidade;
            Cep = cep;
            Logradouro = logradouro;
            UF = uf;
            Pais = pais;
            Numero = numero;
            Complemento = complemento;
            Continente = continente;
        }
    }
}
