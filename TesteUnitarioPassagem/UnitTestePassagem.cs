using System;
using System.Collections.Generic;
using System.Linq;
using MicroServicoPassagemAereaAPI.Configuracao;
using MicroServicoPassagemAereaAPI.Servico;
using Modelo;
using Xunit;

namespace TesteUnitarioPassagem
{
    public class UnitTestePassagem
    {
        private ServicoPassagem _passagem;
        public ServicoPassagem InitializeDataBase()
        {
            var configuracao = new PassagemAPI();
            ServicoPassagem servicoPassagem = new(configuracao);
            return servicoPassagem;
        }

        [Fact]
        public void GetAll()
        {
            _passagem = InitializeDataBase();
           IEnumerable<PassagemAerea> passsagem = _passagem.Get();
            Assert.Equal(3, passsagem.Count());
        }

        [Fact]
        public void GetId()
        {
            _passagem = InitializeDataBase();
            PassagemAerea passsagem = _passagem.Get("6258082a8b81c01bffe7d99e");
            Assert.NotNull(passsagem);
        }

        [Fact]
        public void Create()
        {
            _passagem = InitializeDataBase();
            PassagemAerea passsagem = new();
            Assert.NotNull(_passagem.Create(passsagem));
        }

        [Fact]
        public void Delete()
        {

            _passagem = InitializeDataBase();
            var passagem = _passagem.Get("6258082a8b81c01bffe7d99e");

            _passagem.Remover(passagem);

            passagem = _passagem.Get("6258082a8b81c01bffe7d99e");

            Assert.Null(passagem);
        }

        [Fact]
        public void Update()
        {
            _passagem = InitializeDataBase();
            PassagemAerea passsagem = new();
            Assert.NotNull(_passagem.Atualizar("6258082a8b81c01bffe7d99e", passsagem));
        }
    }
}
