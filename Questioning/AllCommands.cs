using Microsoft.Practices.Unity;
using Questioning.Commands;
using System.Collections.Generic;

namespace Questioning
{
    public static class AllCommands
    {
        static AllCommands()
        {
            UnityServiceLocator serviceLocator = new UnityServiceLocator(UnityConfig.GetConfiguredContainer());
            Items = new Dictionary<string, BaseCommand>()
            {
                { Resource.CmdNewProfile,   serviceLocator.GetInstance<CommandNewProfile>() },
                { Resource.CmdSave,         serviceLocator.GetInstance<CommandSave>() },
                { Resource.CmdDelete,       serviceLocator.GetInstance<CommandDelete>() },

                { Resource.CmdFind,         serviceLocator.GetInstance<CommandFind>() },
                { Resource.CmdList,         serviceLocator.GetInstance<CommandList>() },
                { Resource.CmdListToday,    serviceLocator.GetInstance<CommandListToday>() },

                { Resource.CmdGotoPrevQuestion, serviceLocator.GetInstance<CommandGotoPrevQuestion>() },
                { Resource.CmdGotoQuestion,     serviceLocator.GetInstance<CommandGotoQuestion>() },
                { Resource.CmdRestartProfile,   serviceLocator.GetInstance<CommandRestartProfile>() },

                { Resource.CmdStatistics,   serviceLocator.GetInstance<CommandStatistics>() },
                { Resource.CmdZip,          serviceLocator.GetInstance<CommandZip>() },
                { Resource.CmdExit,         serviceLocator.GetInstance<CommandExit>() },
                { Resource.CmdHelp,         serviceLocator.GetInstance<CommandHelp>() }
            };
        }

        public static IDictionary<string, BaseCommand> Items { get; private set; }
    }
}
