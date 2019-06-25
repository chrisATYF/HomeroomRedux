using Humanizer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HomeroomRedux.Enums;

namespace HomeroomRedux.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        [Display(Name = "Assignment Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Available Point Grades")]
        public int TotalPointsAvailable { get; set; }
        [Display(Name = "Current Point Grade")]
        public int TotalPoints { get; set; }
        [Display(Name = "Assignment Type")]
        public AssignmentType AssignmentType { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Submission> Submissions { get; set; }
        public DateTime AssignDate { get; set; }
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [NotMapped]
        public string AssignDateHumanized => AssignDate.Humanize();
        [NotMapped]
        public string DueDateHumanized => DueDate.Humanize();
    }
}