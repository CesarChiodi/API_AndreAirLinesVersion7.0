using System;
using System.Text;
using System.Threading;
using InterceptaMensagens.Servico;
using Modelo;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace InterceptaMensagens
{
    internal class Program
    {

        private const string QUEUE_NAME = "mensagem micro servico";

        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QUEUE_NAME,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

                    while (true)
                    {
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += async (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var returnMessage = Encoding.UTF8.GetString(body);
                            var message = JsonConvert.DeserializeObject<Log>(returnMessage);
                            await EnviaMongoServico.Add(message);
                        };

                        channel.BasicConsume(queue: QUEUE_NAME,
                                             autoAck: true,
                                             consumer: consumer);

                        Thread.Sleep(2000);
                    }
                }
            }
        }
    }
}
