namespace FluxorDemo.Feature;
using Fluxor;
using FluxorDemo.State;

public class TodoFeature : Feature<TodoState>
{
    public override string GetName() => "Todos";

    protected override TodoState GetInitialState() => new TodoState(new List<TodoItem>());
}
