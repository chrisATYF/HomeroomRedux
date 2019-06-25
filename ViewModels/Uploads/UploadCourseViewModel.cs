using HomeroomRedux.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HomeroomRedux.ViewModels.Uploads
{
    public class UploadCourseViewModel
    {
        public int SubmissionId { get; set; }
        public HttpPostedFileBase File { get; set; }
        public ICollection<SubmissionFile> SubmissionFiles { get; set; }

        public bool CheckFileExtentions(string fileName)
        {
            var allowedExtentions = new[] { ".pdf", ".gif", ".txt", ".png", ".jpg", ".jpeg" };
            var fileExtention = Path.GetExtension(fileName).ToLower();

            if (!allowedExtentions.Contains(fileExtention))
            {
                return false;
            }

            return true;
        }
    }
}