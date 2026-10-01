using LibraryApi.Data;
using LibraryApi.Models;
using MongoDB.Driver;

namespace LibraryApi.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly IMongoCollection<Member> _members;

    public MemberRepository(MongoDbContext context)
    {
        _members = context.Members;
    }

    public async Task<List<Member>> GetAllAsync() =>
        await _members.Find(_ => true).ToListAsync();

    public async Task<Member?> GetByIdAsync(string id) =>
        await _members.Find(m => m.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Member member) =>
        await _members.InsertOneAsync(member);

    public async Task UpdateAsync(string id, Member member) =>
        await _members.ReplaceOneAsync(m => m.Id == id, member);

    public async Task DeleteAsync(string id) =>
        await _members.DeleteOneAsync(m => m.Id == id);
}   