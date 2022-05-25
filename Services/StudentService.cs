using Diligent.MinimalAPI.Database;
using Diligent.MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Diligent.MinimalAPI.Services
{
   
    public class StudentService : IStudentService
    {
        private readonly FacultyContext _facultyContext;
        public StudentService(FacultyContext facultyContext)
        {
            _facultyContext = facultyContext;
        }

        public async Task<bool> CreateStudentAsync(Student student)
        {
            await _facultyContext.Students.AddAsync(student);
            return await _facultyContext.SaveChangesAsync() > 0;
        }
    }
}
