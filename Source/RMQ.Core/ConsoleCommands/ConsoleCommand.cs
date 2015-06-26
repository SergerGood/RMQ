using System;


namespace RMQ.Core.ConsoleCommands
{
    public class ConsoleCommand
    {
        public Action Action { get; set; }

        public string Argument { get; set; }

        public bool Default { get; set; }
    }
}
