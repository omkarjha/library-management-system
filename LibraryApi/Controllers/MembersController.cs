using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Librarian")]
public class MembersController : ControllerBase
{
    private readonly IMemberRepository _repo;

    public MembersController(IMemberRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<List<Member>>> GetAll() =>
        Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Member>> GetById(string id)
    {
        var member = await _repo.GetByIdAsync(id);
        return member is null ? NotFound() : Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Member member)
    {
        await _repo.CreateAsync(member);
        return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Member member)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();
        member.Id = id;
        await _repo.UpdateAsync(id, member);
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