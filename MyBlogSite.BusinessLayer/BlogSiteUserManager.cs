using MyBlogSite.DataAccessLayer.EntityFramework;
using MyBlogSite.Entities;
using MyBlogSite.Entities.Messages;
using MyBlogSite.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.BusinessLayer
{
    public class BlogSiteUserManager
    {
        private Repository<BlogSiteUser> repo_user = new Repository<BlogSiteUser>();
        public BusinessLayerResult<BlogSiteUser> RegisterUser(RegisterViewModel data)
        {
            BlogSiteUser user = repo_user.Find(x => x.Username == data.Username || x.Email == data.Email);
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
             int dbResult = repo_user.Insert(new BlogSiteUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive=false,
                    IsAdmin=false
                });

                if (dbResult > 0)
                {
                    repo_user.Find(x => x.Username == data.Username && x.Email == data.Email);


                    //Aktivasyon maili atılacak
                    //layerResult.Result.ActivateGuid
                }
            }


                return res;
        }

        public BusinessLayerResult<BlogSiteUser> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<BlogSiteUser> res = new BusinessLayerResult<BlogSiteUser>();
            res.Result = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);
            

            if(res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive,"Kullanıcı Aktifleştirilmemiş");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Eposta adresinizi kontrol ediniz");

                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı ya da şifre uyuşmuyor.");
            }

            return res;

        }
    }
}
