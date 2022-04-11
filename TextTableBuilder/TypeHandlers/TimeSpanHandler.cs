namespace TextTableBuilder.TypeHandlers;

public class TimeSpanHandler : SpecificFormatHandler
{
    public TimeSpanHandler(string format = @"hh\:mm\:ss")
        : base(format) { }

    public override string Handle(object value, IFormatProvider formatProvider)
        => ((TimeSpan)value).ToString(Format, formatProvider);
}