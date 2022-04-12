using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TextTableBuilder.Tests;

[TestClass]
public class TableBuilderTests
{
    [TestMethod]
    public void Foo()
    {
        var table = new Table()
            .AddColumns(new[] { "A", "B", "C" })
            .AddRow(1, Guid.NewGuid(), DateTime.Now);

        var tb = new TableBuilder();
        var result = tb.Build(table);
    }
}