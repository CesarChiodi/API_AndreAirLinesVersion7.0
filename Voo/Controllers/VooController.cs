using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoVooAPI.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;

namespace MicroServicoVooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VooController : ControllerBase
    {
        private readonly ServicoVoo _voo;

        public VooController(ServicoVoo vooService)
        {
            _voo = vooService;
        }

        [HttpGet]
        public ActionResult<List<Voo>> Get() =>
            _voo.Get();


        [HttpGet("{id:length(24)}", Name = "GetVoo")]
        public ActionResult<Voo> Get(string idVoo)
        {
            var voo = _voo.Get(idVoo);

            if (voo == null)
            {
                return NotFound();
            }

            return voo;
        }

        [HttpPost]
        public async Task<ActionResult<Voo>> Create(Voo voo)
        {

            if (await _voo.Create(voo) != null)
            {
                return CreatedAtRoute("GetVoo", new { id = voo.Id.ToString() }, voo);

            }
            else
            {
                return BadRequest("Erro ao inserir voo, ou usuario invalido");
            }

        }

        [HttpPut("{id:length(24)}")]

        public IActionResult Update(string idVoo, Voo vooModificacao)
        {
            var voo = _voo.Get(idVoo);

            if (voo == null)
            {
                return NotFound();
            }

            _voo.Atualizar(idVoo, vooModificacao);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string idPrecoBase, Voo vooModificacao, Usuario usuario)
        {
            var voo = _voo.Get(idPrecoBase);

            if (voo == null)
            {
                return NotFound();
            }

            _voo.Remover(voo.Id, vooModificacao, usuario);

            return NoContent();

        }
    }
}