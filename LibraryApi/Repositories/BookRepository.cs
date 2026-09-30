using LibraryApi.Data;
using LibraryApi.Models;
using MongoDB.Driver;

namespace LibraryApi.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _books;

    public BookRepository(MongoDbContext context)
    {
        _books = context.Books;
    }

    public async Task<List<Book>> GetAllAsync() =>
        await _books.Find(_ => true).ToListAsync();

    public async Task<Book?> GetByIdAsync(string id) =>
        await _books.Find(b => b.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Book book) =>
        await _books.InsertOneAsync(book);

    public async Task UpdateAsync(string id, Book book) =>
        await _books.ReplaceOneAsync(b => b.Id == id, book);

    public async Task DeleteAsync(string id) =>
        await _books.DeleteOneAsync(b => b.Id == id);
}