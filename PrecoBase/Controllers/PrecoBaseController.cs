using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoPrecoBaseAPI.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;

namespace MicroServicoPrecoBaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecoBaseController : ControllerBase
    {
        private readonly ServicoPrecoBase _precoBase;

        public PrecoBaseController(ServicoPrecoBase precoBaseService)
        {
            _precoBase = precoBaseService;
        }

        [HttpGet]
        public ActionResult<List<PrecoBase>> Get() =>
            _precoBase.Get();


        [HttpGet("{id:length(24)}", Name = "GetPrecoBase")]
        public ActionResult<PrecoBase> Get(string idPrecoBase)
        {
            var precoBase = _precoBase.Get(idPrecoBase);

            if (precoBase == null)
            {
                return NotFound();
            }

            return precoBase;
        }

        [HttpPost]
        public async Task<ActionResult<PrecoBase>> Create(PrecoBase precoBase)
        {
            if (await _precoBase.Create(precoBase) != null)
            {
                return CreatedAtRoute("GetPrecoBase", new { id = precoBase.Id.ToString() }, precoBase);

            }
            else
            {
                return BadRequest("Erro ao inserir preco base, ou usuario invalido");
            }

        }

        [HttpPut("{id:length(24)}")]

        public IActionResult Update(string idPrecoBase, PrecoBase precoBaseModificacao)
        {
            var precoBase = _precoBase.Get(idPrecoBase);

            if (precoBase == null)
            {
                return NotFound();
            }

            _precoBase.Atualizar(idPrecoBase, precoBaseModificacao);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string idPrecoBase, PrecoBase precoBaseModificacao, Usuario usuario)
        {
            var precoBase = _precoBase.Get(idPrecoBase);

            if (precoBase == null)
            {
                return NotFound();
            }

            _precoBase.Remover(precoBase.Id, precoBaseModificacao, usuario);

            return NoContent();
        }
    }
}
