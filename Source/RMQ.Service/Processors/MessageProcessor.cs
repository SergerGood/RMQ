using Nelibur.ServiceModel.Services.Operations;

using RabbitMQ.Client;

using RMQ.Contract.Commands;
using RMQ.Core;
using RMQ.Core.Extensions;


namespace RMQ.Service.Processors
{
    internal sealed class MessageProcessor : IPostOneWay<SendMessageCommand>
    {
        public void PostOneWay(SendMessageCommand request)
        {
            using (var queue = new DurableQueue("localhost", "hello"))
            {
                IModel channel = queue.GetChannel();
                var message = new Message(request.Message);

                channel.SendPersistentMessageToQueue("hello", message);
            }
        }
    }
}
