using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoAeroportoAPI.Configuracao;
using MicroServicoLogAPI.Servico;
using Modelo;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MicroServicoAeroportoAPI.Servico
{
    public class ServicoAeroporto
    {
        private readonly IMongoCollection<Aeroporto> _aeroporto;

        public ServicoAeroporto(IAeroportoAPI configuracao)
        {
            var aeroporto = new MongoClient(configuracao.ConnectionString);
            var database = aeroporto.GetDatabase(configuracao.DatabaseName);
            _aeroporto = database.GetCollection<Aeroporto>(configuracao.AeroportoCollectionName);
        }

        public List<Aeroporto> Get() =>
            _aeroporto.Find(aeroporto => true).ToList();


        public Aeroporto GetLogin(string login)                 
        {
            Aeroporto aeroporto = new();

            try
            {
                aeroporto = _aeroporto.Find<Aeroporto>(aeroporto => aeroporto.LoginUsuario == login).FirstOrDefault();
                return aeroporto;
            }
            catch (Exception excecao)
            {
                throw;
            }
        }

        public Aeroporto GetIata(string id) =>
            _aeroporto.Find<Aeroporto>(encontraAeroporto => encontraAeroporto.Iata == id).FirstOrDefault();

        public async Task <Aeroporto> Create(Aeroporto aeroporto)
        {
            var encontraAeroporto = GetIata(aeroporto.Iata);
            var usuarioAeroporto = await ServicoVerificaUsuarioAeroporto.BuscaUsuarioAeroporto(aeroporto.LoginUsuario);
            if (usuarioAeroporto == null)
            {
                return null;
            }
            if (usuarioAeroporto.Setor != "ADM")
            {
                return null;
            }

            if (aeroporto.LoginUsuario == null)
            {
                return null;
            }

            if (encontraAeroporto == null)
            {
                _aeroporto.InsertOne(aeroporto);

                Log log = new();
                log.Usuario = usuarioAeroporto;
                log.EntidadeAntes = "";
                log.EntidadeDepois = JsonConvert.SerializeObject(encontraAeroporto);
                log.Operacao = "create";
                log.Data = DateTime.Now.Date;

                var verifica = await ServicoInsereLogAeroporto.InsereLogAeroporto(log);

                if (verifica == "ok")
                {
                    _aeroporto.DeleteOne(aeroportoMudanca => aeroportoMudanca.IdAeroporto == aeroporto.IdAeroporto);

                    return aeroporto;
                }

                return aeroporto;
            }

            return null;
        }

        public async Task<Aeroporto> Atualizar(string id, Aeroporto aeroportoMudanca)
        {
            var encontraAeronave = GetIata(aeroportoMudanca.Iata);
            var usuarioAeroporto = await ServicoVerificaUsuarioAeroporto.BuscaUsuarioAeroporto(aeroportoMudanca.LoginUsuario);
            if (usuarioAeroporto == null)
            {
                return null;
            }

            if (usuarioAeroporto.Setor != "ADM")
            {
                return null;
            }

            if (aeroportoMudanca.LoginUsuario == null)
            {
                return null;
            }

            _aeroporto.ReplaceOne(aeroporto => aeroporto.IdAeroporto == id, aeroportoMudanca);

            Log log = new();
            log.Usuario = usuarioAeroporto;
            log.EntidadeAntes = JsonConvert.SerializeObject(encontraAeronave);
            log.EntidadeDepois = JsonConvert.SerializeObject(aeroportoMudanca);
            log.Operacao = "atualizacao";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogAeroporto.InsereLogAeroporto(log);

            if (verifica == "ok")
            {
                _aeroporto.DeleteOne(aeronaveMudanca => aeronaveMudanca.IdAeroporto == aeronaveMudanca.IdAeroporto);

                return aeroportoMudanca;
            }

            return aeroportoMudanca;
        }

        public void Remover(Aeroporto aeroportoMudanca) =>
            _aeroporto.DeleteOne(aeroporto => aeroporto.IdAeroporto == aeroportoMudanca.IdAeroporto);

        public async Task<string> Remover(string id, Aeroporto aeroportoMudanca, Usuario usuario)
        {
            var encontraAeroporto = GetIata(aeroportoMudanca.Iata);
            var usuarioAeroporto = await ServicoVerificaUsuarioAeroporto.BuscaUsuarioAeroporto(aeroportoMudanca.LoginUsuario);

            if (usuarioAeroporto == null)
            {
                return null;
            }
            if (usuarioAeroporto.Setor != "ADM")
            {
                return null;
            }

            if (aeroportoMudanca.LoginUsuario == null)
            {
                return null;
            }
            _aeroporto.DeleteOne(aeroporto => aeroporto.IdAeroporto == id);

            Log log = new();
            log.Usuario = usuarioAeroporto;
            log.EntidadeAntes = JsonConvert.SerializeObject(encontraAeroporto);
            log.EntidadeDepois = "";
            log.Operacao = "remover";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogAeroporto.InsereLogAeroporto(log);

            if (verifica == "ok")
            {
                _aeroporto.InsertOne(encontraAeroporto);
            }

            return verifica;
        }
    }
}

