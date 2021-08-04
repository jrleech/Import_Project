using System;

using RabbitMQ.Client;

namespace RabbitMQConsumer

{
    class Program

    {

        private const string UserName = "guest";

        private const string Password = "guest";

        private const string HostName = "localhost";

        static void Main(string[] args)

        {

            ConnectionFactory connectionFactory = new ConnectionFactory

            {

                HostName = HostName,

                UserName = UserName,

                Password = Password,

            };

            var connection = connectionFactory.CreateConnection();

            var channel = connection.CreateModel();
 
            channel.BasicQos(0, 1, false);

            MessageReceiver messageReceiver = new MessageReceiver(channel);

            channel.BasicConsume("demoqueue", false, messageReceiver);

            Console.ReadLine();

        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}

