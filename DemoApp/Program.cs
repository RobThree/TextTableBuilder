using System.Net;
using TextTableBuilder;
using TextTableBuilder.ObjectHandlers;
using TextTableBuilder.TypeHandlers;

namespace DemoApp;

public class Program
{
    public static void Main()
    {
        var t = new Table();
        t.AddColumns(new[] { "A", "B", "C", "^D^", "~E~" })
            .AddRow("Foo", 1, 2.23m, DateTime.Now, IPAddress.IPv6Any)
            .AddRow("F", 1, 2.23m, Guid.NewGuid(), IPAddress.IPv6Any)
            .AddRow("Fo", 1, 2.23m, "Test", IPAddress.IPv6Any)
            .AddRow("Foo", 1, 2.23m, null, IPAddress.IPv6Any)
            .AddRow("Fooo", 1, 2.23m, string.Empty, IPAddress.IPv6Any)
            .AddRow("Foooo", 1, 2.23m, DateTime.Now, IPAddress.IPv6Any)
            .AddRow("Fooooo", 1, 2.23m, DateTime.Now, IPAddress.IPv6Any)
            .AddRow("Foooooo", 1, 2.23m, DateTime.Now, IPAddress.IPv6Any)
            .AddRow("Fooooooo", 1, 2.23m, DateTime.Now, IPAddress.IPv6Any)
            .AddRow("Lalaqeww", 132987, 0m, DateTime.Now.AddDays(1), IPAddress.Any)
            .AddRow("Lala", 987, 0m, DateTime.Now.AddDays(1), IPAddress.Broadcast)
            .AddRow(new Customer("Cust", 1, 5.12m, new DateTime(1977, 12, 1), IPAddress.None));

        var tb = new TableBuilder();
        tb.TypeHandlers.NullValueHandler = new NullHandler("<NULL>");
        tb.ObjectHandlers.AddHandler<Customer>(new CustomerHandler());
        Console.WriteLine(tb.Build(t));
    }
}

//public class CurrencyTypeHandler : DecimalHandler
//{
//    public override string Handle(object value, IFormatProvider formatProvider)
//        => $"$ {base.Handle(value, formatProvider)}";
//}

public class CurrencyTypeHandler : DelegatingTypeHandler<decimal>
{
    public CurrencyTypeHandler()
        : base((value, formatProvider) => $"EUR {value.ToString(formatProvider)}") { }
}

//public class CustomerHandler : IObjectHandler
//{
//    public object[] Handle(object value, int columnCount)
//    {
//        var customer = (Customer)value;
//        return new object[] {
//            customer.Name,
//            customer.Id ,
//            customer.Money,
//            customer.BirthDate,
//            customer.Ip
//        };
//    }
//}

public class CustomerHandler : DelegatingObjectHandler<Customer>
{
    public CustomerHandler()
        : base((customer, columnCount) => new object[] {
            customer.Name,
            customer.Id ,
            customer.Money,
            customer.BirthDate,
            customer.Ip
        })
    { }
}


public record Customer(string Name, int Id, decimal Money, DateTime BirthDate, IPAddress Ip);