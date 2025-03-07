using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Common.DTOs;

public class AddTaskDto
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime DueDate { get; set; }
}
