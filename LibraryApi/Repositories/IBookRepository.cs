using LibraryApi.Models;

namespace LibraryApi.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(string id);
    Task CreateAsync(Book book);
    Task UpdateAsync(string id, Book book);
    Task DeleteAsync(string id);
}