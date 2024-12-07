using TimeWarp.State;

namespace TimeWarpStateDemo.Features.Todo;

internal sealed partial class TodoState : State<TodoState>
{
    private List<TodoItem>? TodoItems { get; set; }
    
    public IReadOnlyList<TodoItem>? Todos => TodoItems?.AsReadOnly();
    
    public override void Initialize()
    {
        TodoItems = null;
    }
}

public record TodoItem(Guid Id, string Text, bool IsCompleted);
