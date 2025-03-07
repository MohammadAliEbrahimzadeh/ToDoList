using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Common.DTOs;

public class FilterGetTaskDto : FilterDto
{
    public FilterGetTaskDto()
    {
        Page = 1;
        PageSize = 10;
    }
    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }
}
