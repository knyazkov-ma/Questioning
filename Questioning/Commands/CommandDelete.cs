using Questioning.Services.Interface;

namespace Questioning.Commands
{
    public class CommandDelete : BaseCommand
    {
        private readonly IQuestionService questionService;
        public CommandDelete(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdDelete, Resource.CmdDelete_Help, Resource.CmdDelete_Format);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
             questionService.DeleteProfile(commandParameters[0].ToString());
            return CommandMode.General;
        }
    }
}
