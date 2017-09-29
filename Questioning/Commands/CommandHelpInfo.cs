namespace Questioning.Commands
{
    public class CommandHelpInfo
    {
        public string CommandName { get; private set; }
        public string Description { get; private set; }
        public string Format { get; private set; }

        public CommandHelpInfo(string commandName, string description)
        {
            CommandName = commandName;
            Description = description;
        }

        public CommandHelpInfo(string commandName, string description, string format):
            this(commandName, description)
        {
            Format = format;
        }

    }
}
