using System.Collections.Generic;
using ApiAndreAirlinesMongo.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;

namespace ApiAndreAirlinesMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsereMongoController : ControllerBase
    {
        private readonly ServicoAPIInsereMongo _log;

        public InsereMongoController(ServicoAPIInsereMongo logService)
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
    }
}
