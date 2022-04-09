namespace TextTableBuilder.TypeHandlers;

public interface ITypeHandler
{
    string Handle(object value, IFormatProvider formatProvider);
}
