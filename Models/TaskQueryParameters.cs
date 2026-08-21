namespace TaskManager.Blazor.Models;

public class TaskQueryParameters
{
    public string? Search { get; set; }


    public TaskItemStatus? Status { get; set; }


    public TaskPriority? Priority { get; set; }


    public string? SortBy { get; set; }


    public string SortDirection { get; set; } =
        "asc";


    public int Page { get; set; } =
        1;


    public int PageSize { get; set; } =
        6;
}