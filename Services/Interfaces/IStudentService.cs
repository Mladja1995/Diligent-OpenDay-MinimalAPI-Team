using Diligent.MinimalAPI.Models;

namespace Diligent.MinimalAPI.Services
{
    public interface IStudentService
    {
        Task<bool> CreateStudentAsync(Student student);
    }
}
