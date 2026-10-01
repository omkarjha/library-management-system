using LibraryApi.Models;

namespace LibraryApi.Repositories;

public interface IBorrowRecordRepository
{
    Task<List<BorrowRecord>> GetAllAsync();
    Task<BorrowRecord?> GetByIdAsync(string id);
    Task CreateAsync(BorrowRecord record);
    Task UpdateAsync(string id, BorrowRecord record);
    Task DeleteAsync(string id);
}