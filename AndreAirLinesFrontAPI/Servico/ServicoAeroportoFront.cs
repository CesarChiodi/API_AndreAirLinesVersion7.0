using System.Collections.Generic;
using System.Threading.Tasks;
using AndreAirLinesFrontAPI.Models;
using AndreAirLinesFrontAPI.Configuracao;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AndreAirLinesFrontAPI.Servico
{
    public class ServicoAeroportoFront
    {
        private readonly IMongoCollection<AeroportoMicroServico> _aeroporto;

        public ServicoAeroportoFront(IAeroportoApiFront configuracao)
        {
            var aeroporto = new MongoClient(configuracao.ConnectionString);
            var database = aeroporto.GetDatabase(configuracao.DatabaseName);
            _aeroporto = database.GetCollection<AeroportoMicroServico>(configuracao.AeroportoCollectionName);
        }

        public List<AeroportoMicroServico> Get() =>
            _aeroporto.Find(aeroporto => true).ToList();

    }
}
