namespace TextTableBuilder.TypeHandlers;

public class ULongHandler : NaturalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((short)value).ToString(Format, formatProvider);
}
