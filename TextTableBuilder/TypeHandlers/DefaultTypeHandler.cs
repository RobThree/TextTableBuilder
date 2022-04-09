namespace TextTableBuilder.TypeHandlers;

public class DefaultTypeHandler : ITypeHandler
{
    public string Handle(object value, IFormatProvider formatProvider)
        => value.ToString();
}
