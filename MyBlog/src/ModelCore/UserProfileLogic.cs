using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelCore
{
    public interface IUserProfileLogic
    {
        User GetUserProfile(string id);
     
        Task<int> ChangeUserProfile(User user);
    }

    public class UserProfileLogic:IUserProfileLogic
    {
        DataBaseContext db;

        public UserProfileLogic(DataBaseContext dbContext)
        {
            db = dbContext;
        }

        public async Task<int> ChangeUserProfile(User user)
        {
            db.Users.Update(user);
            return await db.SaveChangesAsync();
        }

      


    

        public User GetUserProfile(string id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
