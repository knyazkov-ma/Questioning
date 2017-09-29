using Questioning.Services.Interface;

namespace Questioning.Commands
{
    public class CommandZip : BaseCommand
    {
        private readonly IQuestionService questionService;
        public CommandZip(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdZip, Resource.CmdZip_Help, Resource.CmdZip_Format);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            questionService.ZipProfile(commandParameters[0].ToString(), commandParameters[1].ToString());
            return CommandMode.General;
        }
    }
}
