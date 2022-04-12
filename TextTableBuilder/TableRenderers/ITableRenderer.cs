using System.Collections.ObjectModel;

namespace TextTableBuilder.TableRenderers;

public interface ITableRenderer
{
    string Render(ReadOnlyCollection<RenderColumn> columns, IEnumerable<string[]> rows);
}
