using Questioning.Commands.Interface;
using Questioning.DTO;
using Questioning.Parser;
using Questioning.Parser.Interface;
using Questioning.Services.Interface;
using System;

namespace Questioning.Commands
{
    public class ConsoleDataReader: IConsoleDataReader
    {
        private readonly IQuestionService questionService;
        private readonly ICommandParser commandParser;

        public ConsoleDataReader(IQuestionService questionService, ICommandParser commandParser)
        {
            this.questionService = questionService;
            this.commandParser = commandParser;
        }

        public CommandMode ReadString(Func<string, string> inputTextDelegate)
        {
            QuestionDTO q = QuestionaryContext.Questions[QuestionaryContext.CurrentQuestion];

            string inputText = inputTextDelegate(q.Name);

            try
            {
                CommandParserResult commandParserResult = commandParser.Parse(inputText);
                if (BaseCommand.ForReader(commandParserResult.CommandName))
                {
                    BaseCommand cmd = AllCommands.Items[commandParserResult.CommandName];
                    return cmd.Run(commandParserResult.CommandParameters);
                }
            }
            catch (CommandParserException ex)
            {
                if (ex.TypeException == TypeCommandParserException.Parameters)
                    throw ex;
            }


            try
            {
                switch (q.TypeAnswer)
                {
                    case TypeAnswer.DateTime:
                        q.DateTimeValue = !String.IsNullOrWhiteSpace(inputText)? DateTime.Parse(inputText): (DateTime?)null;
                        break;
                    case TypeAnswer.Int:
                        q.IntValue = !String.IsNullOrWhiteSpace(inputText) ? Int32.Parse(inputText): 0;
                        break;
                    case TypeAnswer.String:
                        q.StringValue = !String.IsNullOrWhiteSpace(inputText) ? inputText: null;
                        break;
                }

                questionService.ValidateQuestionAnswer(QuestionaryContext.Questions, q);
            }
            catch (FormatException)
            {
                throw new ConsoleDataReaderException(Resource.Text_WrongFormatAnswer);
            }
            catch (CommandException ex)
            {
                throw new ConsoleDataReaderException(ex.Message);
            }
                        
            if (QuestionaryContext.CurrentQuestion < QuestionaryContext.Questions.Length - 1)
            {
                QuestionaryContext.CurrentQuestion++;
                return CommandMode.Read;
            }
           
            return CommandMode.General;
        }
        
    }
}
