using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medicare.Repository.DbContext;
using Medicare.Repository.Entity;
using Medicare.Repository.Interfaces;

namespace Medicare.Repository.Repository
{
    public class DoctorsEducationRepository : IDoctorsEducationRepository
    {
        private readonly MedicareDbContext _context;

        public DoctorsEducationRepository(MedicareDbContext context)
        {
            _context = context;
        }
        public async Task<DoctorsEducation?> AddAsync(DoctorsEducation education)
        {
            await _context.DoctorsEducations.AddAsync(education);
            int retVal = await _context.SaveChangesAsync();
            if(retVal <= 0) return null;
            return education;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.DoctorsEducations.FindAsync(id);
            if (result == null) return false;
            _context.DoctorsEducations.Remove(result);
            int retVal = await _context.SaveChangesAsync();
            return retVal >0;
        }

        public async Task<DoctorsEducation> GetByIdAsync(int id)
        {
            return await _context.DoctorsEducations.FindAsync(id);
        }

        public async Task<DoctorsEducation> UpdateAsync(DoctorsEducation education)
        {
            var result = await _context.DoctorsEducations.FindAsync(education.Id);
            if(result == null) return null;
            result.Degree = education.Degree;
            result.Institution = education.Institution;
            result.FieldOfStudy = education.FieldOfStudy;
            result.YearOfCompletion = education.YearOfCompletion;
            result.Notes = education.Notes;
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
