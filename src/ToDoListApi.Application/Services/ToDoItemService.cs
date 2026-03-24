using Microsoft.Extensions.Logging;
using ToDoListApi.Application.DTOs;
using ToDoListApi.Application.Interfaces;
using ToDoListApi.Domain.Entities;
using ToDoListApi.Domain.Interfaces;

namespace ToDoListApi.Application.Services;

public class ToDoItemService : IToDoItemService
{
    private readonly IToDoItemRepository _repository;
    private readonly ILogger<ToDoItemService> _logger;

    public ToDoItemService(IToDoItemRepository repository, ILogger<ToDoItemService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<ToDoItemDto>> GetAllAsync()
    {
        _logger.LogInformation("Obteniendo todas las tareas");
        var items = await _repository.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<ToDoItemDto?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Obteniendo tarea con Id: {Id}", id);
        var item = await _repository.GetByIdAsync(id);
        return item is null ? null : MapToDto(item);
    }

    public async Task<ToDoItemDto> CreateAsync(CreateToDoItemDto dto)
    {
        _logger.LogInformation("Creando nueva tarea: {Title}", dto.Title);

        var item = new ToDoItem
        {
            Title = dto.Title,
            Description = dto.Description,
            MaxCompletionDate = dto.MaxCompletionDate,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repository.CreateAsync(item);
        _logger.LogInformation("Tarea creada con Id: {Id}", created.Id);
        return MapToDto(created);
    }

    public async Task<ToDoItemDto?> UpdateAsync(int id, UpdateToDoItemDto dto)
    {
        _logger.LogInformation("Actualizando tarea con Id: {Id}", id);

        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
        {
            _logger.LogWarning("Tarea con Id: {Id} no encontrada para actualizar", id);
            return null;
        }

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.MaxCompletionDate = dto.MaxCompletionDate;
        existing.IsCompleted = dto.IsCompleted;

        var updated = await _repository.UpdateAsync(existing);
        return updated is null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation("Eliminando tarea con Id: {Id}", id);
        var result = await _repository.DeleteAsync(id);

        if (!result)
            _logger.LogWarning("Tarea con Id: {Id} no encontrada para eliminar", id);

        return result;
    }

    private static ToDoItemDto MapToDto(ToDoItem item) => new()
    {
        Id = item.Id,
        Title = item.Title,
        Description = item.Description,
        MaxCompletionDate = item.MaxCompletionDate,
        IsCompleted = item.IsCompleted,
        CreatedAt = item.CreatedAt
    };
}
