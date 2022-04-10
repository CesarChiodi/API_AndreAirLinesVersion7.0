using System.Collections.Generic;
using MicroServicoLog.Configuracao;
using Modelo;
using MongoDB.Driver;

namespace MicroServicoLog.Servico
{
    public class ServicoLog
    {
        private readonly IMongoCollection<Log> _log;

        public ServicoLog(ILogAPI configuracao)
        {
            var log = new MongoClient(configuracao.ConnectionString);
            var database = log.GetDatabase(configuracao.DatabaseName);
            _log = database.GetCollection<Log>(configuracao.LogCollectionName);
        }

        public List<Log> Get() =>
            _log.Find(log => true).ToList();

        public Log Get(string id) =>
            _log.Find<Log>(log => log.IdLog == id).FirstOrDefault();

        public Log GetRegistro(string id) =>
           _log.Find<Log>(encontraLog => encontraLog.IdLog == id).FirstOrDefault();


        public Log Create(Log log)
        {
            _log.InsertOne(log);
            return log;
        }

        public void Atualizar(string id, Log logMudanca) =>
            _log.ReplaceOne(log => log.IdLog == id, logMudanca);

        public void Remover(Log logMudanca) =>
            _log.DeleteOne(log => log.IdLog == logMudanca.IdLog);

        public void Remover(string id) =>
            _log.DeleteOne(log => log.IdLog == id);
    }
}
