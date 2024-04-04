using CurrenciesManager.Infra;

namespace BooksManager.Interfaces
{
    public interface IBookManager
    {
        Task<bool> AddBook(string bookXmlElem);
        Task<List<BookModel>> GetAll();
        Task<BookModel> GetBookById(string id);
    }
}
