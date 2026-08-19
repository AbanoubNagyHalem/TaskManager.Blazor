using System.ComponentModel.DataAnnotations;

namespace TaskManager.Blazor.Models;

public class UpdateTaskRequest
{
  [Required(
      ErrorMessage = "Title is required.")]
  [StringLength(
      150,
      MinimumLength = 3,
      ErrorMessage =
          "Title must be between 3 and 150 characters.")]
  public string Title { get; set; } =
      "";


  [StringLength(
      1000,
      ErrorMessage =
          "Description cannot exceed 1000 characters.")]
  public string? Description { get; set; }


  public DateTime? DueDate { get; set; }


  public TaskPriority Priority { get; set; } =
      TaskPriority.Medium;


  public TaskItemStatus Status { get; set; } =
      TaskItemStatus.Pending;
}