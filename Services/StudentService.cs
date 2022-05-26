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

        public async Task<bool> CreateAsync(Student student)
        {
            var existingStudent = await GetByIndexAsync(student.IndexNum);
            if (existingStudent != null)
            {
                return false;
            }

            await _facultyContext.Students.AddAsync(student);
            return await _facultyContext.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteAsync(int index)
        {
            var student = await GetByIndexAsync(index);
            if (student == null)
            {
                return false;
            }

            _facultyContext.Remove(student);
            return await _facultyContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            
            return await _facultyContext.Students.ToListAsync();
        }

        public async Task<Student> GetByIndexAsync(int index)
        {
            return await _facultyContext.Students.Where(x => x.IndexNum == index).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(Student student)
        {
            var existingStudent = await GetByIndexAsync(student.IndexNum);
            if (existingStudent == null)
            {
                return false;
            }

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Address = student.Address;
            existingStudent.Semester = student.Semester;
            existingStudent.IndexNum = student.IndexNum;

            _facultyContext.Update(existingStudent);
            return await _facultyContext.SaveChangesAsync() > 0;
        }
    }
}
