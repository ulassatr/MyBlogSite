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
                ProfileImageFilename="user.png",
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
                ProfileImageFilename = "user.png",
                Password = "123456",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "cagansatir"

            };

            context.BlogSiteUsers.Add(admin);
            context.BlogSiteUsers.Add(standartUser);

            for (int i = 0; i < 8; i++)
            {
                BlogSiteUser User = new BlogSiteUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname =FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    ProfileImageFilename = "user.png",
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{i}",
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = $"user{i}"
                };
                context.BlogSiteUsers.Add(User);
            }

            context.SaveChanges();

            List<BlogSiteUser> userList = context.BlogSiteUsers.ToList();

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
                context.Categories.Add(cat);

                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); k++)
                {
                    BlogSiteUser owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        Category = cat,
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = owner.Username,

                    };
                    cat.Notes.Add(note);

                    //Adding fake comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3,5); j++)
                    {
                        BlogSiteUser owner_comment = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = owner_comment,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUserName = owner_comment.Username,

                        };

                        //Note un commentlerine ekliyoruz
                        note.Comments.Add(comment);
                    }
                    
                    //Adding fake likes
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userList[m]

                        };
                        note.Likes.Add(liked);
                    }
                }
            }

            context.SaveChanges(); 
        }


    }
}
