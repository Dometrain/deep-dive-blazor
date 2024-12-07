using Microsoft.AspNetCore.Components;

namespace BlazorHybrid.Services;

public class CounterService
{
    public int Count { get; set; }
   
    public async Task CountUpAsync()
    {
        Count++;
        CountingUp?.Invoke(Count);
    }

    public event Action<int>? CountingUp;

}
