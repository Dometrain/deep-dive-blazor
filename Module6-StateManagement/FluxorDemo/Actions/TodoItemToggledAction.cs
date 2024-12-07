using FluxorDemo.State;

namespace FluxorDemo.Actions;

public class TodoItemToggledAction
{
    public TodoItem Item { get; }

    public TodoItemToggledAction(TodoItem item)
    {
        Item = item;
    }
}
