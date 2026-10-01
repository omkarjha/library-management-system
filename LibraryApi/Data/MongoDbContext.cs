using LibraryApi.Models;
using LibraryApi.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibraryApi.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<Book> Books => _database.GetCollection<Book>("Books");
    public IMongoCollection<Member> Members => _database.GetCollection<Member>("Members");
    public IMongoCollection<BorrowRecord> BorrowRecords => _database.GetCollection<BorrowRecord>("BorrowRecords");
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}