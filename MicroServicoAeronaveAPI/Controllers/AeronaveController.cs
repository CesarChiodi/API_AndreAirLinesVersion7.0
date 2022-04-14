using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoAeronaveAPI.Servico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;

namespace MicroServicoAeronaveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeronaveController : ControllerBase
    {
        private readonly ServicoAeronave _aeronave;

        public AeronaveController(ServicoAeronave aeronaveService)
        {
            _aeronave = aeronaveService;
        }

        [HttpGet]
        public ActionResult<List<Aeronave>> Get() =>
            _aeronave.Get();


        [HttpGet("{id:length(24)}", Name = "GetAeronave")]
        public ActionResult<Aeronave> Get(string idAeronave)
        {
            var aeronave = _aeronave.Get(idAeronave);

            if (aeronave == null)
            {
                return NotFound();
            }

            return aeronave;
        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Usuario model)
        {
            // Recupera o usuário
            var usuario = await ServicoVerificaUsuarioAeronave.BuscaUsuarioAeronave(model.Login);

            // Verifica se o usuário existe
            if (usuario == null)
                return NotFound(new { message = "Usuário inválido" });

            else if (usuario.Senha != model.Senha)
                return NotFound(new { message = "Senha inválida" });

            // Gera o Token
            var token = ServicoToken1.GenerateToken(usuario);

            // Oculta a senha
            usuario.Senha = "";

            // Retorna os dados
            return new
            {
                usuario = usuario,
                token = token
            };
        }


        [HttpPost]
        public async Task <ActionResult<Aeronave>> Create(Aeronave aeronave)
        {

            if (aeronave.NomeAeronave == null)
            {
                return BadRequest("Api Fora do ar ");
            }
            if (_aeronave.Create(aeronave) == null)
            {
                return BadRequest("Aeronave já Cadastrada! Tente uma nova Requisicao");
            } 
            return CreatedAtRoute("GetAeronave", new { id = aeronave.IdAeronave }, aeronave);
        }

        [HttpPut("{id:length(24)}")]

        public IActionResult Update(string idAeronave, Aeronave aeronaveModificacao)
        {
            var aeronave = _aeronave.Get(idAeronave);

            if (aeronave == null)
            {
                return NotFound();
            }

            _aeronave.Atualizar(idAeronave, aeronaveModificacao);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string idAeronave, ReferenciaDeletar deletar)
        {
            var aeronave = _aeronave.Get(idAeronave);

            if (aeronave == null)
            {
                return NotFound();
            }

            _aeronave.Remover(aeronave.IdAeronave, deletar.Aeronave, deletar.Usuario);

            return NoContent();
        }
    }
}
