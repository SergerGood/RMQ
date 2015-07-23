using System;


namespace RMQ.Contract.Commands
{
    public sealed class SendMessageCommand
    {
        public string Message { get; set; }
    }
}
