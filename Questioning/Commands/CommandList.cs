using Questioning.Services.Interface;
using System;
using System.Collections.Generic;

namespace Questioning.Commands
{
    public class CommandList : BaseCommand
    {
        private readonly IQuestionService questionService;
        public CommandList(IQuestionService questionService)
        {
            this.questionService = questionService;
        }
        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdList, Resource.CmdList_Help);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            IEnumerable<string> profiles = questionService.GetAllProfileNames();
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
