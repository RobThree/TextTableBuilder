namespace TextTableBuilder.ObjectHandlers;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class ColumnOrderAttribute : Attribute
{
    public int Order { get; }

    public ColumnOrderAttribute(int order) => Order = order;
}