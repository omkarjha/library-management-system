using LibraryApi.Models;
using LibraryApi.Repositories;
using LibraryApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BorrowController : ControllerBase
{
    private readonly IBorrowService _service;
    private readonly IBorrowRecordRepository _repo;

    public BorrowController(IBorrowService service, IBorrowRecordRepository repo)
    {
        _service = service;
        _repo = repo;
    }

    public record BorrowRequest(string BookId, string MemberId);

    [Authorize(Roles = "Admin,Librarian")]
    [HttpGet]
    public async Task<ActionResult<List<BorrowRecord>>> GetAll() =>
        Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Borrow(BorrowRequest request)
    {
        try
        {
            var id = await _service.BorrowBookAsync(request.BookId, request.MemberId);
            return Ok(new { borrowRecordId = id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> Return(string id)
    {
        try
        {
            await _service.ReturnBookAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}