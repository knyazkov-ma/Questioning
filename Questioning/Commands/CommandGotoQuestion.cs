namespace Questioning.Commands
{
    public class CommandGotoQuestion : BaseCommand
    {
        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdGotoQuestion, Resource.CmdGotoQuestion_Help, Resource.CmdGotoQuestion_Format);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            int n = (int)commandParameters[0];
            QuestionaryContext.CurrentQuestion = n;
            return CommandMode.Read;
        }
    }
}
