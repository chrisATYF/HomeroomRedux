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
    public class EFSubmissionService : ISubmissions
    {
        protected readonly ApplicationDbContext _context;

        public EFSubmissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Submission> GetAsync(int assignmentId)
        {
            return await _context.Submissions
                .Include(f => f.SubmissionFiles)
                .FirstOrDefaultAsync(i => i.AssignmentId == assignmentId);
        }

        public async Task<List<Submission>> GetAllAsync(int assignmentId)
        {
            return await _context.Submissions
                .Include(s => s.SubmissionFiles)
                .Include(n => n.Assignment)
                .Include(x => x.Student)
                .Where(a => a.AssignmentId == assignmentId)
                .ToListAsync();
        }

        public async Task<Submission> GetSubmissionAsync(int submissionId)
        {
            var model = await _context.Submissions
                .Include(s => s.SubmissionFiles)
                .Include(i => i.Student)
                .Where(a => a.Id == submissionId)
                .FirstOrDefaultAsync(i => i.Id == submissionId);

            return model;
        }

        public async Task<Submission> GetUnsubmittedSubmission(int assignmentId)
        {
            return await _context.Submissions
                .FirstOrDefaultAsync(a => a.AssignmentId == assignmentId && !a.IsSubmitted);
        }

        public async Task<Submission> AddAsync(Submission model)
        {
            _context.Submissions.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Submission> SubmitAsync(Submission model)
        {
            await _context.SaveChangesAsync();

            return model;
        }
    }
}