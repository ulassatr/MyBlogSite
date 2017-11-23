using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlogSite.Entities;

namespace MyBlogSite.BusinessLayer
{
    public class Test
    {
       private  Repository<BlogSiteUser> repo_user = new Repository<BlogSiteUser>();
       private  Repository<Category> repo_category = new Repository<Category>();
        public Test()
        {
            
         //   List<Category> categories = repo_category.List();
        }
        public void InsertTest()
        {
            
            int result = repo_user.Insert(new BlogSiteUser()
            {
                Name = "aaa",
                Surname = "bbb",
                Email = "ulsatr@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "aabb",
                Password = "1111",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "aabb"

            });
        }

    }
}
