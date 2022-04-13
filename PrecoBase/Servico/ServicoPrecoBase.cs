using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoPrecoBaseAPI.Configuraao;
using MicroServicoVooAPI.Servico;
using Modelo;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MicroServicoPrecoBaseAPI.Servico
{
    public class ServicoPrecoBase
    {
        private readonly IMongoCollection<PrecoBase> _precoBase;

        public ServicoPrecoBase(IPrecoBaseAPI configuracao)
        {
            var precoBase = new MongoClient(configuracao.ConnectionString);
            var database = precoBase.GetDatabase(configuracao.DatabaseName);
            _precoBase = database.GetCollection<PrecoBase>(configuracao.PrecoBaseCollectionName);
        }

        public List<PrecoBase> Get() =>
            _precoBase.Find(precoBase => true).ToList();

        public PrecoBase Get(string id) =>
            _precoBase.Find<PrecoBase>(precoBase => precoBase.Id == id).FirstOrDefault();

        public async Task<PrecoBase> Create(PrecoBase precoBase)
        {
            var encontraPrecoBase = Get(precoBase.Id);
            var usuarioPrecoBase = await ServicoVerificaUsuarioPrecoBase.BuscaUsuarioPrecoBase(precoBase.LoginUsuario);

            if (usuarioPrecoBase == null)
            {
                return null;
            }

            var aeroportoOrigem = await VerificaAeroportoV.EncontraAeroportoV(precoBase.Origem);
            if (aeroportoOrigem != null)
            {
                precoBase.Origem = aeroportoOrigem;
            }
            else
            {
                return null;
            }

            var aeroportoDestino = await VerificaAeroportoV.EncontraAeroportoV(precoBase.Destino);
            if (aeroportoDestino != null)
            {
                precoBase.Destino = aeroportoDestino;
            }
            else
            {
                return null;
            }

            if (usuarioPrecoBase.Setor != "ADM")
            {
                return null;
            }

            if (precoBase.LoginUsuario == null)
            {
                return null;
            }

            if (encontraPrecoBase == null)
            {
                _precoBase.InsertOne(precoBase);

                Log log = new();
                log.Usuario = usuarioPrecoBase;
                log.EntidadeAntes = "";
                log.EntidadeDepois = JsonConvert.SerializeObject(encontraPrecoBase);
                log.Operacao = "create";
                log.Data = DateTime.Now.Date;

                var verifica = await ServicoInsereLogPrecoBase.InsereLogPrecoBase(log);

                if (verifica == "ok")
                {
                    return precoBase;
                }
                return precoBase;
            }
            return null;
        }

        public async Task<PrecoBase> Atualizar(string id, PrecoBase precoBaseMudanca)
        {
            var encontraPrecoBase = Get(precoBaseMudanca.Id);
            var usuarioPrecoBase = await ServicoVerificaUsuarioPrecoBase.BuscaUsuarioPrecoBase(precoBaseMudanca.LoginUsuario);

            if (usuarioPrecoBase == null)
            {
                return null;
            }

            var aeroportoOrigem = await VerificaAeroportoV.EncontraAeroportoV(precoBaseMudanca.Origem);
            if (aeroportoOrigem != null)
            {
                precoBaseMudanca.Origem = aeroportoOrigem;
            }
            else
            {
                return null;
            }

            var aeroportoDestino = await VerificaAeroportoV.EncontraAeroportoV(precoBaseMudanca.Destino);
            if (aeroportoDestino != null)
            {
                precoBaseMudanca.Destino = aeroportoDestino;
            }
            else
            {
                return null;
            }

            if (usuarioPrecoBase.Setor != "ADM")
            {
                return null;
            }

            if (precoBaseMudanca.LoginUsuario == null)
            {
                return null;
            }

            _precoBase.ReplaceOne(precoBase => precoBase.Id == id, precoBaseMudanca);

            Log log = new();
            log.Usuario = usuarioPrecoBase;
            log.EntidadeAntes = "";
            log.EntidadeDepois = JsonConvert.SerializeObject(encontraPrecoBase);
            log.Operacao = "atualizar";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogPrecoBase.InsereLogPrecoBase(log);

            if (verifica == "ok")
            {
                _precoBase.DeleteOne(precoBaseMudanca => precoBaseMudanca.Id == precoBaseMudanca.Id);

                return precoBaseMudanca;
            }

            return precoBaseMudanca;
        }

        public void Remover(PrecoBase precoBaseMudanca) =>
            _precoBase.DeleteOne(precoBase => precoBase.Id == precoBaseMudanca.Id);

        public async Task<string> Remover(string id, PrecoBase precoBaseMudanca, Usuario usuario)
        {
            var encontraPrecoBase = Get(precoBaseMudanca.Id);
            var usuarioPrecoBase = await ServicoVerificaUsuarioPrecoBase.BuscaUsuarioPrecoBase(precoBaseMudanca.LoginUsuario);

            if (usuarioPrecoBase == null)
            {
                return null;
            }

            var aeroportoOrigem = await VerificaAeroportoV.EncontraAeroportoV(precoBaseMudanca.Origem);
            if (aeroportoOrigem != null)
            {
                precoBaseMudanca.Origem = aeroportoOrigem;
            }
            else
            {
                return null;
            }

            var aeroportoDestino = await VerificaAeroportoV.EncontraAeroportoV(precoBaseMudanca.Destino);
            if (aeroportoDestino != null)
            {
                precoBaseMudanca.Destino = aeroportoDestino;
            }
            else
            {
                return null;
            }

            if (usuarioPrecoBase.Setor != "ADM")
            {
                return null;
            }

            if (precoBaseMudanca.LoginUsuario == null)
            {
                return null;
            }

            _precoBase.DeleteOne(precoBaseMudanca => precoBaseMudanca.Id == id);

            Log log = new();
            log.Usuario = usuarioPrecoBase;
            log.EntidadeAntes = "";
            log.EntidadeDepois = JsonConvert.SerializeObject(encontraPrecoBase);
            log.Operacao = "remover";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogPrecoBase.InsereLogPrecoBase(log);

            if (verifica == "ok")
            {
                _precoBase.DeleteOne(precoBaseMudanca => precoBaseMudanca.Id == precoBaseMudanca.Id);
            }

            return verifica;
        }
    }
}
