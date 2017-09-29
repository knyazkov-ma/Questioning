using System;
using System.Linq;
using System.Collections.Generic;

namespace Questioning.Commands
{
    public class CommandHelp : BaseCommand
    {
        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdHelp, Resource.CmdHelp_Help);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            int maxCommandLength = AllCommands.Items.Max(c => c.Key.Length);
            int minSpaceLength = 3;
            int maxCommandHelpLength = (int)Math.Round((Console.WindowWidth - maxCommandLength - minSpaceLength)*0.85);

            IList<string> outputLines = new List<string>() { String.Empty };
            foreach (var item in AllCommands.Items)
            {
                CommandHelpInfo info = item.Value.GetCommandHelp();
                int spaceLength = maxCommandLength - info.CommandName.Length;

                string descriptionPart = "";
                string[] words = info.Description.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                bool first = true;
                int commandHelpLength = 0;
                foreach (var s in words)
                {
                    commandHelpLength += s.Length + 1;
                    if (commandHelpLength < maxCommandHelpLength)
                    {
                        descriptionPart += String.Format("{0} ", s);
                    }
                    else
                    {
                        if (first)
                        {
                            outputLines.Add(String.Format(" {0}{1}{2}", info.CommandName, new String(' ', spaceLength + minSpaceLength), descriptionPart));
                            first = false;
                        }
                        else
                        {
                            outputLines.Add(String.Format(" {0}{1}", new String(' ', maxCommandLength + minSpaceLength), descriptionPart));
                        }
                        commandHelpLength = s.Length + 1;
                        descriptionPart = String.Format("{0} ", s);
                    }
                }
                if(first)
                    outputLines.Add(String.Format(" {0}{1}{2}", info.CommandName, new String(' ', spaceLength + minSpaceLength), descriptionPart));
                else
                    outputLines.Add(String.Format(" {0}{1}", new String(' ', maxCommandLength + minSpaceLength), descriptionPart));

                if (!String.IsNullOrWhiteSpace(info.Format))
                {
                    outputLines.Add(String.Empty);
                    outputLines.Add(String.Format(" {0}{1}", new String(' ', maxCommandLength + minSpaceLength), info.Format));
                }

                outputLines.Add(String.Empty);
            }

            if (this.outputLines == null)
                this.outputLines = outputLines;

            return CommandMode.Write;


        }
    }
}
