namespace TextTableBuilder.ObjectHandlers;

public class DelegatingObjectHandler<T> : IObjectHandler
{
    private readonly Func<T, int, object?[]> _delegate;

    public DelegatingObjectHandler(Func<T, int, object?[]> handlerFunction)
        => _delegate = handlerFunction ?? throw new ArgumentNullException(nameof(handlerFunction));

    public object?[] Handle(object value, int columnCount)
        => _delegate((T)value, columnCount);
}