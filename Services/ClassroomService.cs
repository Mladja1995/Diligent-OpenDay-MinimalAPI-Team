using Diligent.MinimalAPI.Database;
using Diligent.MinimalAPI.Models;
using Diligent.MinimalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diligent.MinimalAPI.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly FacultyContext _facultyContext;
        public ClassroomService(FacultyContext facultyContext)
        {
            _facultyContext = facultyContext;
        }

        public async Task<bool> CreateAsync(Classroom classroom)
        {
            var existingClassroom = await GetByIdentifierAsync(classroom.Identifier);
            if (existingClassroom != null)
            {
                return false;
            }
            await _facultyContext.Classrooms.AddAsync(classroom);
            return await _facultyContext.SaveChangesAsync() > 0;
        }
        public async Task<Classroom> GetByIdentifierAsync(string Identifier)
        {
            return await _facultyContext.Classrooms.Where(x => x.Identifier == Identifier).FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteAsync(string identifier)
        {
            var classroom = await GetByIdentifierAsync(identifier);
            if (classroom == null)
            {
                return false;
            }

            _facultyContext.Remove(classroom);
            return await _facultyContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Classroom>> GetAllAsync()
        {
            return await _facultyContext.Classrooms.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Classroom classroom)
        {
            var existingClassroom = await GetByIdentifierAsync(classroom.Identifier);
            if (existingClassroom == null)
            {
                return false;
            }

            existingClassroom.Identifier = classroom.Identifier;
            existingClassroom.Capacity = classroom.Capacity;
            existingClassroom.Floor = classroom.Floor;
            existingClassroom.Sector = classroom.Sector;
            existingClassroom.IsCopmuterLab = classroom.IsCopmuterLab;

            _facultyContext.Update(existingClassroom);
            return await _facultyContext.SaveChangesAsync() > 0;
        }

    }
}
