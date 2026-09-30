using LibraryApi.Data;
using LibraryApi.Models;
using MongoDB.Driver;

namespace LibraryApi.Services;

public class BorrowService : IBorrowService
{
    private readonly MongoDbContext _context;

    public BorrowService(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<string> BorrowBookAsync(string bookId, string memberId)
    {
        var book = await _context.Books.Find(b => b.Id == bookId).FirstOrDefaultAsync();
        if (book is null) throw new KeyNotFoundException("Book not found");
        if (book.AvailableCopies <= 0) throw new InvalidOperationException("No copies available");

        // decrement available copies
        var update = Builders<Book>.Update.Inc(b => b.AvailableCopies, -1);
        await _context.Books.UpdateOneAsync(b => b.Id == bookId, update);

        var record = new BorrowRecord { BookId = bookId, MemberId = memberId };
        await _context.BorrowRecords.InsertOneAsync(record);

        return record.Id;
    }

    public async Task ReturnBookAsync(string borrowRecordId)
    {
        var record = await _context.BorrowRecords.Find(r => r.Id == borrowRecordId).FirstOrDefaultAsync();
        if (record is null || record.ReturnedAt is not null)
            throw new InvalidOperationException("Invalid or already-returned record");

        var updateRecord = Builders<BorrowRecord>.Update.Set(r => r.ReturnedAt, DateTime.UtcNow);
        await _context.BorrowRecords.UpdateOneAsync(r => r.Id == borrowRecordId, updateRecord);

        var updateBook = Builders<Book>.Update.Inc(b => b.AvailableCopies, 1);
        await _context.Books.UpdateOneAsync(b => b.Id == record.BookId, updateBook);
    }
}