using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;


namespace MyBlogSite.Common.Helper
{
   public class ConfigHelper
    {
        public static T Get<T>(string key)
        {
            //Hangi tipte verirsen geriye onu cast et döndür
           return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key],typeof(T));
        }

    }
}