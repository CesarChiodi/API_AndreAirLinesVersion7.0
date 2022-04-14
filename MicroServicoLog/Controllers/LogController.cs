using System.Collections.Generic;
using System.Text;
using MicroServicoLog.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MicroServicoLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "mensagem micro servico";

        public LogController()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }

        [HttpPost]
        public IActionResult PostMessage([FromBody] Log message)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    var stringfieldMessage = JsonConvert.SerializeObject(message);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesMessage
                        );
                }
            }
            return Accepted();
        }







        //[HttpPut("{id:length(24)}")]

        //public IActionResult Update(string idLog, Log logModificacao)
        //{
        //    var log = _log.Get(idLog);

        //    if (log == null)
        //    {
        //        return NotFound();
        //    }

        //    _log.Atualizar(idLog, logModificacao);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public IActionResult Delete(string idLog)
        //{
        //    var log = _log.Get(idLog);

        //    if (log == null)
        //    {
        //        return NotFound();
        //    }

        //    _log.Remover(log.IdLog);

        //    return NoContent();
        //}

    }
}
