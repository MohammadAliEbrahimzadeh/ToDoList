using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Common.DTOs;

public class FilterDto
{
    public int Page { get; set; }

    public int PageSize { get; set; }
}
