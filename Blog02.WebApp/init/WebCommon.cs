using MyBlogSite.Common;
using MyBlogSite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog02.WebApp.init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            if(HttpContext.Current.Session["login"] != null)
            {
                BlogSiteUser user = HttpContext.Current.Session["login"] as BlogSiteUser;
                return user.Username;
            }

            return "system";
        }
    }
}