using MyBlogSite.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        //DbContext sınıfın özelliği, veritabanı üzerinde oturum (session) işlemlerini gerçekleştirip oluşturulan oturumlar üzerinden işlemlerin yapılmasını sağlar.

        public DbSet<BlogSiteUser> BlogSiteUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new Initializer());
        }

    }
}
