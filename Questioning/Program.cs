using Microsoft.Practices.Unity;
using Questioning.Commands;
using Questioning.Parser;
using System;

namespace Questioning
{
    class Program
    {
        static ConsoleColor inputColor = Console.ForegroundColor;
        static void writeErrorLine(string message1, string message2, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(String.Format("{0}{1} {2}", message1, Resource.Cmd_PromptInputData, message2));
            Console.ForegroundColor = inputColor;
        }
        static void Main(string[] args)
        {
            UnityConfig.RegisterTypes(new UnityContainer());

            UnityServiceLocator serviceLocator = new UnityServiceLocator(UnityConfig.GetConfiguredContainer());
            MainRunner mainRunner = serviceLocator.GetInstance<MainRunner>();

            Console.WriteLine(Resource.Text_SelectAction);

            while (true)
            {
                try
                {
                    if(!mainRunner.Run(writeErrorLine, Console.Write, Console.WriteLine, Console.ReadLine))
                        return;
                }
                catch (CommandParserException ex)
                {
                    writeErrorLine(Resource.Text_CommandParserError, ex.Message, ConsoleColor.Red);
                }
                catch (CommandException ex)
                {
                    writeErrorLine(Resource.Text_CommandError, ex.Message, ConsoleColor.White);
                }
                catch (ConsoleDataReaderException ex)
                {
                    writeErrorLine(Resource.Text_DataError, ex.Message, ConsoleColor.DarkRed);
                }
                
            }
            
        }
    }
}
