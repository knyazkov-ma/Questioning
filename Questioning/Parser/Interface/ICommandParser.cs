namespace Questioning.Parser.Interface
{
    public interface ICommandParser
    {
        CommandParserResult Parse(string commandText);

    }
}
