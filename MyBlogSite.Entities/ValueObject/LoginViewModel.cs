using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogSite.Entities.ValueObject
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} alanı boş geçilemez"), DisplayName("kullanıcı adı"),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez"), DisplayName("şifre"), DataType(DataType.Password),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalıdır.")]
        public string Password { get; set; }
    }
}