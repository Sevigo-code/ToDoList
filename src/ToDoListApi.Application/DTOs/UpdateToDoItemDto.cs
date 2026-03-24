namespace ToDoListApi.Application.DTOs;

public class UpdateToDoItemDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime MaxCompletionDate { get; set; }
    public bool IsCompleted { get; set; }
}
