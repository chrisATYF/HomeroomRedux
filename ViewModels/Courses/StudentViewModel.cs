using HomeroomRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeroomRedux.ViewModels.Courses
{
    public class StudentViewModel
    {
        public List<Course> MyCourses { get; set; }
        public List<Course> PopularCourses { get; set; }
    }
}