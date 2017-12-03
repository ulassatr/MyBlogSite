using MyBlogSite.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.DataAccessLayer.EntityFramework
{
    public class RepositoryBase
    {
        //protected olduğu için miras alınan class db değişkenini kullanabilir.
        protected static DatabaseContext db;
        private static object _lock = new object();

        //Constructor ın protected olması classın yenilenemez olması demektir.Sadece miras alan yenileyebilir.
        protected RepositoryBase()
        {
            CreateContext();
        }
        private static void CreateContext()
        {
            if (db == null)
            {
                lock (_lock)
                {
                    if (db == null)
                    {
                        db = new DatabaseContext();
                    }
                }
            }
        
        }


    }
}
