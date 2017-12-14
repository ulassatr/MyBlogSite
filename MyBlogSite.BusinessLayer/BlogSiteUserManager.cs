using MyBlogSite.BusinessLayer.Abstract;
using MyBlogSite.BusinessLayer.Results;
using MyBlogSite.Common.Helper;
using MyBlogSite.DataAccessLayer.EntityFramework;
using MyBlogSite.Entities;
using MyBlogSite.Entities.Messages;
using MyBlogSite.Entities.ValueObject;
using MyEvernote.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.BusinessLayer
{
    public class BlogSiteUserManager : ManagerBase<BlogSiteUser>
    {

        //new anahtar kelimesi method gizlememize yardım eder.

        public BusinessLayerResult<BlogSiteUser> RegisterUser(RegisterViewModel data)
        {
            BlogSiteUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<BlogSiteUser> res = new BusinessLayerResult<BlogSiteUser>();

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı Adı kayıtlı");

                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "Eposta kayıtlı");
                }
            }
            else
            {
                int dbResult = base.Insert(new BlogSiteUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    ProfileImageFilename = "user.png",
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false
                });

                if (dbResult > 0)
                {
                    res.Result = Find(x => x.Username == data.Username && x.Email == data.Email);


                    //Aktivasyon maili atılacak
                    //layerResult.Result.ActivateGuid

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActive/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.Username};<br><br>Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>.";


                    MailHelper.SendMail(body, res.Result.Email, "Hesap Aktifleştirme");
                }
            }

            return res;
        }

        public BusinessLayerResult<BlogSiteUser> GetUserById(int id)
        {
            BusinessLayerResult<BlogSiteUser> res = new BusinessLayerResult<BlogSiteUser>();
            res.Result = Find(x => x.Id == id);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı");
            }
            return res;

        }

        public BusinessLayerResult<BlogSiteUser> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<BlogSiteUser> res = new BusinessLayerResult<BlogSiteUser>();
            res.Result = Find(x => x.Username == data.Username && x.Password == data.Password);


            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı Aktifleştirilmemiş");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Eposta adresinizi kontrol ediniz");

                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı ya da şifre uyuşmuyor.");
            }

            return res;

        }

        public BusinessLayerResult<BlogSiteUser> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<BlogSiteUser> res = new BusinessLayerResult<BlogSiteUser>();
            res.Result = Find(x => x.ActivateGuid == activateId);

            if (res.Result != null)
            {

                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı");
            }
            return res;
        }

        public BusinessLayerResult<BlogSiteUser> UpdateProfile(BlogSiteUser data)
        {
            BlogSiteUser db_user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
            BusinessLayerResult<BlogSiteUser> res = new BusinessLayerResult<BlogSiteUser>();

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;

            if (string.IsNullOrEmpty(data.ProfileImageFilename) == false)
            {
                res.Result.ProfileImageFilename = data.ProfileImageFilename;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");
            }

            return res;
        }

        public BusinessLayerResult<BlogSiteUser> RemoveUserById(int id)
        {
            BusinessLayerResult<BlogSiteUser> res = new BusinessLayerResult<BlogSiteUser>();
            BlogSiteUser user = Find(x => x.Id == id);


            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemedi");
                    return res;

                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı Bulunamadı");
            }
            return res;
        }

        public new BusinessLayerResult<BlogSiteUser> Insert(BlogSiteUser data)
        {
            BlogSiteUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<BlogSiteUser> res = new BusinessLayerResult<BlogSiteUser>();

            res.Result = data;
            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı Adı kayıtlı");

                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "Eposta kayıtlı");
                }
            }
            else
            {
                res.Result.ProfileImageFilename = "user.png";
                res.Result.ActivateGuid = Guid.NewGuid();

                if (base.Insert(res.Result) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı Eklenemedi");
                }

            }

            return res;


        }

        public new BusinessLayerResult<BlogSiteUser> Update(BlogSiteUser data)
        {

            BlogSiteUser db_user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
            BusinessLayerResult<BlogSiteUser> res = new BusinessLayerResult<BlogSiteUser>();

            res.Result = data;


            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            res.Result.IsActive = data.IsActive;
            res.Result.IsAdmin = data.IsAdmin;


            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanıcı güncellenemedi.");
            }

            return res;


        }
    }
}
