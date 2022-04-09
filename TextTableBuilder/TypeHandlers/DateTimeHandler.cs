namespace TextTableBuilder.TypeHandlers;

public class DateTimeHandler : SpecificFormatHandler
{
    public DateTimeHandler(string format = "yyyy-MM-dd HH:mm:ss")
        : base(format) { }

    public override string Handle(object value, IFormatProvider formatProvider)
        => ((DateTime)value).ToString(Format, formatProvider);
}
