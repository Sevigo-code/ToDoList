using ToDoListApi.Application.DTOs;

namespace ToDoListApi.Application.Interfaces;

public interface IToDoItemService
{
    Task<IEnumerable<ToDoItemDto>> GetAllAsync();
    Task<ToDoItemDto?> GetByIdAsync(int id);
    Task<ToDoItemDto> CreateAsync(CreateToDoItemDto dto);
    Task<ToDoItemDto?> UpdateAsync(int id, UpdateToDoItemDto dto);
    Task<bool> DeleteAsync(int id);
}
