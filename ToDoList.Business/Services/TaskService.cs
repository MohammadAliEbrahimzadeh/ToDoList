using Microsoft.EntityFrameworkCore;
using System.Net;
using ToDoList.Business.Contracts;
using ToDoList.Common.DTOs;
using ToDoList.DataAccess;


namespace ToDoList.Business.Services;

public class TaskService : ITaskService
{
    private readonly UnitOfWork _unitOfWork;

    public TaskService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = (UnitOfWork)unitOfWork;
    }


    public async Task<CustomResponse> AddAsync(AddTaskDto dto, CancellationToken cancellationToken)
    {
        var newTask = new ToDoList.Model.Entities.Task()
        {
            Description = dto.Description,
            IsCompleted = false,
            Title = dto.Title,
            DueDate = dto.DueDate
        };

        await _unitOfWork.AddAsync(newTask, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomResponse(message: "Task was added successfully");
    }

    public async Task<CustomResponse> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.FindByIdAsync<Model.Entities.Task>(id, cancellationToken);

        if (task is null)
            return new CustomResponse(message: "Task wasnt found successfully", statusCode: HttpStatusCode.NotFound);

        _unitOfWork.Delete(task);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomResponse(message: "Task was deleted successfully");
    }

    public async Task<CustomResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.FindByIdAsync<Model.Entities.Task>(id, cancellationToken);

        if (task is null)
            return new CustomResponse(message: "Task wasnt found successfully", statusCode: HttpStatusCode.NotFound);

        var taskDto = new GetTaskDto()
        {
            Description = task.Description,
            DueDate = task.DueDate,
            Title = task.Title
        };

        return new CustomResponse(data: taskDto, message: "Task was added successfully");
    }

    public async Task<CustomResponse> GetTasksAsync(FilterGetTaskDto dto, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.GetAsQueryable<Model.Entities.Task>(cancellationToken);

        if (!string.IsNullOrEmpty(dto.Title))
            query = query.Where(t => t.Title!.Contains(dto.Title));

        if (!string.IsNullOrEmpty(dto.Description))
            query = query.Where(t => t.Description!.Contains(dto.Description));

        if (dto.FromDate.HasValue)
            query = query.Where(t => t.DueDate >= dto.FromDate.Value);

        if (dto.ToDate.HasValue)
            query = query.Where(t => t.DueDate <= dto.ToDate.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var tasks = await query
            .Skip((dto.Page - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync(cancellationToken);


        var list = new List<GetTaskDto>();

        foreach (var item in tasks)
        {
            var data = new GetTaskDto()
            {
                Description = item.Description,
                DueDate = item.DueDate,
                Title = item.Title
            };

            list.Add(data);
        }

        return new CustomResponse() { Data = list };
    }

    public async Task<CustomResponse> UpdateAsync(Guid id, UpdateTaskDto dto, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.FindByIdAsync<Model.Entities.Task>(id, cancellationToken);

        if (task is null)
            return new CustomResponse(message: "Task wasnt found successfully", statusCode: HttpStatusCode.NotFound);

        task.DueDate = dto.DueDate;
        task.Description = dto.Description;
        task.Title = dto.Title;

        _unitOfWork.Update(task);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomResponse(message: "Task was updated successfully");
    }
}
