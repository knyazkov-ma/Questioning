using Questioning.Services.Interface;
using System;
using System.Collections.Generic;

namespace Questioning.Commands
{
    public class CommandListToday : BaseCommand
    {
        private readonly IQuestionService questionService;
        public CommandListToday(IQuestionService questionService)
        {
            this.questionService = questionService;
        }
        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdListToday, Resource.CmdListToday_Help);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            IEnumerable<string> profiles = questionService.GetAllTodayProfileNames();
            IList<string> outputLines = new List<string>();
            foreach (var p in profiles)
            {
                outputLines.Add(String.Format(" {0}", p));
            }
            this.outputLines = outputLines;

            return CommandMode.Write;
        }
    }
}
