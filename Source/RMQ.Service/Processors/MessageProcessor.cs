using System;

using Nelibur.ServiceModel.Services.Operations;

using RMQ.Contract.Commands;


namespace RMQ.Service.Processors
{
    internal sealed class MessageProcessor : IPostOneWay<SendMessageCommand>
    {
        public void PostOneWay(SendMessageCommand request)
        {
            Console.WriteLine(request.Message);
        }
    }
}
