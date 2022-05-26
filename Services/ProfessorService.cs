using Diligent.MinimalAPI.Database;
using Diligent.MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Diligent.MinimalAPI.Services
{
    public class ProfessorService:IProfessorService
    {
        private readonly FacultyContext _facultyContext;
        public ProfessorService(FacultyContext facultyContext)
        {
            _facultyContext = facultyContext;
        }

        public async Task<bool> CreateAsync(Professor professor)
        {
            var prof = await GetByJMBGAsync(professor.JMBG);
            if (prof != null)
            {
                return false;
            }

            await _facultyContext.Professors.AddAsync(professor);
            return await _facultyContext.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteAsync(string JMBG)
        {
            var prof = await GetByJMBGAsync(JMBG);
            if (prof == null)
            {
                return false;
            }

            _facultyContext.Remove(prof);
            return await _facultyContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Professor>> GetAllAsync()
        {

            return await _facultyContext.Professors.ToListAsync();
        }

        public async Task<Professor> GetByJMBGAsync(string JMBG)
        {
            return await _facultyContext.Professors.Where(x => x.JMBG == JMBG).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(Professor professor)
        {
            var prof = await _facultyContext.Professors.Where(x => x.JMBG == professor.JMBG).FirstOrDefaultAsync();
            if (prof == null)
            {
                return false;
            }

            prof.FirstName = professor.FirstName;
            prof.LastName = professor.LastName;
            prof.JMBG = professor.JMBG;
            prof.CourseID = professor.CourseID;
            prof.YearsOfExperience = professor.YearsOfExperience;

            _facultyContext.Update(prof);
            return await _facultyContext.SaveChangesAsync() > 0;
        }
    }
}
