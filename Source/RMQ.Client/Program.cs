using System;

using Nelibur.ServiceModel.Clients;

using RMQ.Client.Properties;
using RMQ.Contract.Commands;


namespace RMQ.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new JsonServiceClient(Settings.Default.ServiceAddress);

            Console.WriteLine("-> Waiting for send messages. To exit press CTRL+C");

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                client.Post(new SendMessageCommand { Message = line });
            }
        }
    }
}
