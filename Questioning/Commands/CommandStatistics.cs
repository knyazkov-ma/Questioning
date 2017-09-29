using Questioning.DTO;
using System;
using System.Collections.Generic;
using Questioning.Helpers;
using Questioning.Services.Interface;

namespace Questioning.Commands
{
    public class CommandStatistics : BaseCommand
    {
        private readonly IQuestionService questionService;
        public CommandStatistics(IQuestionService questionService)
        {
            this.questionService = questionService;
        }
        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdStatistics, Resource.CmdStatistics_Help);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            StatisticsDTO s = questionService.GetStatistics();
            if (s == null)
                return CommandMode.General;


            IList<string> outputLines = new List<string>();

            outputLines.Add(String.Format(" {0}: {1} {2}", Resource.Label_AverageAge, s.AverageAge, s.AverageAge.GetYearUnitName()));
            outputLines.Add(String.Format(" {0}: {1}", Resource.Label_TopProgramLanguage, s.TopProgramLanguage));

            this.outputLines = outputLines;

            return CommandMode.Write;
        }
    }
}
