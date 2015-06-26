using System;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace RMQ.Core.Extensions
{
    public static class ChannelExtension
    {
        public static void BindDurableQueue(this IModel channel, string queueName, string exchangeName)
        {
            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queueName, exchangeName, "");
        }


        public static QueueingBasicConsumer CreateConsumerWithAck(this IModel channel, string queueName)
        {
            var consumer = new QueueingBasicConsumer(channel);

            channel.BasicConsume(queueName, false, consumer);

            return consumer;
        }


        public static Message ReceiveNextMessageWithAck(this IModel channel, QueueingBasicConsumer consumer)
        {
            BasicDeliverEventArgs deliverEventArgs = consumer.Queue.Dequeue();

            channel.BasicAck(deliverEventArgs.DeliveryTag, false);

            string xmlString = deliverEventArgs.Body.GetText();
            Message message = MessageSerializer.FromXml(xmlString);

            return message;
        }


        public static void SendPersistentMessageToExchange(this IModel channel, string exchangeName, Message message)
        {
            IBasicProperties properties = channel.CreatePersistentProperties();

            string xmlString = MessageSerializer.ToXml(message);
            byte[] body = xmlString.GetBody();

            channel.BasicPublish(exchangeName, "", properties, body);
        }


        public static void SendPersistentMessageToQueue(this IModel channel, string queueName, Message message)
        {
            IBasicProperties properties = channel.CreatePersistentProperties();

            string xmlString = MessageSerializer.ToXml(message);
            byte[] body = xmlString.GetBody();

            channel.BasicPublish("", queueName, properties, body);
        }


        private static IBasicProperties CreatePersistentProperties(this IModel channel)
        {
            IBasicProperties properties = channel.CreateBasicProperties();
            properties.SetPersistent(true);

            return properties;
        }
    }
}
