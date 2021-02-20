using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMQ.Client
{
    public static class ConnectionBuilder
    {
        public static ConnectionFactory ConnectionFactory => new ConnectionFactory() { HostName = "localhost" };
    }
}
