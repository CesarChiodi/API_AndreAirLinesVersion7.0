using System;
using System.Collections.Generic;
using System.Linq;
using MicroServicoUsuarioAPI.Configuracao;
using MicroServicoUsuarioAPI.Servico;
using Modelo;
using Xunit;

namespace TesteUnitarioUsuario
{
    public class UnitTest1
    {
        private ServicoUsuario _usuario;
        public ServicoUsuario InitializeDataBase()
        {
            var configuracao = new UsuarioAPI();
            ServicoUsuario servicoUsuario = new(configuracao);
            return servicoUsuario;
        }

        [Fact]
        public void GetAll()
        {
            _usuario = InitializeDataBase();
            IEnumerable<Usuario> usuario = _usuario.Get();
            Assert.Equal(3, usuario.Count());
        }

        [Fact]
        public void GetId()
        {
            _usuario = InitializeDataBase();
            Usuario usuario = _usuario.GetLogin("ADM");
            Assert.NotNull(usuario);
        }

        [Fact]
        public void Create()
        {
            _usuario = InitializeDataBase();
            Usuario usuario = new();
            Assert.NotNull(_usuario.Create(usuario));
        }

        [Fact]
        public void Delete()
        {

            
        }

        [Fact]
        public void Update()
        {
            _usuario = InitializeDataBase();
            Usuario usuario = new();
            Assert.NotNull(_usuario.Atualizar("6258082a8b81c01bffe7d99e", usuario));
        }
    }
}
