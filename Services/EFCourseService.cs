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
    public class EFCourseService : ICourse
    {
        protected readonly ApplicationDbContext _context;

        public EFCourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Course> GetAsync(int? id)
        {
            return await _context.Courses
                .Include(i => i.Instructor)
                .Include(s => s.Students)
                .Include(d => d.Discussions.Select(s => s.Student))
                .Include(a => a.Assignments.Select(s => s.Submissions))
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Course>> GetStudentCoursesByAspNetIdAsync(string id)
        {
            return await _context.Courses
                .Include(i => i.Instructor)
                .Include(s => s.Students)
                .Where(c => c.Students.Any(a => a.AspNetUserId == id))
                .ToListAsync();
        }

        public async Task<List<Course>> GetInstructorCoursesByAspNetIdAsync(string id)
        {
            return await _context.Courses.Include(i => i.Instructor).Include(s => s.Students).ToListAsync();
        }

        public async Task<Course> CreateAsync(Course model)
        {
            _context.Courses.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Course> EditAsync(int id, Course model)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            course.Name = model.Name;
            course.Description = model.Description;
            course.AboutCourse = model.AboutCourse;
            course.WhatYouLearn = model.WhatYouLearn;

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task DeleteAsync(Course model)
        {
            _context.Courses.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Course>> GetPopularCoursesAsync(string id)
        {
            return await _context.Courses
                .Include(i => i.Instructor)
                .Include(s => s.Students)
                .Where(c => !c.Students.Any(a => a.AspNetUserId == id))
                //.Where(c => c.Students.All(a => a.AspNetUserId != id))
                .ToListAsync();
        }

        public async Task AddStudentAsync(int id, string aspNetUserId)
        {
            var course = await _context.Courses.Include(s => s.Students).FirstOrDefaultAsync(c => c.Id == id);
            if (course.Students.Any(a => a.AspNetUserId == aspNetUserId))
            {
                return;
            }
            var student = await _context.Students.FirstOrDefaultAsync(s => s.AspNetUserId == aspNetUserId);

            course.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task AddMessageStudentAsync(int id, string aspNetUserId, string message)
        {
            var course = await _context.Courses.Include(s => s.Students).FirstOrDefaultAsync(c => c.Id == id);
            var student = await _context.Students.FirstOrDefaultAsync(s => s.AspNetUserId == aspNetUserId);
            var instructor = await _context.Instructors.FirstOrDefaultAsync(a => a.AspNetUserId == aspNetUserId);
            var model = new Discussion
            {
                CourseId = course.Id,
                StudentId = student.Id,
                Message = message,
                CreateDate = DateTime.UtcNow,
            };

            _context.Discussions.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task AddMessageInstructorAsync(int id, string aspNetUserId, string message)
        {
            var course = await _context.Courses.Include(s => s.Students).FirstOrDefaultAsync(c => c.Id == id);
            var instructor = await _context.Instructors.FirstOrDefaultAsync(a => a.AspNetUserId == aspNetUserId);
            var model = new Discussion
            {
                CourseId = course.Id,
                StudentId = instructor.Id,
                Message = message,
                CreateDate = DateTime.UtcNow,
            };

            _context.Discussions.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task AddAboutCourseAsync(Course model)
        {
            var modelToAdd = await _context.Courses.FirstOrDefaultAsync(i => i.Id == model.Id);
            modelToAdd.AboutCourse = model.AboutCourse;

            await _context.SaveChangesAsync();
        }
    }
}