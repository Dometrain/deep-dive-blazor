using TimeWarp.State;

namespace TimeWarpStateDemo.Features.Todo;

partial class TodoState
{
    public static class ToggleTodoActionSet
    {
        public sealed class Action : IAction
        {
            public Guid TodoId { get; }
            
            public Action(Guid todoId)
            {
                TodoId = todoId;
            }
        }
        
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
                var updatedTodo = await TodoService.ToggleTodoAsync(action.TodoId);
                var index = TodoState.TodoItems?.FindIndex(todo => todo.Id == action.TodoId) ?? -1;
                if (index != -1 && TodoState.TodoItems != null)
                {
                    TodoState.TodoItems[index] = updatedTodo;
                }
            }
        }
    }
}
