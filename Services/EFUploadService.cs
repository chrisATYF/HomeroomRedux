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
    public class EFUploadService : IUpload
    {
        protected readonly ApplicationDbContext _context;

        public EFUploadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubmissionFile>> GetAllFilesAsync()
        {
            var models = await _context.SubmissionFiles.ToListAsync();

            return models;
        }

        public async Task<SubmissionFile> GetAsync(int id)
        {
            var model = await _context.SubmissionFiles.FirstOrDefaultAsync(u => u.Id == id);

            return model;
        }

        public async Task DeleteAsync(int id, SubmissionFile model)
        {
            var modelToDelete = await _context.SubmissionFiles.FirstOrDefaultAsync(u => u.Id == id);

            _context.SubmissionFiles.Remove(modelToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(SubmissionFile model)
        {
            _context.SubmissionFiles.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> GetStudentIdAsync(string aspNetUserId)
        {
            var studentById = await _context.Students.FirstOrDefaultAsync(s => s.AspNetUserId == aspNetUserId);

            return studentById;
        }
    }
}