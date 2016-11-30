using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Model
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

      

        public bool ProverkaSvobodenliEmail(string email)
        {
            if (db.Users.FirstOrDefault(u => u.Email == email) == null)
                return true;
            else
                return false;
        }

        public bool ProverkaSvobodenliUserName(string userName)
        {
            if (db.Users.FirstOrDefault(u => u.Name == userName) == null)
                return true;
            else
                return false;
        }

        public bool ProverkaProviniiPassword(string email, string password)
        {
            if (db.Users.FirstOrDefault(u => u.Email == email && u.Security.Password.Equals(password)) != null)
                return true;
            else
                return false;
        }

    }
}
