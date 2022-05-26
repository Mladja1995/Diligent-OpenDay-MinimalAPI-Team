using Diligent.MinimalAPI.Database;
using Diligent.MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Diligent.MinimalAPI.Services.Interfaces
{
    public class CourseService : ICourseService
    {
        private readonly FacultyContext _facultyContext;

        public CourseService(FacultyContext facultyContext)
        {
            _facultyContext = facultyContext;
        }

        public async Task<bool> CreateAsync(Course course)
        {
            var exsistingCourse = await GetByCodeAsync(course.Code);
            if (exsistingCourse != null)
            {
                return false;
            }
            await _facultyContext.AddAsync(course);
            return await _facultyContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var course = await GetByCodeAsync(code);
            if (course == null)
            {
                return false;
            }
            _facultyContext.Remove(course);
            return await _facultyContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _facultyContext.Courses.ToListAsync();
        }

        public async Task<Course> GetByCodeAsync(string code)
        {
            return await _facultyContext.Courses.Where(a => a.Code == code).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(Course course)
        {
            var existingCourse = await GetByCodeAsync(course.Code);
            if (existingCourse == null)
            {
                return false;
            }
            existingCourse.Name = course.Name;
            existingCourse.Points = course.Points;
            existingCourse.Semester = course.Semester;
            existingCourse.Code = course.Code;
            existingCourse.IsOptional = course.IsOptional;

            _facultyContext.Update(existingCourse);
            return await _facultyContext.SaveChangesAsync() > 0;
        }
    }
}
