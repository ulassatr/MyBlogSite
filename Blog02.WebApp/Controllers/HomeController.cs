using MyBlogSite.Entities.ValueObject;
using MyBlogSite.BusinessLayer;
using MyBlogSite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyBlogSite.Entities.Messages;

namespace Blog02.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //CategoryController üzerinden gelen veri talebi
            //if (TempData["mm"] !=null)
            //{
            //    //Tempdatanın içerisindeki notları al model olarak döndür
            //    return View(TempData["mm"] as List<Note>);

            //}
            NoteManager nm = new NoteManager();

            return View(nm.GetAllNote().OrderByDescending(x => x.ModifiedOn).ToList());
            // return View(nm.GetAllNoteQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value);

            if (cat == null)
            {
                return HttpNotFound();
            }
            //Bir actiondan diger bir actiona geçerken hayatta kalabilen taşıam yöntemi

            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();

            //Her Action için farklı görünümler oluşturmaya gerek yok
            //Index sayfasına git asagidaki modeli dön
            return View("Index", nm.GetAllNote().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BlogSiteUserManager bum = new BlogSiteUserManager();
                BusinessLayerResult<BlogSiteUser> res = bum.LoginUser(model);

                
                if (res.Errors.Count > 0)
                {
                    if(res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = "http:/Home/Activate/123";
                    }
                    //Tüm listeyi foreachle dön hata mesajını modelstate e ekle
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }
                Session["login"] = res.Result; //Session kullanıcı bilgisi saklama
                return RedirectToAction("Index");  // Yönlendirme
            }


            //Giriş kontrolu ve yonlendirme 
            //Session a kullanıcı bilgi saklama
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //kullanıcı username kontrolu
            //kullanıcı eposta kontrolu
            //kayıt işlemi
            //Aktivasyon epostası gönderimi

            if (ModelState.IsValid)
            {
                BlogSiteUserManager bum = new BlogSiteUserManager();
                BusinessLayerResult<BlogSiteUser> res = bum.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    //Tüm listeyi foreachle dön hata mesajını modelstate e ekle
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                return RedirectToAction("RegisterOk");
            }


            return View(model);
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult UserActive(Guid activate_id)
        {
            //Kullanıcı aktivasyonu sağlanacak
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}