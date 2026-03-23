using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApi.Domain.Entities;

namespace ToDoListApi.Domain.Interfaces
{
    internal interface IToDoItemRepository
    {
        Task<IEnumerable<ToDoItem>> GetAllAsync();
        Task<ToDoItem> GetByIdAsync(int id);
        Task<ToDoItem> CreateAsync(ToDoItem item);
        Task<ToDoItem> UpdateAsync(ToDoItem item);
        Task<ToDoItem> DeleteAsync(int id);
    }
}
