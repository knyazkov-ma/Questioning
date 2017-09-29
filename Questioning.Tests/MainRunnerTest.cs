using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace Questioning.Tests
{
    [TestClass]
    public class MainRunnerTest
    {
        Action<string, string, ConsoleColor> emptyWriteErrorLine = (string s1, string s2, ConsoleColor c) => { };
        Action<string> emptyWrite = (string s) => { };
        Action<string> emptyWriteLine = (string s) => { };

        UnityServiceLocator serviceLocator;
        MainRunner mainRunner;
        public MainRunnerTest()
        {
            UnityConfig.RegisterTypes(new UnityContainer());

            serviceLocator = new UnityServiceLocator(UnityConfig.GetConfiguredContainer());
            mainRunner = serviceLocator.GetInstance<MainRunner>();
        }

        [TestMethod]
        public void SaveAndFindCorrectProfile()
        {

            mainRunner.Run(emptyWriteErrorLine,
                emptyWrite,
                emptyWriteLine,
                () => { return "new_profile"; }
             );

            int i = 0;
            mainRunner.Run(emptyWriteErrorLine,
                emptyWrite,
                emptyWriteLine,
                () =>
                {
                    switch (i)
                    {
                        case 0:
                            i++;
                            return "Иванов Иван Иванович";
                        case 1:
                            i++;
                            return "01.02.1988";
                        case 2:
                            i++;
                            return "C#";
                        case 3:
                            i++;
                            return "2";
                        default:
                            i++;
                            return "+79128475214";

                    }
                }
             );

            mainRunner.Run(emptyWriteErrorLine,
                emptyWrite,
                emptyWriteLine,
                () => { return "save"; }
             );


            IList<string> lines = new List<string>();
            mainRunner.Run(emptyWriteErrorLine,
                emptyWrite,
                emptyWriteLine,
                () => { return "find \"Иванов Иван Иванович\""; }
             );
            mainRunner.Run(emptyWriteErrorLine,
                emptyWrite,
                (string s) =>
                {
                    lines.Add(s);
                },
                () => { return null; }
             );

            Assert.IsTrue(lines[0].Contains("Иванов Иван Иванович"));
            Assert.IsTrue(lines[1].Contains("01.02.1988"));
            Assert.IsTrue(lines[2].Contains("C#"));
            Assert.IsTrue(lines[3].Contains("2"));
            Assert.IsTrue(lines[4].Contains("+79128475214"));
            Assert.IsTrue(lines[5].Contains(DateTime.Now.ToShortDateString()));
        }
       
    }
}
