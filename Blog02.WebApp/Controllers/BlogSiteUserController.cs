using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyBlogSite.BusinessLayer;
using MyBlogSite.BusinessLayer.Results;
using MyBlogSite.Entities;

namespace Blog02.WebApp.Controllers
{
    public class BlogSiteUserController : Controller
    {
        private BlogSiteUserManager blogSiteUserManager = new BlogSiteUserManager();
      
        public ActionResult Index()
        {
            return View(blogSiteUserManager.List());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogSiteUser blogSiteUser = blogSiteUserManager.Find(x => x.Id == id.Value);
            if (blogSiteUser == null)
            {
                return HttpNotFound();
            }
            return View(blogSiteUser);
        }

        
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogSiteUser blogSiteUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<BlogSiteUser> res = blogSiteUserManager.Insert(blogSiteUser);
                    
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(blogSiteUser);
                }
                return RedirectToAction("Index");
            }

            return View(blogSiteUser);
        }
        

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogSiteUser blogSiteUser = blogSiteUserManager.Find(x => x.Id == id.Value);
            if (blogSiteUser == null)
            {
                return HttpNotFound();
            }
            return View(blogSiteUser);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BlogSiteUser blogSiteUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<BlogSiteUser> res = blogSiteUserManager.Update(blogSiteUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(blogSiteUser);
                }

                return RedirectToAction("Index");
            }
            return View(blogSiteUser);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogSiteUser blogSiteUser = blogSiteUserManager.Find(x => x.Id == id.Value);
            if (blogSiteUser == null)
            {
                return HttpNotFound();
            }
            return View(blogSiteUser);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogSiteUser blogSiteUser = blogSiteUserManager.Find(x => x.Id == id);
            blogSiteUserManager.Delete(blogSiteUser);
            return RedirectToAction("Index");
        }

    }
}
