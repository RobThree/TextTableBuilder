namespace TextTableBuilder.ObjectHandlers;
public interface IObjectHandler
{
    object?[] Handle(object value, int columnCount);
}
