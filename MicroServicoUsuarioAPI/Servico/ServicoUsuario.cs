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

        public Usuario Get(string id) =>
            _usuario.Find<Usuario>(usuario => usuario.Id == id).FirstOrDefault();


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

        public Usuario Create(Usuario usuario)
        {
            var encontraPassageiro = BuscaCpf(usuario.Cpf);
            if (encontraPassageiro == null)
            {
                _usuario.InsertOne(usuario);
                return usuario;
            }
            return null;
        }

        /*public async Task<Usuario> Create(Usuario usuario)
        {
            var usuarioLogin = GetLoginUsuario(usuario.LoginUsuario);

            if (usuarioLogin.Setor != "ADM")
            {
                return usuario;
            }

            var searchUser = BuscaCpf(usuario.Cpf);

            if (searchUser != null)
            {
                return usuario;
            }

            _usuario.InsertOne(usuario);

            Log log = new();

            log.Usuario = usuario;
            log.EntidadeAntes = "";
            log.EntidadeDepois = JsonConvert.SerializeObject(usuario);
            log.Operacao = "create";
            log.Data = DateTime.Now.Date;

            var returnMsg = InsereLog.InsertLog(log);

            return usuario;
        }*/

        public void Atualizar(string id, Usuario usuarioMudanca) =>
            _usuario.ReplaceOne(usuario => usuario.Id == id, usuarioMudanca);

        public void Remover(Usuario usuarioMudanca) =>
            _usuario.DeleteOne(usuario => usuario.Id == usuarioMudanca.Id);

        public void Remover(string id) =>
            _usuario.DeleteOne(usuario => usuario.Id == id);
    }
}
