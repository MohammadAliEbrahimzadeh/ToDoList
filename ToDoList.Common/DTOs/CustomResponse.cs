using System.Net;

namespace ToDoList.Common.DTOs;

public class CustomResponse
{
    public dynamic? Data { get; set; }

    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    public int TotalCount { get; set; }

    public CustomResponse(dynamic? data = null, bool isSuccess = true, string? message = null
        , HttpStatusCode statusCode = HttpStatusCode.OK, int totalCount = 0)
    {
        Data = data;
        IsSuccess = isSuccess;
        Message = message;
        StatusCode = statusCode;
        TotalCount = totalCount;
    }
}