namespace TextTableBuilder.ObjectHandlers;

public class DefaultObjectHandler : IObjectHandler
{
    public object[] Handle(object value, int columnCount) => new string[columnCount];
}
