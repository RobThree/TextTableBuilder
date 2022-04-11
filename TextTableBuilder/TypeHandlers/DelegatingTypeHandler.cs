namespace TextTableBuilder.TypeHandlers;

public class DelegatingTypeHandler<T> : ITypeHandler
{
    private readonly Func<T, IFormatProvider, string> _delegate;

    public DelegatingTypeHandler(Func<T, IFormatProvider, string> handlerFunction)
        => _delegate = handlerFunction ?? throw new ArgumentNullException(nameof(handlerFunction));

    public string Handle(object value, IFormatProvider formatProvider)
        => _delegate((T)value, formatProvider);
}