using FluxorDemo.State;

namespace FluxorDemo.Actions;

public class TodoItemAddedAction
{
    public TodoItem Item { get; }

    public TodoItemAddedAction(TodoItem item)
    {
        Item = item;
    }
}
