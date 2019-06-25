using HomeroomRedux.Models;
using HomeroomRedux.Services.Interfaces;
using HomeroomRedux.ViewModels.Uploads;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeroomRedux.Controllers
{
    [Authorize]
    [RoutePrefix("FileUpload")]
    public class FileUploadsController : Controller
    {
        protected readonly IUpload _uploadService;
        protected readonly ApplicationDbContext _context;

        public FileUploadsController(IUpload uploadService, ApplicationDbContext context)
        {
            _uploadService = uploadService;
            _context = context;
        }

        [Route("", Name = "UploadIndex")]
        public async Task<ActionResult> Index()
        {
            var model = await _uploadService.GetAllFilesAsync();

            return View(model);
        }

        [Route("Upload/{submissionId}", Name = "UploadFile")]
        public ActionResult Upload(int submissionId)
        {
            var model = new UploadCourseViewModel
            {
                SubmissionId = submissionId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Upload", Name = "UploadFilePost")]
        public async Task<ActionResult> Upload(UploadCourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (!viewModel.CheckFileExtentions(viewModel.File.FileName))
            {
                return View(viewModel);
            }

            var reader = new BinaryReader(viewModel.File.InputStream);
            var data = reader.ReadBytes(viewModel.File.ContentLength);
            var path = Server.MapPath("~/uploads/");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            viewModel.File.SaveAs(Path.Combine(path, viewModel.File.FileName));
            var urlBase = ConfigurationManager.AppSettings["SiteUrlBase"];
            var fileUpload = new SubmissionFile
            {
                FileName = viewModel.File.FileName,
                ContentType = viewModel.File.ContentType,
                ContentLength = viewModel.File.ContentLength,
                ExternalUrl = $"{urlBase}uploads/{viewModel.File.FileName}",
                Data = data,
                SubmissionId = viewModel.SubmissionId,
                CreateDate = DateTime.UtcNow
            };

            var currentSubmission = await _context.Submissions.FirstOrDefaultAsync(i => i.Id == fileUpload.SubmissionId);
            currentSubmission.SubmissionFiles = new List<SubmissionFile>();
            currentSubmission.SubmissionFiles.Add(fileUpload);

            await _uploadService.AddAsync(fileUpload);

            return RedirectToRoute("SubmissionDetails", new { submissionId = currentSubmission.Id });
        }

        [Route("Details/{uploadFileId}", Name = "FileDetails")]
        public async Task<ActionResult> Details(int uploadFileId)
        {
            var model = await _uploadService.GetAsync(uploadFileId);

            return View(model);
        }
    }
}