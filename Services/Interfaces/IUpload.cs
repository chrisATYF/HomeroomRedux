using HomeroomRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeroomRedux.Services.Interfaces
{
    public interface IUpload
    {
        Task<SubmissionFile> GetAsync(int id);
        Task<List<SubmissionFile>> GetAllFilesAsync();
        Task DeleteAsync(int id, SubmissionFile model);
        Task<Student> GetStudentIdAsync(string aspNetUserId);
        Task AddAsync(SubmissionFile model);
    }
}