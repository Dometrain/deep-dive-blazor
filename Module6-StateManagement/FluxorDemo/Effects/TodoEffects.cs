using Fluxor;
using FluxorDemo.Actions;
using FluxorDemo.Services;
using FluxorDemo.State;

namespace FluxorDemo.Effects;

public class TodoEffects(TodoService service)
{
    [EffectMethod]
    public async Task HandleFetchDataAction(FetchTodosAction action, IDispatcher dispatcher)
    {
        var items = await service.GetTodoItemsAsync();
        dispatcher.Dispatch(new FetchTodosResultAction(items));
    }

    [EffectMethod]
    public async Task HandleAddTodoAction(AddTodoAction action, IDispatcher dispatcher)
    {
        var item = await service.AddTodoAsync(action.Text);
        dispatcher.Dispatch(new TodoItemAddedAction(item));
    }

    [EffectMethod]
    public async Task HandleToggleTodoAction(ToggleTodoAction action, IDispatcher dispatcher)
    {
        var item = await service.ToogleTodoAsync(action.TodoId);
        dispatcher.Dispatch(new TodoItemToggledAction(item));
    }
}
