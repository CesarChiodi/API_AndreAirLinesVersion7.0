using MongoDB.Driver;
using Modelo;
using MicroServicoPassageiroAPI.Configuracao;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using MicroServicoPassageiroAPI.Servico;

namespace MicroServicoPassageiroAPI.Servico
{
    public class ServicoPassageiro
    {
        private readonly IMongoCollection<Passageiro> _passageiro;

        public ServicoPassageiro(IPassageiroAPI configuracao)
        {
            var passageiro = new MongoClient(configuracao.ConnectionString);
            var database = passageiro.GetDatabase(configuracao.DatabaseName);
            _passageiro = database.GetCollection<Passageiro>(configuracao.PassageiroCollectionName);
        }

        public List<Passageiro> Get() =>
            _passageiro.Find(passageiro => true).ToList();

        public Passageiro Get(string id) =>
            _passageiro.Find<Passageiro>(passageiro => passageiro.Id == id).FirstOrDefault();
        public Passageiro GetCpf(string id) =>
            _passageiro.Find<Passageiro>(encontraPassageiro => encontraPassageiro.Cpf == id).FirstOrDefault();

        public async Task <Passageiro> Create(Passageiro passageiro)
        {
            var encontraPassageiro = GetCpf(passageiro.Cpf);
            var usuarioPassageiro = await ServicoVerificaUsuarioPassageiro.BuscaUsuarioPassageiro(passageiro.LoginUsuario);
            if (usuarioPassageiro == null)
            {
                return null;
            }
            if (usuarioPassageiro.Setor != "ADM" && usuarioPassageiro.Setor != "USUARIO")
            {
                return null;
            }

            if (passageiro.LoginUsuario == null)
            {
                return null;
            }
            if (encontraPassageiro == null)
            {
                _passageiro.InsertOne(passageiro);

                Log log = new();
                log.Usuario = usuarioPassageiro;
                log.EntidadeAntes = "";
                log.EntidadeDepois = JsonConvert.SerializeObject(encontraPassageiro);
                log.Operacao = "create";
                log.Data = DateTime.Now.Date;

                var verifica = await ServicoInsereLogPassageiro.InsereLogPassageiro(log);

                if (verifica == "ok")
                {
                    _passageiro.DeleteOne(passageiroMudanca => passageiroMudanca.Id == passageiro.Id);

                    return passageiro;
                }

                return passageiro;
            }
            return null;
        }

        public async Task<Passageiro> Atualizar(string id, Passageiro passageiroMudanca)
        {
            var encontraPassageiro = GetCpf(passageiroMudanca.Cpf);
            var usuarioPassageiro = await ServicoVerificaUsuarioPassageiro.BuscaUsuarioPassageiro(passageiroMudanca.LoginUsuario);
            if (usuarioPassageiro == null)
            {
                return null;
            }
            if (usuarioPassageiro.Setor != "ADM" && usuarioPassageiro.Setor != "USUARIO")
            {
                return null;
            }

            if (passageiroMudanca.LoginUsuario == null)
            {
                return null;
            }

            _passageiro.ReplaceOne(passageiro => passageiro.Id == id, passageiroMudanca);

            Log log = new();
            log.Usuario = usuarioPassageiro;
            log.EntidadeAntes = "";
            log.EntidadeDepois = JsonConvert.SerializeObject(encontraPassageiro);
            log.Operacao = "atualizacao";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogPassageiro.InsereLogPassageiro(log);

            if (verifica == "ok")
            {
                _passageiro.DeleteOne(passageiroMudanca => passageiroMudanca.Id == passageiroMudanca.Id);

                return passageiroMudanca;
            }

            return passageiroMudanca;
        }

        public void Remover(Passageiro passageiroMudanca) =>
            _passageiro.DeleteOne(passageiro => passageiro.Id == passageiroMudanca.Id);

        public async Task<string> Remover(string id, Passageiro passageiroMudanca, Usuario usuario)
        {
            var encontraPassageiro = GetCpf(passageiroMudanca.Cpf);
            var usuarioPassageiro = await ServicoVerificaUsuarioPassageiro.BuscaUsuarioPassageiro(passageiroMudanca.LoginUsuario);
            if (usuarioPassageiro == null)
            {
                return null;
            }
            if (usuarioPassageiro.Setor != "ADM" && usuarioPassageiro.Setor != "USUARIO")
            {
                return null;
            }

            if (passageiroMudanca.LoginUsuario == null)
            {
                return null;
            }

            _passageiro.DeleteOne(passageiro => passageiro.Id == id);

            Log log = new();
            log.Usuario = usuarioPassageiro;
            log.EntidadeAntes = "";
            log.EntidadeDepois = JsonConvert.SerializeObject(encontraPassageiro);
            log.Operacao = "remover";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogPassageiro.InsereLogPassageiro(log);

            if (verifica == "ok")
            {
                _passageiro.InsertOne(passageiroMudanca);
            }

            return verifica;
        }
    }
}
