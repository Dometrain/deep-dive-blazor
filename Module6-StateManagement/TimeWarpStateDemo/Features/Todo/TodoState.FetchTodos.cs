using TimeWarp.State;

namespace TimeWarpStateDemo.Features.Todo;

partial class TodoState
{
    public static class FetchTodosActionSet
    {
        public sealed class Action : IAction { }
        
        public sealed class Handler : ActionHandler<Action>
        {
            private readonly TodoService TodoService;
            
            public Handler(IStore store, TodoService todoService) : base(store)
            {
                TodoService = todoService;
            }
            
            private TodoState TodoState => Store.GetState<TodoState>();

            public override async Task Handle(Action action, CancellationToken cancellationToken)
            {
                var todos = await TodoService.GetTodoItemsAsync();
                TodoState.TodoItems = new List<TodoItem>(todos);
            }
        }
    }
}
