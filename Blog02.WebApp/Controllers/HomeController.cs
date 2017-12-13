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
using Blog02.WebApp.ViewModels;
using MyBlogSite.BusinessLayer.Results;

namespace Blog02.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private BlogSiteUserManager blogSiteUserManager = new BlogSiteUserManager();
        // GET: Home
        public ActionResult Index()
        {
            //CategoryController üzerinden gelen veri talebi
            //if (TempData["mm"] !=null)
            //{
            //    //Tempdatanın içerisindeki notları al model olarak döndür
            //    return View(TempData["mm"] as List<Note>);

            //}
            

            return View(noteManager.ListQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
            // return View(nm.GetAllNoteQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Category cat = categoryManager.Find(x=>x.Id==id.Value);

            if (cat == null)
            {
                return HttpNotFound();
            }
            //Bir actiondan diger bir actiona geçerken hayatta kalabilen taşıam yöntemi

            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult MostLiked()
        {          
            //Her Action için farklı görünümler oluşturmaya gerek yok
            //Index sayfasına git asagidaki modeli dön
            return View("Index", noteManager.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult ShowProfile()
        {
            BlogSiteUser currentUser = Session["login"] as BlogSiteUser;

            
            BusinessLayerResult<BlogSiteUser>  res = blogSiteUserManager.GetUserById(currentUser.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel ErrorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors
                };

                return View("Error", ErrorNotifyObj);
                //Kullanıcıyı hata ekranına yönlendirmek gerekiyor.
            }
            return View(res.Result);
        }

        public ActionResult EditProfile()
        {
            BlogSiteUser currentUser = Session["login"] as BlogSiteUser;
            
            BusinessLayerResult<BlogSiteUser> res = blogSiteUserManager.GetUserById(currentUser.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel ErrorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors
                };

                return View("Error", ErrorNotifyObj);
                //Kullanıcıyı hata ekranına yönlendirmek gerekiyor.
            }
            return View(res.Result);
        }

        [HttpPost]
        public ActionResult EditProfile(BlogSiteUser model,HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
               (ProfileImage.ContentType == "image/jpeg" ||
               ProfileImage.ContentType == "image/jpg" ||
               ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";

                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    model.ProfileImageFilename = filename;

                }
                
                BusinessLayerResult<BlogSiteUser> res = blogSiteUserManager.UpdateProfile(model);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel ErrorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Güncellenmedi",
                        RedirectingUrl = "/Home/EditProfile"

                    };

                    return View("Error", ErrorNotifyObj);
                }

                Session["login"] = res.Result; // Profil güncellendiği için session güncellendi.

                return RedirectToAction("ShowProfile");

            }

            return View(model);

        }

        public ActionResult DeleteProfile()
        {
            BlogSiteUser currentUser = Session["login"] as BlogSiteUser;

            
            BusinessLayerResult<BlogSiteUser> res = blogSiteUserManager.RemoveUserById(currentUser.Id);

            if(res.Errors.Count > 0)
            {
                ErrorViewModel ErrorNotifyObj = new ErrorViewModel()
                {
                    Items = res.Errors,
                    Title = "Profil silinemedi",
                    RedirectingUrl = "/Home/ShowProfile"
                };
                return View("Error", ErrorNotifyObj);

            }
            Session.Clear();
            return RedirectToAction("Index");
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
               
                BusinessLayerResult<BlogSiteUser> res = blogSiteUserManager.LoginUser(model);

                
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
               
           
                BusinessLayerResult<BlogSiteUser> res = blogSiteUserManager.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    //Tüm listeyi foreachle dön hata mesajını modelstate e ekle
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                OkViewModel notifyObj = new OkViewModel()
                {

                    Title="Kayıt Başarılı",
                    RedirectingUrl="/Home/Login",
                };
                notifyObj.Items.Add("Lütfen e postanıza gönderdiğimiz aktivasyon linkine tıklayarak hesabıbızı aktif ediniz");

                return View("Ok",notifyObj);
            }


            return View(model);
        }

        public ActionResult UserActive(Guid id)
        {
            //Kullanıcı aktivasyonu sağlanacak
           
            BusinessLayerResult<BlogSiteUser> res = blogSiteUserManager.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel ErrorNotifyObj = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    Items = res.Errors
                };
            
                return View("Error",ErrorNotifyObj);
            }

            OkViewModel OkNotifyObj = new OkViewModel()
            {
                Title = "Hesabınız aktifleştirildi.",
                RedirectingUrl = "/Home/Login"
            };
            OkNotifyObj.Items.Add("Hesabınız aktifleştirildi.");

            return View("Ok",OkNotifyObj);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}