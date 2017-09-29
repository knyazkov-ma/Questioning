using Questioning.Commands.Interface;
using System;
using System.Collections.Generic;

namespace Questioning.Commands
{
    public abstract class BaseCommand
    {
        public static bool ForReader(string commandName)
        {
            return (commandName == Resource.CmdGotoQuestion ||
                    commandName == Resource.CmdGotoPrevQuestion ||
                    commandName == Resource.CmdRestartProfile);
        }

        public BaseCommand()
        {

        }

        protected readonly IConsoleDataReader dataReader;
        public BaseCommand(IConsoleDataReader dataReader)
        {
            this.dataReader = dataReader;
        }

        
        public CommandMode ReadAll(Func<string, string> inputTextDelegate)
        {
            CommandMode commandMode;
            while (true)
            {
                commandMode = dataReader.ReadString(inputTextDelegate);
                if (commandMode == CommandMode.General)
                    return commandMode;
            }

        }

        protected IEnumerable<string> outputLines;
        public CommandMode WiteAll(Action<string> outputTextDelegate)
        {
            foreach (var s in outputLines)
                outputTextDelegate(s);

            outputLines = null;

            return CommandMode.General;
        }

        abstract public CommandHelpInfo GetCommandHelp();

        abstract public CommandMode Run(object[] commandParameters = null);
    }
}
