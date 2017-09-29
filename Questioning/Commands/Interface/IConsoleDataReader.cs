using System;

namespace Questioning.Commands.Interface
{
    public interface IConsoleDataReader
    {
        CommandMode ReadString(Func<string, string> inputTextDelegate);
    }
}
