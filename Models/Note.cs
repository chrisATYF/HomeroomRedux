
using Humanizer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeroomRedux.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NoteSummary { get; set; }
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [NotMapped]
        public string CreateDateHumanized => CreateDate.Humanize();
    }
}