using System;
using System.Collections.Generic;
using System.Linq;


namespace RMQ.Core.ConsoleCommands
{
    public sealed class CommandExecutor
    {
        private readonly string[] args;
        private readonly IEnumerable<ConsoleCommand> commands;


        public CommandExecutor(IEnumerable<ConsoleCommand> commands, string[] args)
        {
            this.commands = commands;
            this.args = args;
        }


        public void Process()
        {
            RunCommand(c => args.Contains(c.Argument));
            RunCommand(c => c.Default);

            Console.WriteLine("Допустимые команды {0}", string.Join(" ", commands.Select(c => c.Argument)));
        }


        private void RunCommand(Func<ConsoleCommand, bool> predicate)
        {
            ConsoleCommand command = commands.FirstOrDefault(predicate);
            if (command == null || command.Action == null)
            {
                return;
            }

            command.Action();
        }
    }
}
