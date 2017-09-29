using System;

namespace Questioning.Commands
{
    public class CommandException: Exception
    {
        public CommandException(string message):base(message)
        {
        }
                
    }
}
