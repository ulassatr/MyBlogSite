﻿using MyBlogSite.BusinessLayer;
using MyBlogSite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Blog02.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        // TempData ile category listeleme 
        //public ActionResult select(int? id)
        //{
        //    if (id == null)
        //    {
        //       return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CategoryManager cm = new CategoryManager();
        //    Category cat = cm.GetCategoryById(id.Value);

        //    if (cat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    //Bir actiondan diger bir actiona geçerken hayatta kalabilen taşıam yöntemi
        //    TempData["mm"] = cat.Notes;
        //    return RedirectToAction("Index","Home");
        //}
    }
}