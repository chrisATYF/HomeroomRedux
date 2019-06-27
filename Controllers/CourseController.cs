using HomeroomRedux.Models;
using HomeroomRedux.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using HomeroomRedux.ViewModels.Courses;

namespace HomeroomRedux.Controllers
{
    [Authorize]
    [RoutePrefix("Course")]
    public class CourseController : Controller
    {
        protected readonly ICourse _courseService;
        protected readonly ApplicationDbContext _context;

        public CourseController(ICourse courseService, ApplicationDbContext context)
        {
            _courseService = courseService;
            _context = context;
        }

        [Authorize(Roles = Constants.RoleInstructor)]
        [Route("IndexInstructor", Name = "CourseInstructorIndex")]
        public async Task<ActionResult> IndexInstructor()
        {
            var aspNetUserId = User.Identity.GetUserId();
            var model = await _context.Instructors.Include(c => c.Courses).FirstOrDefaultAsync(i => i.AspNetUserId == aspNetUserId);

            return View(model);
        }

        [Authorize(Roles = Constants.RoleStudent)]
        [Route("StudentIndex", Name = "StudentCourseIndex")]
        public async Task<ActionResult> StudentIndex()
        {
            var aspNetUserId = User.Identity.GetUserId();
            var courses = await _courseService.GetStudentCoursesByAspNetIdAsync(aspNetUserId);
            var popularCourses = await _courseService.GetPopularCoursesAsync(aspNetUserId);
            var model = new StudentViewModel
            {
                MyCourses = courses,
                PopularCourses = popularCourses
            };

            return View(model);
        }

        [Authorize(Roles = Constants.RoleInstructor)]
        [Route("Create", Name = "CourseCreate")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create", Name = "CourseCreatePost")]
        public async Task<ActionResult> Create(Course model)
        {
            var aspNetUserId = User.Identity.GetUserId();
            var instructor = await _context.Instructors.Include(c => c.Courses).FirstOrDefaultAsync(i => i.AspNetUserId == aspNetUserId);
            model.InstructorId = instructor.Id;
            var createdModel = await _courseService.CreateAsync(model);

            if (User.IsInRole(Constants.RoleInstructor))
            {
                return RedirectToRoute("InstructorDetails", new { id = createdModel.Id });
            }
            else
            {
                return RedirectToRoute("StudentDetails", new { id = createdModel.Id });
            }
        }

        [Authorize(Roles = Constants.RoleStudent)]
        [Route("StudentCourseDetails/{id}", Name = "StudentDetails")]
        public async Task<ActionResult> StudentCourseDetails(int id)
        {
            var model = await _courseService.GetAsync(id);

            return View(model);
        }

        [Authorize(Roles = Constants.RoleInstructor)]
        [Route("InstructorCourseDetails/{id}", Name = "InstructorDetails")]
        public async Task<ActionResult> InstructorCourseDetails(int id)
        {
            var model = await _courseService.GetAsync(id);

            return View(model);
        }

        [Route("Edit/{id}", Name = "CourseEdit")]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _courseService.GetAsync(id);

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}", Name = "CourseEditPost")]
        public async Task<ActionResult> Edit(Course model)
        {
            await _courseService.EditAsync(model.Id, model);

            if (User.IsInRole(Constants.RoleInstructor))
            {
                return RedirectToRoute("InstructorDetails", new { id = model.Id });
            }
            else
            {
                return RedirectToRoute("StudentDetails", new { id = model.Id });
            }
        }

        [Route("Delete/{id}", Name = "CourseDelete")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _courseService.GetAsync(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}", Name = "CourseDeletePost")]
        public async Task<ActionResult> Delete(Course model)
        {
            var modelToDelete = await _courseService.GetAsync(model.Id);
            await _courseService.DeleteAsync(modelToDelete);

            return RedirectToRoute("CourseInstructorIndex ");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Enroll/{id}", Name = "EnrollPost")]
        public async Task<ActionResult> Enroll(int id)
        {
            await _courseService.AddStudentAsync(id, User.Identity.GetUserId());

            if (User.IsInRole(Constants.RoleInstructor))
            {
                return RedirectToRoute("InstructorDetails", new { id });
            }
            else
            {
                return RedirectToRoute("StudentDetails", new { id });
            }
        }

        [Route("Message/{id}", Name = "Message")]
        public async Task<ActionResult> Message(int id)
        {
            var model = await _courseService.GetAsync(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Message/{id}", Name = "MessagePost")]
        public async Task<ActionResult> Message(int id, string newMessage)
        {
            var model = await _courseService.GetAsync(id);

            if (User.IsInRole(Constants.RoleInstructor))
            {
                await _courseService.AddMessageInstructorAsync(id, User.Identity.GetUserId(), newMessage);

                return RedirectToRoute("Message", new { id = model.Id });
            }
            else
            {
                await _courseService.AddMessageStudentAsync(id, User.Identity.GetUserId(), newMessage);

                return RedirectToRoute("Message", new { id = model.Id });
            }
        }
    }
}