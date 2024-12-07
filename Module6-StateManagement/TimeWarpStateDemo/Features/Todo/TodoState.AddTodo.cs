using TimeWarp.State;

namespace TimeWarpStateDemo.Features.Todo;
partial class TodoState
{
    public static class AddTodoActionSet
    {
        public sealed class Action : IAction
        {
            public string Text { get; }

            public Action(string text)
            {
                Text = text;
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
                var newTodo = await TodoService.AddTodoAsync(action.Text);
                TodoState.TodoItems ??= new List<TodoItem>();
                TodoState.TodoItems.Add(newTodo);
            }
        }
    }
}
