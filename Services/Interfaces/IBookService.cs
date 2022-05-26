using Diligent.MinimalAPI.Models;

namespace Diligent.MinimalAPI.Services.Interfaces
{
    public interface IBookService
    {

        Task<bool> CreateAsync(Book book);
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
    }
}
