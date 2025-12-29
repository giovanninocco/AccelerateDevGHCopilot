using Library.ApplicationCore.Entities;
using Library.ApplicationCore;

namespace Library.Infrastructure.Data;


public class JsonBookRepository : IBookRepository
{
    private readonly JsonData _jsonData;

    public JsonBookRepository(JsonData jsonData)
    {
        _jsonData = jsonData;
    }

    public async Task<List<Book>> SearchBooks(string title)
    {
        await _jsonData.EnsureDataLoaded();

        return _jsonData.Books!
            .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public async Task<Book?> GetBook(int id)
    {
        await _jsonData.EnsureDataLoaded();

        foreach (Book book in _jsonData.Books!)
        {
            if (book.Id == id)
            {
                return _jsonData.GetPopulatedBook(book);
            }
        }
        return null;
    }
}