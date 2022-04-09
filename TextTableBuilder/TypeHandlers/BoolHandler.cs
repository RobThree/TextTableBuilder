namespace TextTableBuilder.TypeHandlers;

public class BoolHandler : ITypeHandler
{
    public string Handle(object value, IFormatProvider formatProvider) => ((bool)value).ToString(formatProvider);
}
