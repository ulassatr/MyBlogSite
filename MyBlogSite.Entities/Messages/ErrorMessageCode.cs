﻿

namespace MyBlogSite.Entities.Messages
{
    //Hataları hata koduyla tutarsak dilden  bağımsız olarak işlem yapabiliriz.
  public enum ErrorMessageCode
    {
        UsernameAlreadyExists = 101 ,
        EmailAlreadyExists = 102,
        UserIsNotActive = 151,
        UsernameOrPassWrong = 152,
        CheckYourEmail =153
    }
}
