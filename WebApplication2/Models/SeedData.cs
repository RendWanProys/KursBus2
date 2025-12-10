using KursBus2.Models;
using Microsoft.EntityFrameworkCore;

namespace KursBus2.Models
{
    public class SeedData
    {
        public static void SeedDatabase(KursProjectContext context)
        {
            context.Database.Migrate();
            if (context.UserData.Count() == 0)
            {
                UserData user = new UserData { Email = "admin@mail.ru", PassWord = "1234" };
                user.PassWord = AuthOptions.GetHash(user.PassWord);
                context.UserData.Add(user);
                context.SaveChanges();
            }
        }
    }
}

