using Questioning.Services.Interface;

namespace Questioning.Commands
{
    public class CommandRestartProfile : BaseCommand
    {
        private readonly IQuestionService questionService;
        public CommandRestartProfile(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdRestartProfile, Resource.CmdRestartProfile_Help);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            return AllCommands.Items[Resource.CmdNewProfile].Run();
        }
    }
}
