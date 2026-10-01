using LibraryApi.Models;

namespace LibraryApi.Repositories;

public interface IMemberRepository
{
    Task<List<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(string id);
    Task CreateAsync(Member member);
    Task UpdateAsync(string id, Member member);
    Task DeleteAsync(string id);
}