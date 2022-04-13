using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicoAeroportoAPI.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using ValidaCEP;

namespace MicroServicoAeroportoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeroportoController : ControllerBase
    {

        private readonly ServicoAeroporto _aeroporto;

        public AeroportoController(ServicoAeroporto aeroportoService)
        {
            _aeroporto = aeroportoService;
        }

        [HttpGet]
        public ActionResult<List<Aeroporto>> Get() =>
            _aeroporto.Get();


        [HttpGet("{id:length(24)}", Name = "GetAeroporto")]
        public ActionResult<Aeroporto> Get(string idAeroporto)
        {
            var aeroporto = _aeroporto.GetLogin(idAeroporto);

            if (aeroporto == null)
            {
                return NotFound();
            }
            //else if(aeroporto.Id != null)
            //{
            //    return BadRequest("");          //fazendo isso
            //}
            return aeroporto;
        }

        [HttpPost]
        public async Task <ActionResult<Aeroporto>> Create(Aeroporto aeroporto, string iata)
        {
            var endereco = await ApiCorreios.ViacepJsonAsync(aeroporto.EnderecoAeroporto.Cep);
            if (endereco.Logradouro != null)
            {
                return BadRequest("Api Fora do ar, ou Cep invalido, ou usuario invalido");
            }
            if (endereco != null)
            {
                aeroporto.EnderecoAeroporto = new Endereco(endereco.Bairro, endereco.Localidade,endereco.Cep, endereco.Logradouro, endereco.UF, aeroporto.EnderecoAeroporto.Pais, aeroporto.EnderecoAeroporto.Numero, aeroporto.EnderecoAeroporto.Complemento, aeroporto.EnderecoAeroporto.Continente);
            }

            if(_aeroporto.Create(aeroporto) == null)
            {
                return BadRequest("Aeroporto já Cadastrado! Tente uma nova Requisicao");
            }

            return CreatedAtRoute("GetAeroporto", new { id = aeroporto.IdAeroporto.ToString() }, aeroporto);
        }

        [HttpPut("{id:length(24)}")]

        public IActionResult Update(string idAeroporto, Aeroporto aeroportoModificacao)
        {
            var aeroporto = _aeroporto.GetLogin(idAeroporto);

            if (aeroporto == null)
            {
                return NotFound();
            }

            _aeroporto.Atualizar(idAeroporto, aeroportoModificacao);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string idAeroporto, Aeroporto aeroportoModificacao, Usuario usuario)
        {
            var aeroporto = _aeroporto.GetLogin(idAeroporto);

            if (aeroporto == null)
            {
                return NotFound();
            }

            _aeroporto.Remover(aeroporto.IdAeroporto, aeroportoModificacao, usuario);

            return NoContent();
        }
    }
}
