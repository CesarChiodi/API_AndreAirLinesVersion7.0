using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoPassagemAereaAPI.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;

namespace MicroServicoPassagemAereaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassagemController : ControllerBase
    {
        private readonly ServicoPassagem _passagem;

        public PassagemController(ServicoPassagem passagemService)
        {
            _passagem = passagemService;
        }

        [HttpGet]
        public ActionResult<List<PassagemAerea>> Get() =>
            _passagem.Get();


        [HttpGet("{id:length(24)}", Name = "GetPassagemAerea")]
        public ActionResult<PassagemAerea> Get(string idPassagem)
        {
            var passagem = _passagem.Get(idPassagem);

            if (passagem == null)
            {
                return NotFound();
            }

            return passagem;
        }

        [HttpPost]
        public async Task<ActionResult<PassagemAerea>> Create(PassagemAerea passagem)
        {
            if (await _passagem.Create(passagem) != null)
            {
                return CreatedAtRoute("GetPassagemAerea", new { id = passagem.Id.ToString() }, passagem);

            }
            else
            {
                return BadRequest("Erro ao inserir passagem aerea, ou usuario invalido");
            }
        }

        [HttpPut("{id:length(24)}")]

        public IActionResult Update(string idPassagem, PassagemAerea passagemModificacao)
        {
            var passagem = _passagem.Get(idPassagem);

            if (passagem == null)
            {
                return NotFound();
            }

            _passagem.Atualizar(idPassagem, passagemModificacao);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string idPassagem, ReferenciaDeletar deletar)
        {
            var passagem = _passagem.Get(idPassagem);

            if (passagem == null)
            {
                return NotFound();
            }

            _passagem.Remover(passagem.Id, deletar.PassagemAerea, deletar.Usuario);

            return NoContent();
        }
    }
}
