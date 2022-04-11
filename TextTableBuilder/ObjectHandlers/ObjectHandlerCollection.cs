namespace TextTableBuilder.ObjectHandlers;

public class ObjectHandlerCollection : HandlerCollection<IObjectHandler>
{
    public void AddHandler<T>(Func<T, int, object[]> func) => AddHandler<T>(new DelegatingObjectHandler<T>(func));

    public ObjectHandlerCollection() =>
        AddHandler<object>(new DefaultObjectHandler());
}
