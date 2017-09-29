using Questioning.Commands.Interface;
using Questioning.Services.Interface;

namespace Questioning.Commands
{
    public class CommandNewProfile : BaseCommand
    {
        private readonly IQuestionService questionService;
        public CommandNewProfile(IQuestionService questionService, IConsoleDataReader dataReader)
            :base(dataReader)
        {
            this.questionService = questionService;
        }
        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdNewProfile, Resource.CmdNewProfile_Help);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            QuestionaryContext.CurrentQuestion = 0;
            QuestionaryContext.Questions = questionService.GetNewProfile();
                        
            return CommandMode.Read;
        }
    }
}
