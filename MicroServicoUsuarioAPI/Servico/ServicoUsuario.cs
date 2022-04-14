using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoUsuarioAPI.Configuracao;
using Modelo;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MicroServicoUsuarioAPI.Servico
{
    public class ServicoUsuario
    {
        private readonly IMongoCollection<Usuario> _usuario;

        public ServicoUsuario(IUsuarioAPI configuracao)
        {
            var usuario = new MongoClient(configuracao.ConnectionString);
            var database = usuario.GetDatabase(configuracao.DatabaseName);
            _usuario = database.GetCollection<Usuario>(configuracao.UsuarioCollectionName);
        }

        public List<Usuario> Get() =>
            _usuario.Find(usuario => true).ToList();

        public Usuario GetLogin(string login) =>
            _usuario.Find<Usuario>(usuario => usuario.Login == login).FirstOrDefault();



        public Usuario BuscaCpf(string cpf)
        {
            Usuario usuario = new();
            //Usuario usuarioErro = new();

            try
            {
                usuario = _usuario.Find<Usuario>(usuario => usuario.Cpf == cpf).FirstOrDefault();

                return usuario;
            }
            catch (Exception excecao)
            {
                throw;
            }
        }

        public async Task<Usuario> Create(Usuario usuario)
        {
            var encontraUsuario = BuscaCpf(usuario.Cpf);
            var usuarioUsuario = await ServicoVerificaUsuario.BuscaUsuario(usuario.LoginUsuario);

            if (usuarioUsuario == null)
            {
                return null;
            }
            if (usuarioUsuario.Login != "ADM")
            {
                return null;
            }

            if (usuario.LoginUsuario == null)
            {
                return null;
            }

            if (usuarioUsuario == null)
            {
                _usuario.InsertOne(usuario);

                Log log = new();
                log.Usuario = usuarioUsuario;
                log.EntidadeAntes = "";
                log.EntidadeDepois = JsonConvert.SerializeObject(encontraUsuario);
                log.Operacao = "create";
                log.Data = DateTime.Now.Date;

                var verifica = await InsereLog.InsereLogUsuario(log);

                if (verifica == "ok")
                {
                    _usuario.DeleteOne(aeronaveMudanca => aeronaveMudanca.Id == usuario.Id);

                    return usuario;
                }

                return usuario;
            }

            return null;
        }



        public async Task<Usuario> Atualizar(string id, Usuario usuarioMudanca)
        {
            var encontraUsuario = BuscaCpf(usuarioMudanca.Cpf);
            var usuarioUsuario = await ServicoVerificaUsuario.BuscaUsuario(usuarioMudanca.LoginUsuario);

            if (usuarioUsuario == null)
            {
                return null;
            }
            if (usuarioUsuario.Login != "ADM")
            {
                return null;
            }

            if (usuarioMudanca.LoginUsuario == null)
            {
                return null;
            }

            _usuario.ReplaceOne(usuario => usuario.Id == id, usuarioMudanca);

            Log log = new();
            log.Usuario = usuarioUsuario;
            log.EntidadeAntes = "";
            log.EntidadeDepois = JsonConvert.SerializeObject(encontraUsuario);
            log.Operacao = "create";
            log.Data = DateTime.Now.Date;

            var verifica = await InsereLog.InsereLogUsuario(log);

            if (verifica == "ok")
            {
                _usuario.DeleteOne(aeronaveMudanca => aeronaveMudanca.Id == usuarioMudanca.Id);

                return usuarioMudanca;
            }

            return usuarioMudanca;
        }

        public void Remover(Usuario usuarioMudanca) =>
            _usuario.DeleteOne(usuario => usuario.Id == usuarioMudanca.Id);

        public async Task<string> Remover(string id, Usuario usuarioMudanca)
        {
            var encontraUsuario = BuscaCpf(usuarioMudanca.Cpf);
            var usuarioUsuario = await ServicoVerificaUsuario.BuscaUsuario(usuarioMudanca.LoginUsuario);

            if (usuarioUsuario == null)
            {
                return null;
            }
            if (usuarioUsuario.Login != "ADM")
            {
                return null;
            }

            if (usuarioMudanca.LoginUsuario == null)
            {
                return null;
            }
            _usuario.ReplaceOne(usuario => usuario.Id == id, usuarioMudanca);

            Log log = new();
            log.Usuario = usuarioMudanca;
            log.EntidadeAntes = JsonConvert.SerializeObject(encontraUsuario);
            log.EntidadeDepois = "";
            log.Operacao = "remover";
            log.Data = DateTime.Now.Date;

            var verifica = await InsereLog.InsereLogUsuario(log);

            if (verifica == "ok")
            {
                _usuario.InsertOne(encontraUsuario);
            }

            return verifica;
        }
    }
}
