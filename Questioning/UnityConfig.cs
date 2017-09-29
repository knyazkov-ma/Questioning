using System;
using Microsoft.Practices.Unity;
using Questioning.Repository.Interface;
using Questioning.Entity;
using Questioning.Repository;
using Questioning.Services;
using Questioning.Services.Interface;
using Questioning.Commands;
using Questioning.Interface;
using Questioning.Commands.Interface;
using Questioning.Parser;
using Questioning.Parser.Interface;

namespace Questioning
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICommandParser, CommandParser>();
            container.RegisterType<IPersistenceSettings, PersistenceSettings>();
            container.RegisterType<IRepository<Profile>, ProfileRepository>();
            container.RegisterType<IQuestionService, QuestionService>();
            container.RegisterType<IConsoleDataReader, ConsoleDataReader>();
            
            container.RegisterType<BaseCommand, CommandDelete>();
            container.RegisterType<BaseCommand, CommandExit>();
            container.RegisterType<BaseCommand, CommandFind>();
            container.RegisterType<BaseCommand, CommandGotoPrevQuestion>();
            container.RegisterType<BaseCommand, CommandGotoQuestion>();
            container.RegisterType<BaseCommand, CommandHelp>();
            container.RegisterType<BaseCommand, CommandList>();
            container.RegisterType<BaseCommand, CommandListToday>();
            container.RegisterType<BaseCommand, CommandNewProfile>();
            container.RegisterType<BaseCommand, CommandRestartProfile>();
            container.RegisterType<BaseCommand, CommandSave>();
            container.RegisterType<BaseCommand, CommandStatistics>();
            container.RegisterType<BaseCommand, CommandZip>();

            container.RegisterType<MainRunner>();
        }
        
    }
}
