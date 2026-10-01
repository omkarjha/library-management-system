using LibraryApi.Data;
using LibraryApi.Models;
using MongoDB.Driver;

namespace LibraryApi.Repositories;

public class BorrowRecordRepository : IBorrowRecordRepository
{
    private readonly IMongoCollection<BorrowRecord> _records;

    public BorrowRecordRepository(MongoDbContext context)
    {
        _records = context.BorrowRecords;
    }

    public async Task<List<BorrowRecord>> GetAllAsync() =>
        await _records.Find(_ => true).ToListAsync();

    public async Task<BorrowRecord?> GetByIdAsync(string id) =>
        await _records.Find(m => m.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(BorrowRecord record) =>
        await _records.InsertOneAsync(record);

    public async Task UpdateAsync(string id, BorrowRecord record) =>
        await _records.ReplaceOneAsync(m => m.Id == id, record);

    public async Task DeleteAsync(string id) =>
        await _records.DeleteOneAsync(m => m.Id == id);
}