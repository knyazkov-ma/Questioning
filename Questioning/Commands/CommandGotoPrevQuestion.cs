namespace Questioning.Commands
{
    public class CommandGotoPrevQuestion : BaseCommand
    {
        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdGotoPrevQuestion, Resource.CmdGotoPrevQuestion_Help);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            if (QuestionaryContext.CurrentQuestion !=0)
                QuestionaryContext.CurrentQuestion--;

            return CommandMode.Read;
        }
    }
}
