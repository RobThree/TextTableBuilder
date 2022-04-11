using System.Collections.Concurrent;
using System.Reflection;

namespace TextTableBuilder.ObjectHandlers;

public class DefaultObjectHandler : IObjectHandler
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _propertycache = new();
    private static readonly ConcurrentDictionary<PropertyInfo, int> _columnordercache = new();

    public object?[] Handle(object value, int columnCount)
    {
        // Get properties in correct order
        var props = _propertycache.GetOrAdd(value.GetType(), (t) => t
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.CanRead)
            .OrderBy(p => _columnordercache.GetOrAdd(p, (pi) => pi.GetCustomAttribute<ColumnOrderAttribute>()?.Order ?? 0))
            .ThenBy(p => p.Name)
            .ToArray()
        );

        // Figure out if we need to pad our results with one or more (null) columns
        var padcols = columnCount > props.Length ? Enumerable.Repeat<object?>(null, columnCount - props.Length) : Array.Empty<object?>();

        // Return property values (and optional padding)
        return props.Take(columnCount).Select(p => p.GetValue(value)).Concat(padcols).ToArray();
    }
}