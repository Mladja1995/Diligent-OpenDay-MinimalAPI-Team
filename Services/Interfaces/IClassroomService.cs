using Diligent.MinimalAPI.Models;

namespace Diligent.MinimalAPI.Services.Interfaces
{
    public interface IClassroomService
    {
        Task<bool> CreateAsync(Classroom classroom);
        Task<bool> UpdateAsync(Classroom classroom);
        Task<bool> DeleteAsync(string identifier);
        Task<List<Classroom>> GetAllAsync();
        Task<Classroom> GetByIdentifierAsync(string identifier);
    }
}
