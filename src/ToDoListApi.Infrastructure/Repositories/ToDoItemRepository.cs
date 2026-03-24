using Microsoft.EntityFrameworkCore;
using ToDoListApi.Domain.Entities;
using ToDoListApi.Domain.Interfaces;
using ToDoListApi.Infrastructure.Data;

namespace ToDoListApi.Infrastructure.Repositories;

public class ToDoItemRepository : IToDoItemRepository
{
    private readonly AppDbContext _context;

    public ToDoItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ToDoItem>> GetAllAsync()
    {
        return await _context.ToDoItems
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<ToDoItem?> GetByIdAsync(int id)
    {
        return await _context.ToDoItems.FindAsync(id);
    }

    public async Task<ToDoItem> CreateAsync(ToDoItem item)
    {
        _context.ToDoItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<ToDoItem?> UpdateAsync(ToDoItem item)
    {
        _context.ToDoItems.Update(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item is null) return false;

        _context.ToDoItems.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}
