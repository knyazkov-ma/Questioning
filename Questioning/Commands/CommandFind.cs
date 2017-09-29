using Questioning.DTO;
using Questioning.Services.Interface;
using System;
using System.Collections.Generic;

namespace Questioning.Commands
{
    public class CommandFind : BaseCommand
    {
        private readonly IQuestionService questionService;
        public CommandFind(IQuestionService questionService)
        {
            this.questionService = questionService;
        }
        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdFind, Resource.CmdFind_Help, Resource.CmdFind_Format);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            QuestionDTO[] questions = questionService.GetProfile(commandParameters[0].ToString());

            IList<string> outputLines = new List<string>();
            foreach (var q in questions)
            {
                string val = null;
                switch (q.TypeAnswer)
                {
                    case TypeAnswer.DateTime:
                        val = q.DateTimeValue.Value.ToShortDateString();
                        break;
                    case TypeAnswer.Int:
                        val = q.IntValue.ToString();
                        break;
                    case TypeAnswer.String:
                        val = q.StringValue;
                        break;
                }

                outputLines.Add(String.Format(" {0}: {1}", q.Name, val));
            }
            this.outputLines = outputLines;

            return CommandMode.Write;
        }
    }
}
