using System;
using System.ServiceModel.Web;

using Nelibur.ServiceModel.Services;
using Nelibur.ServiceModel.Services.Default;

using RMQ.Contract.Commands;
using RMQ.Service.Processors;


namespace RMQ.Service
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            NeliburRestService.Configure(x => x.Bind<SendMessageCommand, MessageProcessor>());

            var service = new WebServiceHost(typeof(JsonServicePerCall));
            service.Open();

            Console.WriteLine("-> Waiting for messages. To exit press CTRL+C");

            string line;
            while ((line = Console.ReadLine()) != null)
            {

            }
        }
    }
}
