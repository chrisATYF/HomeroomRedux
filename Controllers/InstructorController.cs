using HomeroomRedux.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeroomRedux.Controllers
{
    [RoutePrefix("AboutInstructor")]
    [Authorize(Roles = Constants.RoleInstructor)]
    public class InstructorController : Controller
    {
        protected readonly IInstructor _instructorService;

        public InstructorController(IInstructor instructorService)
        {
            _instructorService = instructorService;
        }

        [Route("AboutInstructor/{instructorId}", Name = "AboutInstructorGet")]
        public async Task<ActionResult> AboutInstructor(int instructorId)
        {
            var model = await _instructorService.GetAsync(instructorId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AboutInstructor", Name = "AboutInstructorPost")]
        public async Task<ActionResult> AboutInstructor(Instructor model)
        {
            await _instructorService.AddAboutAsync(model);

            return RedirectToRoute("InstructorCourseIndex");
        }
    }
}