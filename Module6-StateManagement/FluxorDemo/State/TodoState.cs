namespace FluxorDemo.State;
using System.Collections.Generic;

public record TodoState(IReadOnlyList<TodoItem> Todos);
