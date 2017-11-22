using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyBlogSite.Entities;

namespace MyBlogSite.DataAccessLayer
{
   public class Initializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            BlogSiteUser admin = new BlogSiteUser()
            {
                Name = "Ulaş",
                Surname = "Şatır",
                Email = "ulassatr@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "ulassatr",
                Password = "12345",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "ulassatr"

            };
            BlogSiteUser standartUser = new BlogSiteUser()
            {
                Name = "Çagan",
                Surname = "Şatır",
                Email = "cagansatir@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "cagansatir",
                Password = "123456",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "cagansatir"

            };

            context.BlogSiteUsers.Add(admin);
            context.BlogSiteUsers.Add(standartUser);

            context.SaveChanges();

            //Adding fake categories
            for (int i = 0; i < 10; i++)
            {

                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "ulassatr"

                };

                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); k++)
                {
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        Category = cat,
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(10, 50),
                        Owner = (k % 2 == 0) ? admin : standartUser,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = (k % 2 == 0) ? admin.Username : standartUser.Username,

                    };
                    cat.Notes.Add(note);
                        
                }
            }

        }


    }
}
