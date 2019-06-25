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
    public class EFNotesService : INotes
    {
        protected readonly ApplicationDbContext _context;

        public EFNotesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Note> GetAsync(int id)
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note> AddAsync(string aspNetUser, Note model)
        {
            var currentUser = await _context.Students.FirstOrDefaultAsync(s => s.AspNetUserId == aspNetUser);
            model.CreateDate = DateTime.UtcNow;
            model.StudentId = currentUser.Id;

            _context.Notes.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Note> EditAsync(Note model)
        {
            var modelToEdit = await _context.Notes.FirstOrDefaultAsync(n => n.Id == model.Id);
            modelToEdit.NoteSummary = model.NoteSummary;
            modelToEdit.Title = model.Title;
            modelToEdit.CreateDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return modelToEdit;
        }

        public async Task DeleteAsync(Note model)
        {
            var modelToDelete = await _context.Notes.FirstOrDefaultAsync(n => n.Id == model.Id);
            _context.Notes.Remove(modelToDelete);
            await _context.SaveChangesAsync();
        }
    }
}