namespace TextTableBuilder.ObjectHandlers;

public class ObjectHandlerCollection : HandlerCollection<IObjectHandler>
{
    public ObjectHandlerCollection() =>
        AddHandler<object>(new DefaultObjectHandler());
}
