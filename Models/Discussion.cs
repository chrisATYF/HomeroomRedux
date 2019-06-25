using Humanizer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeroomRedux.Models
{
    public class Discussion
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime CreateDate { get; set; }

        [NotMapped]
        public string CreateDateHumanized => CreateDate.Humanize();
    }
}