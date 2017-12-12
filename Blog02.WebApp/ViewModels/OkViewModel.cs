using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog02.WebApp.ViewModels
{
    public class OkViewModel : NotifyViewModelBase<string>
    {

        public OkViewModel()
        {
            Title = "İşlem Başarılı...";
        }
    }
}