using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace RMQ.Client
{
    public class RpcClient
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly IBasicProperties prop;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> resQueue = new BlockingCollection<string>();
        private readonly string replyQ;

        public RpcClient()
        {
            connection = ConnectionBuilder.ConnectionFactory.CreateConnection();
            channel = connection.CreateModel();

            replyQ = channel.QueueDeclare().QueueName;

            prop = channel.CreateBasicProperties();
            prop.CorrelationId = new Guid().ToString();
            prop.ReplyTo = replyQ;

            consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
             {
                 if (ea.BasicProperties.CorrelationId == prop.CorrelationId)
                     resQueue.Add(Encoding.UTF8.GetString(ea.Body.ToArray()));
             };
        }

        public string Publish(string[] args)
        {

            channel.BasicPublish(
                "", 
                "rpc_q", 
                false, 
                prop, 
                Encoding.UTF8.GetBytes(GetMessage(args)));

            channel.BasicConsume(replyQ, true, consumer);

            return resQueue.Take();
        }

        public void Close()
        {
            connection.Close();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World");
        }
    }
}
