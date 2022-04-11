# ![Logo](logo.png) TextTableBuilder

[![Build status](https://ci.appveyor.com/api/projects/status/am77wpby61rbgqxh)](https://ci.appveyor.com/project/RobIII/texttablebuilder) <a href="https://www.nuget.org/packages/TextTableBuilder/"><img src="http://img.shields.io/nuget/v/TextTableBuilder.svg?style=flat-square" alt="NuGet version" height="18"></a>

A simple, opinionated, modern table builder. Supports configuring how different datatypes will be formatted. Available as [Nuget package](https://www.nuget.org/packages/TextTableBuilder/)


## Quickstart

```c#
// Create table
var table = new Table();
table.AddColumn("No")
    .AddColumn("Name")
    .AddColumn("Position")
    .AddColumn("Salary", Align.Right, Align.Right)  // Align column header and values to the right
    .AddRow(1, "Bill Gates", "Founder Microsoft", 10000)
    .AddRow(2, "Steve Jobs", "Founder Apple", 1200000)
    .AddRow(3, "Larry Page", "Founder Google", 1100000)
    .AddRow(4, "Mark Zuckerberg", "Founder Facebook", 1300000);

// Use TableBuilder to render table
var tablebuilder = new TableBuilder();
Console.WriteLine(tablebuilder.Build(table));
```

```cmd
No | Name            | Position          |    Salary
-- | --------------- | ----------------- | ---------
1  | Bill Gates      | Founder Microsoft |    10,000
2  | Steve Jobs      | Founder Apple     | 1,200,000
3  | Larry Page      | Founder Google    | 1,100,000
4  | Mark Zuckerberg | Founder Facebook  | 1,300,000
```

## Convenience methods

### Columns

An easier, quicker way to add columns is to invoke `AddColumns()`. By passing an array of column names all columns can be specified in one call:

```c#
var table = new Table();
table.AddColumns(new[] { "No.", "Name", "Position", "^Salary^" })
    .AddRow(1, "Bill Gates", "Founder Microsoft", 10000)
    // etc...
```

For aligning columns, see [Aligning columns and values](#aligning-columns-and-values).

### Rows

Rows can be added in three ways:

1. `AddRow(Row row)`<br>Either pass a `ValueRow` or `ObjectRow`
2. `AddRow(params object[] values)`<br>Pass all values (e.g. `.AddRow("foo", 123, "bar)`)
3. `AddRow<T>(value)`<br>Pass an object (e.g `.AddRow<Customer>(paul)`) (see [Type handling](#type-handling))

Method 2 adds a `ValueRow` to the table whereas method 3 adds an `ObjectRow` to the table. Method 1 is provided only for completeness' sake.

For aligning row values, see [Aligning columns and values](#aligning-columns-and-values).

## Aligning columns and values

Columnames can be prefixed and suffixed with:

* `^` Align right
* `~` Align center

When a columnname is specified as `"^Salary"`, the column name will be right aligned, the values will default to left. When the name is specified as `"Salary^"` the column values will be right aligned, the column name itself will default to left aligned. And, finally, when the name is specified as `"^Salary^"` then both the column name and values will be right aligned.

If you want more control over a column you'll need to use the `AddColumn()` method which allows you to specify a minimum width for the column as well as a `TypeHandler` (see [Type Handling](#type-handling)).

## Column widths

A column, by default, simply stretches to be wide enough to contain all values in that column. You can, however, specify a minimum width (`MinWidth`) or a width (`Width`). The `MinWidth` ensures the column is always at least the number of specified characters wide, but may be less wide when the column only contains values of less length. The `Width` ensures a column is always exactly the specified width. Longer values will be truncated. Note that truncating depends on the alignment of the values. Right-aligned values will be truncated from the left, left-aligned values will be truncated from the right and center-aligned values will be truncated from both sides.

To specifiy a width, use either the `AddColumn()` overload that allows you to pass an optional `minWidth` or `width` argument, or the `AddColumn(Column)` overload and specify the `width` or `minWidth` with the `Column`'s constructor arguments.

## Internationalization (i18n)

TextTableBuilder supports i18n by supporting an `IFormatProvider` which can be specified by passing it to the `Build()` method. The above example is based on an `en_US` locale. If we pass another locale, we get:

```c#
Console.WriteLine(tablebuilder.Build(table, new CultureInfo("nl_NL")));
```

```cmd
No | Name            | Position          |    Salary
-- | --------------- | ----------------- | ---------
1  | Bill Gates      | Founder Microsoft |    10.000
2  | Steve Jobs      | Founder Apple     | 1.200.000
3  | Larry Page      | Founder Google    | 1.100.000
4  | Mark Zuckerberg | Founder Facebook  | 1.300.000
```

By default, unless specified otherwise, the TaxtTableBuilder uses the current UI locale (`CultureInfo.CurrentUICulture`).

## Type handling

By default TextTableBuilder comes with type handlers for all primitives (e.g. `int`, `decimal`, ...) and some other common types like `DateTime` and `TimeSpan`. However, you can customize how a type is formatted by specifying a `TypeHandler` that implements `ITypeHandler`.

TextTableBuilder will first try to use the `TypeHandler` for the column being formatted; when no `TypeHandler` is specified for a column then the type of the value is used to determine which `TypeHandler` to use.

An example of a typehandler is:

```c#
public class CurrencyTypeHandler : ITypeHandler
{
    public string Handle(object value, IFormatProvider formatProvider)
        => string.Format("$ {0:N2}", value);
}
```

So when we then specify our values as decimals (by adding the `m`-suffix)...

```c#
var table = new Table();
table.AddColumns(new[] { "No.", "Name", "Position", "^Salary^" })
    .AddRow(1, "Bill Gates", "Founder Microsoft", 10000m)
    .AddRow(2, "Steve Jobs", "Founder Apple", 1200000m)
    .AddRow(3, "Larry Page", "Founder Google", 1100000m)
    .AddRow(4, "Mark Zuckerberg", "Founder Facebook", 1300000m);
```

...and we register our new `CurrencyTypeHandler`...

```c#
var tablebuilder = new TableBuilder();
tablebuilder.TypeHandlers.AddHandler<decimal>(new CurrencyTypeHandler());
Console.WriteLine(tablebuilder.Build(table, new CultureInfo("en_US")));
```

...we get:

```cmd
No. | Name            | Position          |         Salary
--- | --------------- | ----------------- | --------------
1   | Bill Gates      | Founder Microsoft |    $ 10.000,00
2   | Steve Jobs      | Founder Apple     | $ 1.200.000,00
3   | Larry Page      | Founder Google    | $ 1.100.000,00
4   | Mark Zuckerberg | Founder Facebook  | $ 1.300.000,00
```

An alternative method of creating a `TypeHandler` is to inherit from `DelegatingTypeHandler<T>` which allows you to simply use a delegate function:

```c#
public class CurrencyTypeHandler : DelegatingTypeHandler<decimal>
{
    public CurrencyTypeHandler()
        : base((value, formatProvider) => string.Format("$ {0:N2}", value)) { }
}
```

Or, even shorter:

```c#
tablebuilder.TypeHandlers.AddHandler<decimal>(new DelegatingTypeHandler<decimal>((value, fp) => string.Format("$ {0:N2}", value)));
```

And still shorter:

```c#
tablebuilder.TypeHandlers.AddHandler<decimal>((value, formatProvider) => string.Format("$ {0:N2}", value));
```

And for those about to point out this can be written even shorter:

```c#
tablebuilder.TypeHandlers.AddHandler<decimal>((v, _) => $"$ {v:N2}");
```

### Null value handling

A special case is the `NullValueHandler`; by default a `null` value is formatted as an empty string. However, you may want to show `null` values as "`<NULL>`" for example. To accomplish this we simply use the built-in `NullValueHandler`:

```c#
tablebuilder.TypeHandlers.NullValueHandler = new NullHandler("<NULL>");
```

It is possible to implement your own `NullValueHandler` by implementing `INullValueHandler`.

### Object handling

For the following examples we're going to assume a collection of persons:

```c#
public record Person(string Name, string Position, decimal Salary);

var persons = new[]
{
    new Person("Bill Gates", "Founder Microsoft", 10000m),
    new Person("Steve Jobs", "Founder Apple", 1200000m),
    new Person("Larry Page", "Founder Google", 1100000m),
    new Person("Mark Zuckerberg", "Founder Facebook", 1300000m),
};
```
#### Default object handling

By default the TextTableBuilder outputs properties of objects in alfabetical order; for our example that just happens to work out:

```c#
var table = new Table();
table.AddColumns(new[] { "Name", "Position", "^Salary^" })
    .AddRows(persons);

var tablebuilder = new TableBuilder();
Console.WriteLine(tablebuilder.Build(table));
```

The order of the outputted properties can be changed by using a [ColumnOrder attribute](#columnorder-attribute). Properties (or fields) that don't have this attribute will be ordered by name.

You'll probably want (a lot) more control; in which case you should look into [Custom object handling](#custom-object-handling).

#### Custom object handling
First, we implement an `IObjectHandler`:

```c#
public class PersonHandler : IObjectHandler
{
    public object[] Handle(object value, int columnCount)
    {
        var person = (Person)value;
        // Return properties as value array
        return new object[] { person.Name, person.Position, person.Salary };
    }
}
```

After that, building a table for this data is simple:

```c#
var table = new Table();
table.AddColumns(new[] { "Name", "Position", "^Salary^" })
    .AddRows(persons);

var tablebuilder = new TableBuilder();
// Specify object handler to use for persons
tablebuilder.ObjectHandlers.AddHandler<Person>(new PersonHandler());
Console.WriteLine(tablebuilder.Build(table));
```

Which outputs:

```cmd
Name            | Position          |       Salary
--------------- | ----------------- | ------------
Bill Gates      | Founder Microsoft |    10.000,00
Steve Jobs      | Founder Apple     | 1.200.000,00
Larry Page      | Founder Google    | 1.100.000,00
Mark Zuckerberg | Founder Facebook  | 1.300.000,00
```

TextTableBuilder will still use the `TypeHandler`s to handle the types of the values as always.

A shorter method is to inherit from the `DelegateObjectHandler<T>`:

```c#
public class PersonHandler : DelegatingObjectHandler<Person>
{
    public PersonHandler()
        : base((person, columnCount) => new object[] { person.Name, person.Position, person.Salary }) { }
}
```

Even shorter:

```c#
tablebuilder.ObjectHandlers.AddHandler<Person>(new DelegatingObjectHandler<decimal>((person, fp) => new object[] { person.Name, person.Position, person.Salary }));
```

Still shorter:

```c#
tablebuilder.ObjectHandlers.AddHandler<Person>((person, columnCount) => new object[] { person.Name, person.Position, person.Salary });
```

When no handler for a specific object can be found then the `DefaultObjectHandler` is used which simply takes all readable properties and returns those in alfabetical order unless...

### ColumnOrder attribute

When adding rows by adding objects directly (e.g. `.AddRow(myperson)` where `myperson` is a `Person` objecy) the order of the properties can be specified for the `DefaultObjectHandler`. If you implement your own `IObjectHandler` then you need to either return the values in te correct order or look for the `ColumnOrder` attribute and use it's `Order` property to determine the order of the properties.

```c#
public record Person(
    [property: ColumnOrder(2)] string Name,
    [property: ColumnOrder(1)] string Position,
    [property: ColumnOrder(3)] decimal Salary,
    [property: ColumnOrder(4)] DateTime DateOfBirth
);
```

Or, a bit more old-fashioned:

```c#
public class Person
{
    [ColumnOrder(2)]
    public string Name { get; set; }
    [ColumnOrder(1)]
    public string Position { get; set; }
    [ColumnOrder(3)]
    public decimal Salary { get; set; }
    [ColumnOrder(4)]
    public DateTime DateOfBirth { get; set; }
}
```

If we now print the table:

```c#
var persons = new[]
{
    new Person("Bill Gates", "Founder Microsoft", 10000m, new DateTime(1955, 10, 28)),
    new Person("Steve Jobs", "Founder Apple", 1200000m, new DateTime(1955, 2, 24)),
    new Person("Larry Page", "Founder Google", 1100000m, new DateTime(1973, 3, 26)),
    new Person("Mark Zuckerberg", "Founder Facebook", 1300000m, new DateTime(1984, 3, 14)),
};

 var table = new Table();
table.AddColumns(new[] { "Position", "Name", "^Salary^" })
    .AddRows(persons);

var tablebuilder = new TableBuilder();
Console.WriteLine(tablebuilder.Build(table));
```

The result is:

```cmd
Position          | Name            |       Salary
----------------- | --------------- | ------------
Founder Microsoft | Bill Gates      |    10,000.00
Founder Apple     | Steve Jobs      | 1,200,000.00
Founder Google    | Larry Page      | 1,100,000.00
Founder Facebook  | Mark Zuckerberg | 1,300,000.00
```

Note the DateOfBirth column is missing; this is because the `DefaultObjectHandler`, by default, only takes the number of properties equal to the number of columns.

However, if we print the table like this:

```c#
var table = new Table();
table.AddColumns(new[] { "Position", "Name", "^Salary^", "Birthdate", "Alma mater", "Spouse" })
    .AddRows(persons);

var tablebuilder = new TableBuilder();
tablebuilder.TypeHandlers.AddHandler<DateTime>(new DelegatingTypeHandler<DateTime>((date, formatprovider) => $"{date:yyyy-MM-dd}"));
Console.WriteLine(tablebuilder.Build(table));
```

The result is:

```cmd
Position          | Name            |       Salary | Birthdate  | Alma mater | Spouse
----------------- | --------------- | ------------ | ---------- | ---------- | ------
Founder Microsoft | Bill Gates      |    10,000.00 | 1955-10-28 |            |
Founder Apple     | Steve Jobs      | 1,200,000.00 | 1955-02-24 |            |
Founder Google    | Larry Page      | 1,100,000.00 | 1973-03-26 |            |
Founder Facebook  | Mark Zuckerberg | 1,300,000.00 | 1984-03-14 |            |
```

The `DefaultObjectHandler`, by default, pads all rows with missing values with `null` values.

## Styles

The TextTableBuilder has support for (very simple) styles. These can be specified as an optional argument to the `Build()` method. Currently only a very few styles are supported.

To specify a style, invoke the `Build()` method with a `style` argument:

```c#
Console.WriteLine(tablebuilder.Build(table, TableStyle.MSDOS));
```

Going back to our [very first example](#quickstart), the following styles are currently implemented. More _may_ be added in the future (as well as ANSI color support etc.):

### TableStyle.Default

```cmd
No | Name            | Position          |    Salary
-- | --------------- | ----------------- | ---------
1  | Bill Gates      | Founder Microsoft |    10,000
2  | Steve Jobs      | Founder Apple     | 1,200,000
3  | Larry Page      | Founder Google    | 1,100,000
4  | Mark Zuckerberg | Founder Facebook  | 1,300,000
```

### TableStyle.Minimal

```cmd
No Name            Position             Salary
1  Bill Gates      Founder Microsoft    10,000
2  Steve Jobs      Founder Apple     1,200,000
3  Larry Page      Founder Google    1,100,000
4  Mark Zuckerberg Founder Facebook  1,300,000
```

### TableStyle.MSDOS

```cmd
No║Name           ║Position         ║   Salary
══║═══════════════║═════════════════║═════════
1 ║Bill Gates     ║Founder Microsoft║   10,000
2 ║Steve Jobs     ║Founder Apple    ║1,200,000
3 ║Larry Page     ║Founder Google   ║1,100,000
4 ║Mark Zuckerberg║Founder Facebook ║1,300,000
```

## Example

With all the examples above demonstrating a specific option each, you may not have noticed how easy this package can make your life (that's what it's meant to do). So here's an example that shows typical usage. Assuming you have a `Person` class/record but you can't (or don't want to) 'pollute' it with `ColumnOrder`-attributes:

```c#
var table = new Table()
    .AddColumns(new[] { "Name", "Position", "^Salary^" })
    .AddRows(DBContext.Persons.Where(p => p.Salary > 100));

var tablebuilder = new TableBuilder();
// Specify object handler to use for persons
tablebuilder.ObjectHandlers.AddHandler<Person>((person, columnCount) => new object[] { person.Name, person.Position, person.Salary });
Console.WriteLine(tablebuilder.Build(table));
```

---

Icon made by [Freepik](http://www.flaticon.com/authors/freepik) from [www.flaticon.com](http://www.flaticon.com) is licensed by [CC 3.0](http://creativecommons.org/licenses/by/3.0/).
