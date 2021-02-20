using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMQ.Client
{
    public static class SenderExchange
    {
        public static void Publish(string[] args)
        {
            using (var connection = ConnectionBuilder.ConnectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("log", type: ExchangeType.Fanout);

                var msg = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(msg);

                channel.BasicPublish("log", "", null, body);

                Console.WriteLine("Msg sent: {0}", msg);
            }
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
