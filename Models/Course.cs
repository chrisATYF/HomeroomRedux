using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeroomRedux.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Course Description")]
        public string Description { get; set; }
        [Display(Name = "About Course")]
        public string AboutCourse { get; set; }
        [Display(Name = "What You Learn")]
        public string WhatYouLearn { get; set; }
        public Instructor Instructor { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Discussion> Discussions { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}