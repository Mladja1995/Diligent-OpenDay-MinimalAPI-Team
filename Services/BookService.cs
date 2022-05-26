using Diligent.MinimalAPI.Database;
using Diligent.MinimalAPI.Models;
using Diligent.MinimalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diligent.MinimalAPI.Services
{
    public class BookService : IBookService
    {
        private readonly FacultyContext _facultyContext;
        public BookService(FacultyContext facultyContext)
        {
            _facultyContext = facultyContext;
        }

        public async Task<bool> CreateAsync(Book book)
        {
            var existingBook = await GetByIdAsync(book.Id);
            if (existingBook != null)
            {
                return false;
            }

            await _facultyContext.Book.AddAsync(book);
            return await _facultyContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingBook = await GetByIdAsync(id);
            if (existingBook == null)
            {
                return false;
            }

            _facultyContext.Remove(existingBook);
            return await _facultyContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Book>> GetAllAsync()
        {

            return await _facultyContext.Book.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _facultyContext.Book.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            var existingBook = await GetByIdAsync(book.Id);
            if (existingBook == null)
            {
                return false;
            }

            existingBook.Title = book.Title;
            existingBook.ISBN = book.ISBN;
            existingBook.ReleasedDate = book.ReleasedDate;
            existingBook.Description = book.Description;
            existingBook.Publisher = book.Publisher;

            _facultyContext.Update(existingBook);
            return await _facultyContext.SaveChangesAsync() > 0;
        }
    }
    
}
