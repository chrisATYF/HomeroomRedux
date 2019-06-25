using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeroomRedux.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "About Instructor")]
        public string AboutInstructor { get; set; }
        public ICollection<Course> Courses { get; set; }
        public string AspNetUserId { get; set; }
    }
}