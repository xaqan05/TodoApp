using System;
using System.Collections.Generic;

namespace TodoApp.Models;

public partial class Todo
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime Deadline { get; set; }

    public bool IsDone = false;
}
