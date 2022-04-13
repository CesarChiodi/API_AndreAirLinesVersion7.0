using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoAeronaveAPI.Configuracao;
using Modelo;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MicroServicoAeronaveAPI.Servico
{
    public class ServicoAeronave
    {
        private readonly IMongoCollection<Aeronave> _aeronave;

        public ServicoAeronave(IAeronaveAPI configuracao)
        {
            var aeronave = new MongoClient(configuracao.ConnectionString);
            var database = aeronave.GetDatabase(configuracao.DatabaseName);
            _aeronave = database.GetCollection<Aeronave>(configuracao.AeronaveCollectionName);
        }

        public List<Aeronave> Get() =>
            _aeronave.Find(aeronave => true).ToList();

        public Aeronave Get(string id) =>
            _aeronave.Find<Aeronave>(aeronave => aeronave.IdAeronave == id).FirstOrDefault();

        public Aeronave GetRegistro(string id) =>
           _aeronave.Find<Aeronave>(encontraAeronave => encontraAeronave.Registro == id).FirstOrDefault();

       
        public async Task <Aeronave> Create(Aeronave aeronave)
        {

            var encontraAeronave = GetRegistro(aeronave.Registro);
            var usuarioAeronave = await ServicoVerificaUsuarioAeronave.BuscaUsuarioAeronave(aeronave.LoginUsuario);
            if (usuarioAeronave == null)
            {
                return null;
            }
            if (usuarioAeronave.Login != "ADM")
            {
                return null;
            }

            if (aeronave.LoginUsuario == null)
            {
                return null;
            }

            if (encontraAeronave == null)
            {
                _aeronave.InsertOne(aeronave);

                Log log = new();
                log.Usuario = usuarioAeronave;
                log.EntidadeAntes = "";
                log.EntidadeDepois = JsonConvert.SerializeObject(encontraAeronave);
                log.Operacao = "create";
                log.Data = DateTime.Now.Date;

                var verifica = await ServicoInsereLogAeronave.InsereLogAeronave(log);

                if(verifica == "ok")
                {
                    _aeronave.DeleteOne(aeronaveMudanca => aeronaveMudanca.IdAeronave == aeronave.IdAeronave);

                    return aeronave;
                }

                return aeronave;
            }
            
            return null;
        }

        public async Task<Aeronave> Atualizar(string id, Aeronave aeronaveMudanca)
        {
            var encontraAeronave = GetRegistro(aeronaveMudanca.Registro);
            var usuarioAeronave = await ServicoVerificaUsuarioAeronave.BuscaUsuarioAeronave(aeronaveMudanca.LoginUsuario);
            if (usuarioAeronave == null)
            {
                return null;
            }

            if (usuarioAeronave.Setor != "ADM")
            {
                return null;
            }

            if (aeronaveMudanca.LoginUsuario == null)
            {
                return null;
            }

            _aeronave.ReplaceOne(aeronave => aeronave.IdAeronave == id, aeronaveMudanca);

            Log log = new();
            log.Usuario = usuarioAeronave;
            log.EntidadeAntes = JsonConvert.SerializeObject(encontraAeronave);
            log.EntidadeDepois = JsonConvert.SerializeObject(aeronaveMudanca);
            log.Operacao = "atualizacao";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogAeronave.InsereLogAeronave(log);

            if (verifica == "ok")
            {
                _aeronave.DeleteOne(aeronaveMudanca => aeronaveMudanca.IdAeronave == aeronaveMudanca.IdAeronave);

                return aeronaveMudanca;
            }

            return aeronaveMudanca;
        }

        public void Remover(Aeronave aeronaveMudanca) =>
            _aeronave.DeleteOne(aeronave => aeronave.IdAeronave == aeronaveMudanca.IdAeronave);

        public async Task<string> Remover(string id, Aeronave aeronaveMudanca, Usuario usuario)
        {
            var encontraAeronave =  GetRegistro(aeronaveMudanca.Registro);
            var usuarioAeronave = await ServicoVerificaUsuarioAeronave.BuscaUsuarioAeronave(usuario.LoginUsuario);

            if (usuarioAeronave == null)
            {
                return null;
            }
            if (usuarioAeronave.Setor != "ADM")
            {
                return null;
            }

            if (usuario.LoginUsuario == null)
            {
                return null;
            }
            _aeronave.DeleteOne(aeronave => aeronave.IdAeronave == id);

            Log log = new();
            log.Usuario = usuarioAeronave;
            log.EntidadeAntes = JsonConvert.SerializeObject(encontraAeronave); 
            log.EntidadeDepois = "";
            log.Operacao = "remover";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogAeronave.InsereLogAeronave(log);

            if (verifica == "ok")
            {
                _aeronave.InsertOne(encontraAeronave);
            }

            return verifica;
        } 
    }
}

