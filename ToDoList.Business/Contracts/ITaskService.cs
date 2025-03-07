using ToDoList.Common.DTOs;


namespace ToDoList.Business.Contracts;

public interface ITaskService
{
    Task<CustomResponse> AddAsync(AddTaskDto dto, CancellationToken cancellationToken);

    Task<CustomResponse> UpdateAsync(Guid id, UpdateTaskDto dto, CancellationToken cancellationToken);

    Task<CustomResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<CustomResponse> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<CustomResponse> GetTasksAsync(FilterGetTaskDto dto, CancellationToken cancellationToken);

}
