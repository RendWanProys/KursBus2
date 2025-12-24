using KursBus2.Models;
using Microsoft.EntityFrameworkCore;

namespace KursBus2.Models
{
    public class SeedData
    {
        public static void SeedDatabase(KursProjectContext context)
        {
            //context.Database.Migrate();
            //if (context.UserDates.Count() == 0)
            //{
            //    UserData user = new UserData 
            //    { 
            //        Email = "admin@mail.ru", 
            //        PassWord = AuthOptions.GetHash("1234") 
            //    };
            //    context.UserDates.Add(user);
            //    context.SaveChanges();
            //}
        }
    }
}

