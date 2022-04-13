using System.Collections.Generic;
using System.Threading.Tasks;
using Modelo;
using MongoDB.Driver;
using MicroServicoPassagemAereaAPI.Configuracao;
using Newtonsoft.Json;
using System;
using MicroServicoVooAPI.Servico;

namespace MicroServicoPassagemAereaAPI.Servico
{
    public class ServicoPassagem
    {
        private readonly IMongoCollection<PassagemAerea> _passagem;

        public ServicoPassagem(IPassagemAPI configuracao)
        {
            var passagem = new MongoClient(configuracao.ConnectionString);
            var database = passagem.GetDatabase(configuracao.DatabaseName);
            _passagem = database.GetCollection<PassagemAerea>(configuracao.PassagemAereaCollectionName);
        }

        public List<PassagemAerea> Get() =>
            _passagem.Find(passagem => true).ToList();

        public PassagemAerea Get(string id) =>
            _passagem.Find(passagem => passagem.Id == id).FirstOrDefault();

        public async Task<PassagemAerea> Create(PassagemAerea passagem)
        {
            var encontraPassagem = Get(passagem.Id);
            var usuarioPassagem = await ServicoVerificaUsuarioPassagemAerea.BuscaUsuarioPassagemAerea(passagem.LoginUsuario);

            if (usuarioPassagem == null)
            {
                return null;
            }

            var aeroportoOrigem = await VerificaAeroportoV.EncontraAeroportoV(passagem.Voo.Origem);
            if (aeroportoOrigem != null)
            {
                passagem.Voo.Origem = aeroportoOrigem;
            }
            else
            {
                return null;
            }

            var aeroportoDestino = await VerificaAeroportoV.EncontraAeroportoV(passagem.Voo.Destino);
            if (aeroportoDestino != null)
            {
                passagem.Voo.Destino = aeroportoDestino;
            }
            else
            {
                return null;
            }

            if (aeroportoOrigem.IdAeronave.Equals(aeroportoDestino.IdAeronave) || aeroportoDestino.IdAeronave.Equals(aeroportoOrigem.IdAeronave))
            {
                return null;
            }

            var aeronave = await VerificaAeronaveV.EncontraAeronaveV(passagem.Voo.Aeronave);
            if (aeronave != null)
            {
                passagem.Voo.Aeronave = aeronave;
            }
            else
            {
                return null;
            }

            var passageiro = await VerificaPassageiroPA.EncontraPassageiroPA(passagem.Passageiro);
            if (passageiro != null)
            {
                passagem.Passageiro = passageiro;
            }
            else
            {
                return null;
            }

            if (usuarioPassagem.Setor != "ADM" && usuarioPassagem.Setor != "USUARIO")
            {
                return null;
            }

            if (passagem.LoginUsuario == null)
            {
                return null;
            }

            if (encontraPassagem == null)
            {
                _passagem.InsertOne(passagem);

                Log log = new();
                log.Usuario = usuarioPassagem;
                log.EntidadeAntes = "";
                log.EntidadeDepois = JsonConvert.SerializeObject(encontraPassagem);
                log.Operacao = "create";
                log.Data = DateTime.Now.Date;

                var verifica = await ServicoInsereLogPassagemAerea.InsereLogPassagemAerea(log);

                if (verifica == "ok")
                {
                    return passagem;
                }
                return passagem;
            }
            
            return null;
        }

        public async Task<PassagemAerea> Atualizar(string id, PassagemAerea passagemMudanca)
        {
            var encontraPassagem = Get(passagemMudanca.Id);
            var usuarioPassagem = await ServicoVerificaUsuarioPassagemAerea.BuscaUsuarioPassagemAerea(passagemMudanca.LoginUsuario);

            if (usuarioPassagem == null)
            {
                return null;
            }

            var aeroportoOrigem = await VerificaAeroportoV.EncontraAeroportoV(passagemMudanca.Voo.Origem);
            if (aeroportoOrigem != null)
            {
                passagemMudanca.Voo.Origem = aeroportoOrigem;
            }
            else
            {
                return null;
            }

            var aeroportoDestino = await VerificaAeroportoV.EncontraAeroportoV(passagemMudanca.Voo.Destino);
            if (aeroportoDestino != null)
            {
                passagemMudanca.Voo.Destino = aeroportoDestino;
            }
            else
            {
                return null;
            }

            if (usuarioPassagem.Setor != "ADM")
            {
                return null;
            }

            if (passagemMudanca.LoginUsuario == null)
            {
                return null;
            }

            _passagem.ReplaceOne(precoBase => precoBase.Id == id, passagemMudanca);

            Log log = new();
            log.Usuario = usuarioPassagem;
            log.EntidadeAntes = "";
            log.EntidadeDepois = JsonConvert.SerializeObject(encontraPassagem);
            log.Operacao = "atualizar";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogPassagemAerea.InsereLogPassagemAerea(log);

            if (verifica == "ok")
            {
                _passagem.DeleteOne(precoBaseMudanca => precoBaseMudanca.Id == precoBaseMudanca.Id);

                return passagemMudanca;
            }

            return passagemMudanca;
        }

        public void Remover(PassagemAerea passagemMudanca) =>
            _passagem.DeleteOne(passagem => passagem.Id == passagemMudanca.Id);

        public async Task<string> Remover(string id, PassagemAerea passagemMudanca, Usuario usuario)
        {
            var encontraPassagem = Get(passagemMudanca.Id);
            var usuarioPassagem = await ServicoVerificaUsuarioPassagemAerea.BuscaUsuarioPassagemAerea(passagemMudanca.LoginUsuario);

            if (usuarioPassagem == null)
            {
                return null;
            }

            var aeroportoOrigem = await VerificaAeroportoV.EncontraAeroportoV(passagemMudanca.Voo.Origem);
            if (aeroportoOrigem != null)
            {
                passagemMudanca.Voo.Origem = aeroportoOrigem;
            }
            else
            {
                return null;
            }

            var aeroportoDestino = await VerificaAeroportoV.EncontraAeroportoV(passagemMudanca.Voo.Destino);
            if (aeroportoDestino != null)
            {
                passagemMudanca.Voo.Destino = aeroportoDestino;
            }
            else
            {
                return null;
            }

            if (usuarioPassagem.Setor != "ADM")
            {
                return null;
            }

            if (passagemMudanca.LoginUsuario == null)
            {
                return null;
            }

            _passagem.DeleteOne(passagemMudanca => passagemMudanca.Id == id);

            Log log = new();
            log.Usuario = usuarioPassagem;
            log.EntidadeAntes = "";
            log.EntidadeDepois = JsonConvert.SerializeObject(encontraPassagem);
            log.Operacao = "remover";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogPassagemAerea.InsereLogPassagemAerea(log);

            if (verifica == "ok")
            {
                _passagem.DeleteOne(precoBaseMudanca => precoBaseMudanca.Id == precoBaseMudanca.Id);
            }

            return verifica;
        }
    }
}
