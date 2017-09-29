namespace Questioning.Parser
{
    public class CommandParserResult
    {
        public string CommandName { get; }
        public object[] CommandParameters { get; }

        public CommandParserResult(string commandName, object[] commandParameters)
        {
            CommandName = commandName;
            CommandParameters = commandParameters;
        }
    }
}
