using HomeroomRedux.Models;
using HomeroomRedux.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeroomRedux.Controllers
{
    [Authorize]
    [RoutePrefix("Notes")]
    public class NotesController : Controller
    {
        protected readonly INotes _notesService;
        protected readonly ApplicationDbContext _context;

        public NotesController(INotes notesService, ApplicationDbContext context)
        {
            _notesService = notesService;
            _context = context;
        }

        [Route("", Name = "NotesIndex")]
        public async Task<ActionResult> Index()
        {
            var model = await _notesService.GetAllNotesAsync();

            return View(model);
        }

        [Route("Details/{id}", Name = "NoteDetails")]
        public async Task<ActionResult> Details(int id)
        {
            var model = await _notesService.GetAsync(id);

            return View(model);
        }

        [Route("Add/{id}", Name = "AddNote")]
        public async Task<ActionResult> Add(int id)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = await _context.Students.FirstOrDefaultAsync(u => u.AspNetUserId == userId);
            var model = new Note
            {
                CourseId = id,
                StudentId = currentUser.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Add/{id}", Name = "AddNotePost")]
        public async Task<ActionResult> Add(Note model)
        {
            var userId = User.Identity.GetUserId();
            await _notesService.AddAsync(userId, model);

            if (User.IsInRole(Constants.RoleInstructor))
            {
                return RedirectToRoute("InstructorDetails", new { id = model.CourseId });
            }
            else
            {
                return RedirectToRoute("StudentDetails", new { id = model.CourseId });
            }
        }

        [Route("Edit/{id}", Name = "EditNote")]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _notesService.GetAsync(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}", Name = "EditNotePost")]
        public async Task<ActionResult> Edit(Note model)
        {
            await _notesService.EditAsync(model);

            if (User.IsInRole(Constants.RoleInstructor))
            {
                return RedirectToRoute("InstructorDetails", new { id = model.CourseId });
            }
            else
            {
                return RedirectToRoute("StudentDetails", new { id = model.CourseId });
            }
        }

        [Route("Delete/{id}", Name = "DeleteNote")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _notesService.GetAsync(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}", Name = "DeleteNotePost")]
        public async Task<ActionResult> Delete(Note model)
        {
            await _notesService.DeleteAsync(model);

            if (User.IsInRole(Constants.RoleInstructor))
            {
                return RedirectToRoute("InstructorDetails", new { id = model.CourseId });
            }
            else
            {
                return RedirectToRoute("StudentDetails", new { id = model.CourseId });
            }
        }
    }
}