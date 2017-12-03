using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogSite.Entities.ValueObject
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "{0} alanı boş geçilemez"), 
            DisplayName("kullanıcı adı"),
           StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalıdır.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "{0} alanı boş geçilemez"), 
            DisplayName("E posta"),
           StringLength(70, ErrorMessage = "{0} max. {1} karakter olmalıdır."),
            EmailAddress(ErrorMessage ="Lütfen {0} alanı için geçerli bir eposta adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez"),
            DisplayName("şifre"), 
            DataType(DataType.Password),
           StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez"),
            DisplayName("şifre tekrar"),
            DataType(DataType.Password),
           StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalıdır."),
            Compare("Password",ErrorMessage ="{0} ile {1} birbiriyle uyuşmuyor.")]
        public string RePassword { get; set; }

    }
}