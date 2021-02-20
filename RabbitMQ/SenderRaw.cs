using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMQ.Client
{
    public static class SenderRaw
    {
        public static void Publish()
        {
            Console.WriteLine("Write message: ");
            string message = Console.ReadLine();

            using (var connection = ConnectionBuilder.ConnectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                             "first_q",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "first_q", basicProperties: null, body: body);

                Console.WriteLine("Message Sent");
            }
        }
    }
}
