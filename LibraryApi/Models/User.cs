using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryApi.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = "Member"; // Admin, Librarian, Member
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}