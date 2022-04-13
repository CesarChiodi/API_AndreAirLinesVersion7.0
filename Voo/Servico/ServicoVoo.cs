using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoVooAPI.Configuracao;
using Modelo;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MicroServicoVooAPI.Servico
{
    public class ServicoVoo
    {
        private readonly IMongoCollection<Voo> _voo;

        public ServicoVoo(IVooAPI configuracao)
        {
            var voo = new MongoClient(configuracao.ConnectionString);
            var database = voo.GetDatabase(configuracao.DatabaseName);
            _voo = database.GetCollection<Voo>(configuracao.VooCollectionName);
        }

        public List<Voo> Get() =>
            _voo.Find(voo => true).ToList();

        public Voo Get(string id) =>
            _voo.Find<Voo>(voo => voo.Id == id).FirstOrDefault();

        public async Task <Voo> Create(Voo voo)
        {
            var encontraVoo = Get(voo.Id);
            var usuarioVoo = await ServicoVerificaUsuarioVoo.BuscaUsuarioVoo(voo.LoginUsuario);

            if (usuarioVoo == null)
            {
                return null;
            }
            if (usuarioVoo.Setor != "ADM" && usuarioVoo.Setor != "USUARIO")
            {
                return null;
            }

            if (voo.LoginUsuario == null)
            {
                return null;
            }

            var aeroportoOrigem = await VerificaAeroportoV.EncontraAeroportoV(voo.Origem);
            if(aeroportoOrigem != null)
            {
                voo.Origem = aeroportoOrigem;
            }
            else
            {
                return null;
            }

            var aeroportoDestino = await VerificaAeroportoV.EncontraAeroportoV(voo.Destino);
            if (aeroportoDestino != null)
            {
                voo.Destino = aeroportoDestino;
            }
            else
            {
                return null;
            }

            if (aeroportoOrigem.IdAeronave.Equals(aeroportoDestino.IdAeronave) || aeroportoDestino.IdAeronave.Equals(aeroportoOrigem.IdAeronave))
            {
                return null;
            }

            var aeronave = await VerificaAeronaveV.EncontraAeronaveV(voo.Aeronave);
            if (aeronave != null)
            {
                voo.Aeronave = aeronave;
            }
            else
            {
                return null;
            }

            if (encontraVoo == null)
            {
                _voo.InsertOne(voo);

                Log log = new();
                log.Usuario = usuarioVoo;
                log.EntidadeAntes = "";
                log.EntidadeDepois = JsonConvert.SerializeObject(encontraVoo);
                log.Operacao = "create";
                log.Data = DateTime.Now.Date;

                var verifica = await ServicoInsereLogVoo.InsereLogVoo(log);

                if (verifica == "ok")
                {
                    return voo;
                }
                return voo;
            }

            return null;
        }

        public async Task<Voo> Atualizar(string id, Voo vooMudanca)
        {
            var encontraVoo = Get(vooMudanca.Id);
            var usuarioVoo = await ServicoVerificaUsuarioVoo.BuscaUsuarioVoo(vooMudanca.LoginUsuario);

            if (usuarioVoo == null)
            {
                return null;
            }
            if (usuarioVoo.Setor != "ADM" && usuarioVoo.Setor != "USUARIO")
            {
                return null;
            }

            if (vooMudanca.LoginUsuario == null)
            {
                return null;
            }

            var aeroportoOrigem = await VerificaAeroportoV.EncontraAeroportoV(vooMudanca.Origem);
            if (aeroportoOrigem != null)
            {
                vooMudanca.Origem = aeroportoOrigem;
            }
            else
            {
                return null;
            }

            var aeroportoDestino = await VerificaAeroportoV.EncontraAeroportoV(vooMudanca.Destino);
            if (aeroportoDestino != null)
            {
                vooMudanca.Destino = aeroportoDestino;
            }
            else
            {
                return null;
            }

            if (aeroportoOrigem.IdAeronave.Equals(aeroportoDestino.IdAeronave) || aeroportoDestino.IdAeronave.Equals(aeroportoOrigem.IdAeronave))
            {
                return null;
            }

            var aeronave = await VerificaAeronaveV.EncontraAeronaveV(vooMudanca.Aeronave);
            if (aeronave != null)
            {
                vooMudanca.Aeronave = aeronave;
            }
            else
            {
                return null;
            }

            _voo.ReplaceOne(voo => voo.Id == id, vooMudanca);


            Log log = new();
            log.Usuario = usuarioVoo;
            log.EntidadeAntes = "";
            log.EntidadeDepois = JsonConvert.SerializeObject(encontraVoo);
            log.Operacao = "atualizar";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogVoo.InsereLogVoo(log);

            if (verifica == "ok")
            {
                return vooMudanca;
            }
            return vooMudanca;
        }

        public void Remover(Voo vooMudanca) =>
            _voo.DeleteOne(voo => voo.Id == vooMudanca.Id);

        public async Task<string> Remover(string id, Voo vooMudanca, Usuario usuario)
        {
            var encontraVoo = Get(vooMudanca.Id);
            var usuarioVoo = await ServicoVerificaUsuarioVoo.BuscaUsuarioVoo(vooMudanca.LoginUsuario);

            if (usuarioVoo == null)
            {
                return null;
            }
            if (usuarioVoo.Setor != "ADM" && usuarioVoo.Setor != "USUARIO")
            {
                return null;
            }

            if (vooMudanca.LoginUsuario == null)
            {
                return null;
            }

            var aeroportoOrigem = await VerificaAeroportoV.EncontraAeroportoV(vooMudanca.Origem);
            if (aeroportoOrigem != null)
            {
                vooMudanca.Origem = aeroportoOrigem;
            }
            else
            {
                return null;
            }

            var aeroportoDestino = await VerificaAeroportoV.EncontraAeroportoV(vooMudanca.Destino);
            if (aeroportoDestino != null)
            {
                vooMudanca.Destino = aeroportoDestino;
            }
            else
            {
                return null;
            }

            if (aeroportoOrigem.IdAeronave.Equals(aeroportoDestino.IdAeronave) || aeroportoDestino.IdAeronave.Equals(aeroportoOrigem.IdAeronave))
            {
                return null;
            }

            var aeronave = await VerificaAeronaveV.EncontraAeronaveV(vooMudanca.Aeronave);
            if (aeronave != null)
            {
                vooMudanca.Aeronave = aeronave;
            }
            else
            {
                return null;
            }

            _voo.ReplaceOne(voo => voo.Id == id, vooMudanca);


            Log log = new();
            log.Usuario = usuarioVoo;
            log.EntidadeAntes = "";
            log.EntidadeDepois = JsonConvert.SerializeObject(encontraVoo);
            log.Operacao = "remover";
            log.Data = DateTime.Now.Date;

            var verifica = await ServicoInsereLogVoo.InsereLogVoo(log);

            if (verifica == "ok")
            {
                _voo.DeleteOne(voo => voo.Id == id);
            }
            return verifica;
           
        }
    }
}

