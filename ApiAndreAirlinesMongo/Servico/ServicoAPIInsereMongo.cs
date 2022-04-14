using System.Collections.Generic;
using ApiAndreAirlinesMongo.Configuracao;
using Modelo;
using MongoDB.Driver;

namespace ApiAndreAirlinesMongo.Servico
{
    public class ServicoAPIInsereMongo
    {
        private readonly IMongoCollection<Log> _log;

        public ServicoAPIInsereMongo(IInsereLogMongo configuracao)
        {
            var log = new MongoClient(configuracao.ConnectionString);
            var database = log.GetDatabase(configuracao.DatabaseName);
            _log = database.GetCollection<Log>(configuracao.LogCollectionName);
        }

        public List<Log> Get() =>
            _log.Find(log => true).ToList();

        public Log Get(string id) =>
            _log.Find<Log>(log => log.IdLog == id).FirstOrDefault();


        public Log Create(Log log)
        {
            _log.InsertOne(log);
            return log;
        }
    }
}
