using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeroomRedux.Controllers
{
    [RoutePrefix("Home")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [Route("", Name = "IndexPage")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(Constants.RoleInstructor))
                {
                    return RedirectToRoute("InstructorCourseIndex");
                }

                return RedirectToRoute("StudentCourseIndex");
            }

            return View();
        }
    }
}