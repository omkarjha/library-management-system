using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BorrowController : ControllerBase
{
    private readonly IBorrowService _service;
    public BorrowController(IBorrowService service) => _service = service;

    public record BorrowRequest(string BookId, string MemberId);

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