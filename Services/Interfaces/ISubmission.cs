using HomeroomRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeroomRedux.Services.Interfaces
{
    public interface ISubmissions
    {
        Task<Submission> GetAsync(int assignmentId);
        Task<List<Submission>> GetAllAsync(int assignmentId);
        Task<Submission> GetSubmissionAsync(int submissionId);
        Task<Submission> GetUnsubmittedSubmission(int assignmentId);
        Task<Submission> AddAsync(Submission model);
        Task<Submission> SubmitAsync(Submission model);
    }
}