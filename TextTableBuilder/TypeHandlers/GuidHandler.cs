namespace TextTableBuilder.TypeHandlers;

public class GuidHandler : ITypeHandler
{
    public string Handle(object value, IFormatProvider formatProvider) => ((Guid)value).ToString("D", formatProvider);
}
