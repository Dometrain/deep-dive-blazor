namespace TimeWarpStateDemo.Features.Todo;

public class TodoService
{
    private List<TodoItem> _items = new();
    public IReadOnlyList<TodoItem> Items => _items.AsReadOnly();

    public async Task<IReadOnlyList<TodoItem>> GetTodoItemsAsync()
    {
        await Task.Delay(3000);
        if (!_items.Any())
        {
            _items = new() { new(Guid.NewGuid(), "Item from database", false) };
        }
        
        return Items;
    }

    public async Task<TodoItem> ToggleTodoAsync(Guid id)
    {
        await Task.Delay(1000);
        var index = _items.FindIndex(todo => todo.Id == id);
        if (index != -1)
        {
            var item = Items[index];
            var newItem = item with { IsCompleted = !item.IsCompleted };
            _items[index] = newItem;
            return newItem;
        }
        throw new Exception("Item not found");
    }

    public async Task<TodoItem> AddTodoAsync(string text)
    {
        var item = new TodoItem(Guid.NewGuid(), text, false);
        await Task.Delay(1000);
        _items.Add(item);
        return item;
    }
}
