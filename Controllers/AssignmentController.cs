﻿using HomeroomRedux.Models;
using HomeroomRedux.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeroomRedux.Controllers
{
    [Authorize]
    [RoutePrefix("Assignment")]
    public class AssignmentController : Controller
    {
        protected readonly IAssignment _assignmentService;

        public AssignmentController(IAssignment assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [Route("", Name = "AssignmentIndex")]
        public ActionResult Index()
        {
            var model = _assignmentService.GetAllAssignmentsAsync();

            return View(model);
        }

        [Route("Create/{courseId}", Name = "CreateAssignment")]
        public ActionResult Create(int courseId)
        {
            var model = new Assignment
            {
                CourseId = courseId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create/{id}", Name = "CreateAssignmentPost")]
        public async Task<ActionResult> Create(Assignment assignmentModel)
        {
            await _assignmentService.AddAsync(assignmentModel);

            if (User.IsInRole(Constants.RoleInstructor))
            {
                return RedirectToRoute("InstructorDetails", new { id = assignmentModel.CourseId });
            }
            else
            {
                return RedirectToRoute("StudentDetails", new { id = assignmentModel.CourseId });
            }
        }

        [Route("Details/{id}", Name = "AssignmentDetails")]
        public async Task<ActionResult> Details(int id)
        {
            var model = await _assignmentService.GetAsync(id);

            return View(model);
        }

        [Route("Edit/{id}", Name = "EditAssignment")]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _assignmentService.GetAsync(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit", Name = "EditAssignmentPost")]
        public async Task<ActionResult> Edit(Assignment model)
        {
            var modelToEdit = await _assignmentService.GetAsync(model.Id);

            await _assignmentService.EditAsync(model);

            if (User.IsInRole(Constants.RoleInstructor))
            {
                return RedirectToRoute("InstructorDetails", new { id = modelToEdit.CourseId });
            }
            else
            {
                return RedirectToRoute("StudentDetails", new { id = modelToEdit.CourseId });
            }
        }

        [Route("Delete/{id}", Name = "DeleteAssignment")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _assignmentService.GetAsync(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}", Name = "DeleteAssignmentPost")]
        public async Task<ActionResult> Delete(Assignment model)
        {
            await _assignmentService.DeleteAsync(model);

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