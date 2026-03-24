namespace ToDoListApi.Application.DTOs;

public class CreateToDoItemDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime MaxCompletionDate { get; set; }
}
