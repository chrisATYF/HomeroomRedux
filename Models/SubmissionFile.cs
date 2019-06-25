using Humanizer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeroomRedux.Models
{
    public class SubmissionFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
        public string ExternalUrl { get; set; }
        public byte[] Data { get; set; }
        public int SubmissionId { get; set; }
        public Submission Submission { get; set; }
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [NotMapped]
        public string CreateDateHumanized => CreateDate.Humanize();
    }
}