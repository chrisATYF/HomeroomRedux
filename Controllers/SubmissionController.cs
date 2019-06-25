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
    [RoutePrefix("Submissions")]
    public class SubmissionsController : Controller
    {
        protected readonly ISubmissions _submissionService;
        protected readonly ApplicationDbContext _context;

        public SubmissionsController(ISubmissions submissionService, ApplicationDbContext context)
        {
            _submissionService = submissionService;
            _context = context;
        }

        [Route("Index/{assignmentId}", Name = "SubmissionIndex")]
        public async Task<ActionResult> Index(int assignmentId)
        {
            var aspNetUserId = User.Identity.GetUserId();
            var userByAspNetId = await _context.Students.FirstOrDefaultAsync(a => a.AspNetUserId == aspNetUserId);
            if (User.IsInRole(Constants.RoleStudent))
            {
                var modelSubmissions = await _submissionService.GetUnsubmittedSubmission(assignmentId);
                if (modelSubmissions == null || modelSubmissions.IsSubmitted)
                {
                    var model = new Submission
                    {
                        AssignmentId = assignmentId,
                        IsSubmitted = false,
                        StudentId = userByAspNetId.Id,
                        SubmissionCreateDate = DateTime.UtcNow
                    };

                    await _submissionService.AddAsync(model);

                    return RedirectToRoute("SubmissionDetails", new { submissionId = model.Id });
                }
                else
                {
                    return RedirectToRoute("SubmissionDetails", new { submissionId = modelSubmissions.Id });
                }
            }
            else
            {
                return RedirectToRoute("InstructorSubIndex", new { assignmentId = assignmentId });
            }
        }

        [Route("Details/{submissionId}", Name = "SubmissionDetails")]
        public async Task<ActionResult> Details(int submissionId)
        {
            var model = await _submissionService.GetSubmissionAsync(submissionId);

            return View(model);
        }

        [Route("Submit/{submissionId}", Name = "FileSubmit")]
        public async Task<ActionResult> Submit(int submissionId)
        {
            var model = await _submissionService.GetSubmissionAsync(submissionId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Submit", Name = "FileSubmitPost")]
        public async Task<ActionResult> Submit(Submission model)
        {
            var modelToSubmit = await _submissionService.GetSubmissionAsync(model.Id);
            modelToSubmit = model.SetSubmissionAndDate(modelToSubmit);
            await _submissionService.SubmitAsync(modelToSubmit);
            var assignmentById = await _context.Assignments.FirstOrDefaultAsync(i => i.Id == modelToSubmit.AssignmentId);

            return RedirectToRoute("StudentDetails", new { id = assignmentById.CourseId });
        }

        [Route("InstructorSubmissionIndex/{assignmentId}", Name = "InstructorSubIndex")]
        public async Task<ActionResult> InstructorSubmissionIndex(int assignmentId)
        {
            var models = await _submissionService.GetAllAsync(assignmentId);

            return View(models);
        }

        [Authorize(Roles = Constants.RoleInstructor)]
        [Route("SubmissionGrade/{submissionId}", Name = "InstructorSubmissionGrade")]
        public async Task<ActionResult> SubmissionGrade(int submissionId)
        {
            var model = await _submissionService.GetSubmissionAsync(submissionId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.RoleInstructor)]
        [Route("SubmissionGrade", Name = "InstructorSubmissionGradePost")]
        public async Task<ActionResult> SubmissionGrade(Submission model)
        {
            var modelToGrade = await _submissionService.GetSubmissionAsync(model.Id);
            modelToGrade = model.SetSubGrade(modelToGrade);
            await _context.SaveChangesAsync();

            return RedirectToRoute("InstructorSubIndex", new { assignmentId = modelToGrade.AssignmentId });
        }
    }
}