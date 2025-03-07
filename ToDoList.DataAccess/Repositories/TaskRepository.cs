using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DataAccess.Context;

namespace ToDoList.DataAccess.Repositories;

public class TaskRepository
{
    private readonly ToDoListContext _beaconContext;

    public TaskRepository(ToDoListContext beaconContext)
    {
        _beaconContext = beaconContext;
    }

}
