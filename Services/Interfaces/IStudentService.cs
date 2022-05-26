using Diligent.MinimalAPI.Models;

namespace Diligent.MinimalAPI.Services
{
    public interface IStudentService
    {
        Task<bool> CreateAsync(Student student);
        Task<bool> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int index);
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIndexAsync(int index);
    }
}
