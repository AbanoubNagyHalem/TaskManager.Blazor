namespace TaskManager.Blazor.Models;

public class TaskResponse
{
  public int Id { get; set; }


  public string Title { get; set; } =
      "";


  public string? Description { get; set; }


  public DateTime? DueDate { get; set; }


  public TaskPriority Priority { get; set; }


  public TaskItemStatus Status { get; set; }


  public DateTime CreatedAtUtc { get; set; }


  public int UserId { get; set; }
}