namespace FluxorDemo.Reducers;

using Fluxor;
using FluxorDemo.Actions;
using FluxorDemo.State;

public static class TodoReducers
{
    [ReducerMethod]
    public static TodoState ReduceFetchTodosResultAction(TodoState state, FetchTodosResultAction action)
    {
        var items = new List<TodoItem>(action.Items);
        return new TodoState(items);
    }

    [ReducerMethod]
    public static TodoState ReduceTodoItemAddedAction(TodoState state, TodoItemAddedAction action)
    {
        var todos = new List<TodoItem>(state.Todos)
        {
            action.Item
        }; 
        return new TodoState(todos);
    }

    [ReducerMethod]
    public static TodoState ReduceTodoItemToggledAction(TodoState state, TodoItemToggledAction action)
    {
        var todos = new List<TodoItem>(state.Todos);
        var index = todos.FindIndex(todo => todo.Id == action.Item.Id);
        if (index != -1)
        {
            var todo = todos[index];
            todos[index] = todo with { IsCompleted = !todo.IsCompleted };
        }
        return new TodoState(todos);
    }
}
