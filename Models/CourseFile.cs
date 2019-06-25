using Humanizer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeroomRedux.Models
{
    public class CourseFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
        public string ExternalUrl { get; set; }
        public byte[] Data { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime CreateDate { get; set; }

        [NotMapped]
        public string CreateDateHumanized => CreateDate.Humanize();
    }
}