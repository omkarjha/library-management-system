namespace LibraryApi.Services;

public interface IBorrowService
{
    Task<string> BorrowBookAsync(string bookId, string memberId);
    Task ReturnBookAsync(string borrowRecordId);
}