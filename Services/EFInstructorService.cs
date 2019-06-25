using HomeroomRedux.Models;
using HomeroomRedux.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeroomRedux.Services
{
    public class EFInstructorService : IInstructor
    {
        protected readonly ApplicationDbContext _context;

        public EFInstructorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Instructor> GetAsync(int instructorId)
        {
            var model = await _context.Instructors.FirstOrDefaultAsync(i => i.Id == instructorId);

            return model;
        }

        public async Task AddAboutAsync(Instructor model)
        {
            var modelInstructor = await _context.Instructors.FirstOrDefaultAsync(i => i.Id == model.Id);
            modelInstructor.AboutInstructor = model.AboutInstructor;

            await _context.SaveChangesAsync();
        }
    }
}