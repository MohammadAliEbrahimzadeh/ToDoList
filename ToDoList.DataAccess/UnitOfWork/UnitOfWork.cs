using ToDoList.DataAccess.Context;
using ToDoList.DataAccess.Repositories;

namespace ToDoList.DataAccess;

public class UnitOfWork : BaseUnitOfWork
{
    private readonly ToDoListContext _context;
    private TaskRepository? _taskRepository;

    public UnitOfWork(ToDoListContext context) : base(context)
    {
        _context = context;
    }

    public TaskRepository TaskRepository => _taskRepository ??= new TaskRepository(_context);
}