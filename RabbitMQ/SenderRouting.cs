using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMQ.Client
{
    public static class SenderRouting
    {
        public static void Publish(string[] args)
        {
            using (var connection = ConnectionBuilder.ConnectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("log.direct", type: ExchangeType.Direct);

                var prop = channel.CreateBasicProperties();
                prop.Persistent = true;

                var routingKey = args.Length > 0 ? args[0] : "info";

                var msg = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(msg);

                channel.BasicPublish("log.direct", routingKey, prop, body);

                Console.WriteLine("Msg sent: {0}", msg);
            }
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 1) ? string.Join(" ", args.Skip(1)) : "Test Default");
        }
    }
}
