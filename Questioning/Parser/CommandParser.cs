using Questioning.Parser.Interface;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Questioning.Parser
{
    public class CommandParser: ICommandParser
    {
        public CommandParserResult Parse(string commandText)
        {
            if (String.IsNullOrWhiteSpace(commandText))
                throw new CommandParserException(Resource.Text_ParserEmptyValue);

            var parts = new Regex("(?<=\")[^\"]*(?=\")|[^\" ]+")
                .Matches(commandText)
                .Cast<Match>()
                .Select(m => m.Value.ToLower().Replace("\"", "").Replace("\'", ""))
                .ToArray();

            if (!AllCommands.Items.ContainsKey(parts[0]))
            {
                throw new CommandParserException(Resource.Text_ParserUnknownCommand);
            }

            if (parts[0] == Resource.CmdNewProfile ||
                parts[0] == Resource.CmdStatistics ||
                parts[0] == Resource.CmdSave ||
                parts[0] == Resource.CmdGotoPrevQuestion ||
                parts[0] == Resource.CmdRestartProfile ||
                parts[0] == Resource.CmdList ||
                parts[0] == Resource.CmdListToday ||
                parts[0] == Resource.CmdHelp ||
                parts[0] == Resource.CmdExit)

                return new CommandParserResult(parts[0], null);

            if (parts[0] == Resource.CmdGotoQuestion)
            {
                if(parts.Length < 2)
                    throw new CommandParserException(String.Format("{0} - {1}", Resource.Text_NeedRequeryParameter, Resource.Text_NumberQuestion),  
                        TypeCommandParserException.Parameters, parts[0]);

                int n = 0;
                if (!Int32.TryParse(parts[1], out n))
                    throw new CommandParserException(Resource.Text_NumberQuestionMustBeNumber, 
                        TypeCommandParserException.Parameters, parts[0]);
                if (n <=0 || n > QuestionaryContext.Questions.Length)
                    throw new CommandParserException(String.Format(Resource.Text_NumberQuestionMustBePositiveNumber, QuestionaryContext.Questions.Length), 
                        TypeCommandParserException.Parameters, parts[0]);

                return new CommandParserResult(parts[0], new object[] { n - 1 });
            }

            if (parts[0] == Resource.CmdFind || parts[0] == Resource.CmdDelete || parts[0] == Resource.CmdZip)
            {
                if (parts.Length < 2)
                    throw new CommandParserException(String.Format("{0} - {1}", Resource.Text_NeedRequeryParameter, Resource.Text_Questionary), 
                        TypeCommandParserException.Parameters, parts[0]);

                if (parts[0] != Resource.CmdZip)
                    return new CommandParserResult(parts[0], new object[] { parts[1] });
                else
                {
                    if (parts.Length < 3)
                        throw new CommandParserException(String.Format("{0} - {1}", Resource.Text_NeedRequeryParameter, Resource.Text_ArchivePath), 
                            TypeCommandParserException.Parameters, parts[0]);

                    return new CommandParserResult(parts[0], new object[] { parts[1], parts[2] });
                }
            }

            throw new CommandParserException(Resource.Text_ParserOtherError, TypeCommandParserException.Other);
        }
        
    }
}
