using Questioning.Commands;
using Questioning.Parser;
using System;

namespace Questioning
{
    public class MainRunner
    {
        private readonly CommandParser commandParser;
        
        public MainRunner(CommandParser commandParser)
        {
            this.commandParser = commandParser;
        }

        private CommandParserResult commandParserResult = null;
        private BaseCommand cmd = null;
        private CommandMode commandMode = CommandMode.General;
        public bool Run(Action<string, string, ConsoleColor> writeErrorLine,
            Action<string> write,
            Action<string> writeLine,
            Func<string> readLine)
        {
            switch (commandMode)
            {
                case CommandMode.General:
                    write(Resource.Cmd_PromptInputCommand);
                    string line = readLine();

                    try
                    {
                        commandParserResult = commandParser.Parse(line);
                        if (commandParserResult.CommandName == Resource.CmdExit)
                            return false;
                    }
                    catch (CommandParserException ex)
                    {
                        if (ex.TypeException == TypeCommandParserException.CommandName)
                            throw ex;

                        if (BaseCommand.ForReader(ex.CommandName))
                        {
                            writeErrorLine(ex.CommandName, Resource.Text_CommandContextError, ConsoleColor.Green);
                            break;
                        }

                    }

                    cmd = AllCommands.Items[commandParserResult.CommandName];

                    if (BaseCommand.ForReader(commandParserResult.CommandName))
                    {
                        writeErrorLine(commandParserResult.CommandName, Resource.Text_CommandContextError, ConsoleColor.Green);
                        break;
                    }

                    commandMode = cmd.Run(commandParserResult.CommandParameters);
                    break;
                case CommandMode.Read:
                    commandMode = cmd.ReadAll(
                    (string s) =>
                    {
                        write(String.Format(" {0}{1} ", s, Resource.Cmd_PromptInputData));
                        return readLine();
                    });
                    break;
                case CommandMode.Write:
                    commandMode = cmd.WiteAll((string s) =>
                    {
                        writeLine(s);
                    });
                    break;
            }

            return true;
        }
    }
}
