using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoUsuarioAPI.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;

namespace MicroServicoUsuarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ServicoUsuario _servicoUsuario;

        public UserController(ServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> Get() =>
            _servicoUsuario.Get();


        [HttpGet("{id}", Name = "GetUsuario")]
        public ActionResult<Usuario> Get(string id)
        {
            var usuario = _servicoUsuario.Get(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        //[HttpGet("login/{login}")]
        //public async Task<ActionResult<Usuario>> GetLoginUsuario(string loginUser)
        //{
        //    var usuario = await _servicoUsuario.GetLoginUsuario(loginUser);
        //    if (usuario == null)
        //    {
        //        return BadRequest();
        //    }
        //    return usuario;
        //}

        [HttpPost]
        public ActionResult<Usuario> Create(Usuario usuario)
        {
            var usuarioInsercao = _servicoUsuario.Create(usuario);

            if (_servicoUsuario.Create(usuario) == null)
            {
                return BadRequest("Usuario já Cadastrado! Tente uma nova Requisicao");
            }
            return CreatedAtRoute("GetUsuario", new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Usuario usuarioModificacao)
        {
            var usuario = _servicoUsuario.Get(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _servicoUsuario.Atualizar(id, usuarioModificacao);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var usuario = _servicoUsuario.Get(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _servicoUsuario.Remover(usuario.Id);

            return NoContent();
        }
    }
}
