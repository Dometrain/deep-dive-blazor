using Microsoft.AspNetCore.Components;
using NonFluxorDemo.State;
using System.Collections.ObjectModel;

namespace FluxorDemo.Services;

public class TodoService
{
    //This is our made up database or API.
    public ReadOnlyCollection<TodoItem> Items => _items.AsReadOnly();
    private List<TodoItem> _items = new List<TodoItem>();

    public async Task<ReadOnlyCollection<TodoItem>> GetTodoItemsAsync()
    {
        await Task.Delay(3000);
        if (!_items.Any())
        {
            _items = new() { new(Guid.NewGuid(), "Item from database", false) };
        }
        ItemsChanged?.Invoke();
        return Items.AsReadOnly();
    }

    public async Task<TodoItem> ToogleTodoAsync(Guid id)
    {
        await Task.Delay(1000);
        //Call the database or API
        var index = _items.FindIndex(todo => todo.Id == id);
        if (index != -1)
        {
            var item = Items[index];
            var newitem = item with { IsCompleted = !item.IsCompleted };
            _items[index] = newitem;
            ItemsChanged?.Invoke();
            return newitem;
        }
        throw new Exception("Item not found");
    }

    public async Task<TodoItem> AddTodoAsync(string text)
    {
        var item = new TodoItem(Guid.NewGuid(), text, false);
        await Task.Delay(1000);
        _items.Add(item);
        ItemsChanged?.Invoke();
        return item;
    }
    
    public Action? ItemsChanged { get; set; }

}
