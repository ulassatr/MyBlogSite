using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog02.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            MyBlogSite.BusinessLayer.Test test = new MyBlogSite.BusinessLayer.Test();
            test.InsertTest();
            return View();
        }
    }
}