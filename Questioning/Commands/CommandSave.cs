using Questioning.Services.Interface;

namespace Questioning.Commands
{
    public class CommandSave : BaseCommand
    {
        private readonly IQuestionService questionService;
        public CommandSave(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdSave, Resource.CmdSave_Help);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            if (QuestionaryContext.Questions == null)
                return CommandMode.General;

            QuestionaryContext.CurrentQuestion = 0;
            questionService.SaveProfile(QuestionaryContext.Questions);
            QuestionaryContext.Questions = null;
            return CommandMode.General;
        }
    }
}
