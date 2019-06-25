using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeroomRedux.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AspNetUserId { get; set; }
        public ICollection<Submission> Submission { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Discussion> Discussions { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}