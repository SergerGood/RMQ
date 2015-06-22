using System;


namespace RMQ.Core
{
    [Serializable]
    public class Message
    {
        public Message()
        {
        }


        public Message(string body)
        {
            Body = body;
        }


        public string Body { get; set; }


        public override string ToString()
        {
            return string.Format("message '{0}'", Body);
        }
    }
}
