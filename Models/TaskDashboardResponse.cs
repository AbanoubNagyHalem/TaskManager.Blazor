namespace TaskManager.Blazor.Models;

public class TaskDashboardResponse
{
  public int TotalTasks { get; set; }


  public int PendingTasks { get; set; }


  public int InProgressTasks { get; set; }


  public int CompletedTasks { get; set; }


  public int HighPriorityTasks { get; set; }


  public int OverdueTasks { get; set; }
}