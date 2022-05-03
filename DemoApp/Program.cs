using TextTableBuilder;
using TextTableBuilder.TableRenderers;

var table = new Table()
    .AddColumns("No", "Name", "Position", "^Salary^")
    .AddRow(1, "Bill Gates", "Founder Microsoft", 10000m)
    .AddRow(2, "Steve Jobs", "Founder Apple", 1200000m)
    .AddRow(3, "Larry Page", "Founder Google", 1100000m)
    .AddRow(4, "Mark Zuckerberg", "Founder Facebook", 1300000m);

var renderers = new ITableRenderer[] {
    new DefaultTableRenderer(),
    new MinimalTableRenderer(),
    new MSDOSTableRenderer(),
    new SimpleLineTableRenderer(),
    new SingleLineTableRenderer(),
    new DoubleLineTableRenderer(),
    new RoundedCornersTableRenderer(),
    new HatchedTableRenderer(),
    new DotsTableRenderer()
};

var tablebuilder = new TableBuilder()
    .AddTypeHandler<decimal>((v, f) => $"$ {v:N2}");

foreach (var r in renderers)
{
    Console.Write($"{r.GetType().Name}:\n\n");
    Console.Write(tablebuilder.Build(table, r));
    Console.Write("\n\n");
}