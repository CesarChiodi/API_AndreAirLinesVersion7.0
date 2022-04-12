using System.Collections.Generic;
using System.Threading.Tasks;
using CorreiosAPI;
using MicroServicoUsuarioAPI.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using ValidaCPF;

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


        [HttpGet("{login}", Name = "GetUsuario")]
        public ActionResult<Usuario> Get(string login)
        {
            var usuario = _servicoUsuario.Get(login);

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
        public async Task <ActionResult<Usuario>> Create(Usuario usuario)
        {
            //
            if (!Validacao.ValidaCpf(usuario.Cpf))
            {
                return BadRequest("Cpf invalido! Tente uma nova Requisicao");
            }
            if (usuario.Cpf == "00000000000" || usuario.Cpf == "11111111111" || usuario.Cpf == "22222222222" || usuario.Cpf == "33333333333" || usuario.Cpf == "44444444444" || usuario.Cpf == "55555555555" || usuario.Cpf == "66666666666" || usuario.Cpf == "77777777777" || usuario.Cpf == "88888888888" || usuario.Cpf == "99999999999")
            {
                return BadRequest("Cpf invalido! Tente uma nova Requisicao");
            }

            var endereco = await ApiCorreios.ViacepJsonAsync(usuario.Endereco.Cep);
            if (endereco.Logradouro == null)
            {
                return BadRequest("Api Fora do ar ou Cep invalido");
            }
            if (endereco != null)
            {
                usuario.Endereco = new Endereco(endereco.Bairro, endereco.Localidade, endereco.Cep, endereco.Logradouro, endereco.UF, usuario.Endereco.Pais, usuario.Endereco.Numero, usuario.Endereco.Complemento, usuario.Endereco.Continente);
            }
            if (_servicoUsuario.Create(usuario) == null)
            {
                return BadRequest("Passageiro já Cadastrado! Tente uma nova Requisicao");
            }
            //
            return CreatedAtRoute("GetUsuario", new { login = usuario.Login }, usuario);
        }

        [HttpPut("{login}")]
        public IActionResult Update(string login, Usuario usuarioModificacao)
        {
            var usuario = _servicoUsuario.Get(login);

            if (usuario == null)
            {
                return NotFound();
            }

            _servicoUsuario.Atualizar(login, usuarioModificacao);

            return NoContent();
        }

        [HttpDelete("{login}")]
        public IActionResult Delete(string login)
        {
            var usuario = _servicoUsuario.Get(login);

            if (usuario == null)
            {
                return NotFound();
            }

            _servicoUsuario.Remover(usuario.Login);

            return NoContent();
        }
    }
}
