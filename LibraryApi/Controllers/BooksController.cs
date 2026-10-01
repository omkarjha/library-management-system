using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _repo;

    public BooksController(IBookRepository repo) => _repo = repo;

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetAll() =>
        Ok(await _repo.GetAllAsync());

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetById(string id)
    {
        var book = await _repo.GetByIdAsync(id);
        return book is null ? NotFound() : Ok(book);
    }

    [Authorize(Roles = "Admin,Librarian")]
    [HttpPost]
    public async Task<ActionResult> Create(Book book)
    {
        book.AvailableCopies = book.TotalCopies;
        await _repo.CreateAsync(book);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [Authorize(Roles = "Admin,Librarian")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Book book)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();
        book.Id = id;
        await _repo.UpdateAsync(id, book);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}