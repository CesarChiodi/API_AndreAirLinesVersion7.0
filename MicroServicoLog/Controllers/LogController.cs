using System.Collections.Generic;
using MicroServicoLog.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;

namespace MicroServicoLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ServicoLog _log;

        public LogController(ServicoLog logService)
        {
            _log = logService;
        }

        [HttpGet]
        public ActionResult<List<Log>> Get() =>
            _log.Get();


        [HttpGet("{id:length(24)}", Name = "GetLog")]
        public ActionResult<Log> Get(string idLog)
        {
            var log = _log.Get(idLog);

            if (log == null)
            {
                return NotFound();
            }

            return log;
        }

        [HttpPost]
        public ActionResult<Log> Create(Log log)
        {
            //if (log.NomeLog == null)
            //{
            //    return BadRequest("Api Fora do ar ");
            //}
            if (_log.Create(log) == null)
            {
                return BadRequest("Log já Cadastrada! Tente uma nova Requisicao");
            }
            return CreatedAtRoute("GetLog", new { id = log.IdLog }, log);
        }

        [HttpPut("{id:length(24)}")]

        public IActionResult Update(string idLog, Log logModificacao)
        {
            var log = _log.Get(idLog);

            if (log == null)
            {
                return NotFound();
            }

            _log.Atualizar(idLog, logModificacao);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string idLog)
        {
            var log = _log.Get(idLog);

            if (log == null)
            {
                return NotFound();
            }

            _log.Remover(log.IdLog);

            return NoContent();
        }
    }
}
