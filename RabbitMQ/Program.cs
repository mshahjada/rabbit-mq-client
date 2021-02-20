using RabbitMQ.Client;
using System;
using System.Text;

namespace RMQ.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //SenderWorker.Publish(args);

            //SenderExchange.Publish(args);

            //SenderRouting.Publish(args);

            var rpcClient = new RpcClient();
            var data = rpcClient.Publish(args);
            Console.WriteLine("Is {0} palindrome? => {1}", string.Join("", args), data);
            rpcClient.Close();
        }

        //public void BuildConnection()
        //{
        //    using(var bus= RabbitHutch.CreateBus("host=localhost;username=guest;password=guest"))
        //    {
        //        bus.PubSub.PublishAsync("Hello World");
        //    }
        //}

        //public void HandleMessage(IBus bus)
        //{

        //}
    }
}
