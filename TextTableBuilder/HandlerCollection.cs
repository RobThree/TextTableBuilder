using System.Collections.Concurrent;

namespace TextTableBuilder;

public abstract class HandlerCollection<T>
{
    private readonly ConcurrentDictionary<Type, T> _handlers = new();

    public void AddHandler(Type type, T typeHandler)
        => _handlers.AddOrUpdate(type, typeHandler, (t, v) => typeHandler);
    public void AddHandler<THandler>(T typeHandler)
        => AddHandler(typeof(THandler), typeHandler);

    public T GetHandler(Type type)
        => _handlers.TryGetValue(type, out var typeHandler)
        ? typeHandler
        : GetHandler(typeof(object));
}
