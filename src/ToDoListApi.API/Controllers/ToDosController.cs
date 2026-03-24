using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Application.DTOs;
using ToDoListApi.Application.Interfaces;

namespace ToDoListApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDosController : ControllerBase
{
    private readonly IToDoItemService _service;

    public ToDosController(IToDoItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItemDto>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null)
            return NotFound(new { message = $"Tarea con Id {id} no encontrada." });

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItemDto>> Create([FromBody] CreateToDoItemDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ToDoItemDto>> Update(int id, [FromBody] UpdateToDoItemDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (updated is null)
            return NotFound(new { message = $"Tarea con Id {id} no encontrada." });

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result)
            return NotFound(new { message = $"Tarea con Id {id} no encontrada." });

        return NoContent();
    }
}
