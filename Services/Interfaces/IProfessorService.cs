using Diligent.MinimalAPI.Models;

namespace Diligent.MinimalAPI.Services
{
    public interface IProfessorService
    {
        Task<bool> CreateAsync(Professor professor);
        Task<bool> UpdateAsync(Professor professor);
        Task<bool> DeleteAsync(string JMBG);
        Task<List<Professor>> GetAllAsync();
        Task<Professor> GetByJMBGAsync(string JMBG);
    }
}
