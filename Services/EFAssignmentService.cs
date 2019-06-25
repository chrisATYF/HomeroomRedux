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
    public class EFAssignmentService : IAssignment
    {
        protected readonly ApplicationDbContext _context;

        public EFAssignmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Assignment> GetAsync(int id)
        {
            return await _context.Assignments
                .Include(u => u.Submissions)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Assignment>> GetAllAssignmentsAsync()
        {
            return await _context.Assignments.ToListAsync();
        }

        public async Task<Assignment> AddAsync(Assignment model)
        {
            model.AssignDate = DateTime.UtcNow;

            _context.Assignments.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Assignment> EditAsync(Assignment model)
        {
            var modelToEdit = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == model.Id);
            modelToEdit.Description = model.Description;
            modelToEdit.Name = model.Name;
            modelToEdit.DueDate = model.DueDate;
            modelToEdit.AssignDate = DateTime.UtcNow;
            modelToEdit.TotalPointsAvailable = model.TotalPointsAvailable;

            await _context.SaveChangesAsync();

            return modelToEdit;
        }

        public async Task DeleteAsync(Assignment model)
        {
            var modelToDelete = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == model.Id);

            _context.Assignments.Remove(modelToDelete);
            await _context.SaveChangesAsync();
        }
    }
}