using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Model.Entities;

public class Task
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime DueDate { get; set; }

}
