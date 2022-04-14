using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoPassageiroAPI.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using ValidaCEP;
using ValidaCPF;

namespace MicroServicoPassageiroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassageiroController : ControllerBase
    {
        private readonly ServicoPassageiro _passageiro;

        public PassageiroController(ServicoPassageiro clienteService)
        {
            _passageiro = clienteService;
        }

        [HttpGet]
        public ActionResult<List<Passageiro>> Get() =>
            _passageiro.Get();


        [HttpGet("{id:length(24)}", Name = "GetPassageiro")]
        public ActionResult<Passageiro> Get(string idPassageiro)
        {
            var passageiro = _passageiro.Get(idPassageiro);

            if (passageiro == null)
            {
                return NotFound();
            }

            return passageiro;
        }

        [HttpPost]
        public async Task<ActionResult<Passageiro>> Create(Passageiro passageiro)
        {

            if (!Validacao.ValidaCpf(passageiro.Cpf))
            {
                return BadRequest("Cpf invalido! Tente uma nova Requisicao");
            }
            if(passageiro.Cpf == "00000000000" || passageiro.Cpf == "11111111111" || passageiro.Cpf == "22222222222" || passageiro.Cpf == "33333333333" || passageiro.Cpf == "44444444444" || passageiro.Cpf == "55555555555" || passageiro.Cpf == "66666666666" || passageiro.Cpf == "77777777777" || passageiro.Cpf == "88888888888" || passageiro.Cpf == "99999999999")
            {
                return BadRequest("Cpf invalido! Tente uma nova Requisicao");
            }
            
            var endereco = await ApiCorreios.ViacepJsonAsync(passageiro.Endereco.Cep);
            if(endereco.Logradouro == null)
            {
                return BadRequest("Api Fora do ar ou Cep invalido");
            }
            if (endereco != null)
            {
                passageiro.Endereco = new Endereco(endereco.Bairro, endereco.Localidade, endereco.Cep, endereco.Logradouro, endereco.UF, passageiro.Endereco.Pais, passageiro.Endereco.Numero, passageiro.Endereco.Complemento, passageiro.Endereco.Continente);
            }
            if (_passageiro.Create(passageiro) == null)//armazenar em uma variavel; se o retorno for nulo passar o bad requast se nao passar o log
            {
                return BadRequest("Passageiro já Cadastrado! Tente uma nova Requisicao");
            }
            //chamar o micro serv de log 
            return CreatedAtRoute("GetPassageiro", new { id = passageiro.Id.ToString() }, passageiro);
        }

        [HttpPut("{id:length(24)}")]

        public IActionResult Update(string idPassageiro, Passageiro passageiroModificacao)
        {
            var passageiro = _passageiro.Get(idPassageiro);

            if (passageiro == null)
            {
                return NotFound();
            }

            _passageiro.Atualizar(idPassageiro, passageiroModificacao);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string idPassageiro, ReferenciaDeletar deletar)
        {
            var passageiro = _passageiro.Get(idPassageiro);

            if (passageiro == null)
            {
                return NotFound();
            }

            _passageiro.Remover(passageiro.Id, deletar.Passageiro, deletar.Usuario);

            return NoContent();
        }
    }
}
