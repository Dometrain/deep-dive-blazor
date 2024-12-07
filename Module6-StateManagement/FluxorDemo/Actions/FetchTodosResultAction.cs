using FluxorDemo.State;

namespace FluxorDemo.Actions;

public class FetchTodosResultAction
{
    public IReadOnlyList<TodoItem> Items { get; }

    public FetchTodosResultAction(IReadOnlyList<TodoItem> items)
    {
        Items = items;
    }
}
