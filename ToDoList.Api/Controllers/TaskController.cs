using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ToDoList.Business.Contracts;
using ToDoList.Common.DTOs;


namespace ToDoList.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ITaskService _taskService;

    public TaskController(IConfiguration configuration, ITaskService taskService)
    {
        _configuration = configuration;
        _taskService = taskService;
    }


    [HttpPost]
    [Route("AddTask")]
    public async Task<CustomResponse> AddTaskAsync(AddTaskDto dto, CancellationToken cancellationToken)
    {
        return await _taskService.AddAsync(dto, cancellationToken);
    }

    [HttpGet]
    [Route("GetTaskById/{id}")]
    public async Task<CustomResponse> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _taskService.GetByIdAsync(id, cancellationToken);
    }


    [HttpGet]
    [Route("GetTasks")]
    [ResponseCache(Duration = 30)]
    public async Task<CustomResponse> GetTasksAsync([FromQuery] FilterGetTaskDto dto, CancellationToken cancellationToken)
    {
        return await _taskService.GetTasksAsync(dto, cancellationToken);
    }

    [HttpPut]
    [Route("UpdateTasks/{id}")]
    public async Task<CustomResponse> UpdateTasks(Guid id, UpdateTaskDto dto, CancellationToken cancellationToken)
    {
        return await _taskService.UpdateAsync(id, dto, cancellationToken);
    }

    [HttpDelete]
    [Route("DeletedTaskById/{id}")]
    public async Task<CustomResponse> DeletedTaskByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _taskService.DeleteAsync(id, cancellationToken);
    }


}
