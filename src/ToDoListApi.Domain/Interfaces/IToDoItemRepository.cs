using ToDoListApi.Domain.Entities;

namespace ToDoListApi.Domain.Interfaces;

public interface IToDoItemRepository
{
    Task<IEnumerable<ToDoItem>> GetAllAsync();
    Task<ToDoItem?> GetByIdAsync(int id);
    Task<ToDoItem> CreateAsync(ToDoItem item);
    Task<ToDoItem?> UpdateAsync(ToDoItem item);
    Task<bool> DeleteAsync(int id);
}
