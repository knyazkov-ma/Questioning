using System;

namespace Questioning.Parser
{
    public enum TypeCommandParserException { CommandName, Parameters, Other }
    public class CommandParserException : Exception
    {
        public TypeCommandParserException TypeException { get; set;}
        public string CommandName { get; set; }
        public CommandParserException(string message):base(message)
        {
            TypeException = TypeCommandParserException.CommandName;
        }

        public CommandParserException(string message, TypeCommandParserException typeException) : base(message)
        {
            TypeException = typeException;
        }

        public CommandParserException(string message, TypeCommandParserException typeException, string commandName) : base(message)
        {
            TypeException = typeException;
            CommandName = commandName;
        }
    }
}
