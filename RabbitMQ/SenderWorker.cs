using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMQ.Client
{
    public static class SenderWorker
    {
        public static void Publish(string[] args)
        {
            using(var connection = ConnectionBuilder.ConnectionFactory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "worker_q",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    null
                    );

                var prop = channel.CreateBasicProperties();
                prop.Persistent = true;

                string msg = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(msg);

                channel.BasicPublish(exchange: "", routingKey: "worker_q", basicProperties: prop, body: body);

                Console.WriteLine("Sent {0}", msg);
            }
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
