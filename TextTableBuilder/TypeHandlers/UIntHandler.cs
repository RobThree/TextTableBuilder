namespace TextTableBuilder.TypeHandlers;

public class UIntHandler : NaturalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((uint)value).ToString(Format, formatProvider);
}
