using System;

namespace Questioning.Commands
{
    public class CommandExit : BaseCommand
    {
        public override CommandHelpInfo GetCommandHelp()
        {
            return new CommandHelpInfo(Resource.CmdExit, Resource.CmdExit_Help);
        }

        public override CommandMode Run(object[] commandParameters = null)
        {
            throw new NotImplementedException();
        }
    }
}
