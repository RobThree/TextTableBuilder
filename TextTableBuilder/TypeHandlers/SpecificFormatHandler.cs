namespace TextTableBuilder.TypeHandlers;

public abstract class SpecificFormatHandler : ITypeHandler
{
    protected string Format { get; }
    public SpecificFormatHandler(string format) => Format = format;

    public abstract string Handle(object value, IFormatProvider formatProvider);
}
