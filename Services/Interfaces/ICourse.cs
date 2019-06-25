using HomeroomRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeroomRedux.Services.Interfaces
{
    public interface ICourse
    {
        Task<Course> GetAsync(int? id);
        Task<List<Course>> GetStudentCoursesByAspNetIdAsync(string id);
        Task<List<Course>> GetInstructorCoursesByAspNetIdAsync(string id);
        Task<List<Course>> GetPopularCoursesAsync(string id);
        Task AddStudentAsync(int id, string aspNetUserId);
        Task AddMessageStudentAsync(int id, string aspNetUserId, string message);
        Task AddMessageInstructorAsync(int id, string aspNetUserId, string message);
        Task<Course> CreateAsync(Course model);
        Task<Course> EditAsync(int id, Course model);
        Task DeleteAsync(Course model);
        Task AddAboutCourseAsync(Course model);
    }
}