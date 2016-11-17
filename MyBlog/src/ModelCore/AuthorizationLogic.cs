using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelCore
{
    public interface IAuthorizationLogic
    {
        Task<string> Authorization(string email, string password);
        Task<string> Authorization(string email, string password, string capctha);

        User Autondification(string email, string AuthToken);
    }

    public class AuthorizationLogic:IAuthorizationLogic
    {
        //int numberAttempts;
        string CAPCTHA;

        DataBaseContext db;

        UserProfileLogic userProfileLogic;

        public AuthorizationLogic(DataBaseContext dbContext)
        {
            db = dbContext;
           //numberAttempts = 0;

            userProfileLogic = new UserProfileLogic(db);
        }

       

        private bool ProverkaCapcha(string capcha)
        {
            return CAPCTHA.Equals(capcha);
        }

        public async Task<string> Authorization(string email, string password)
        {
            if (userProfileLogic.ProverkaProviniiPassword(email, password))
                return await GetAuthToken(email);

            return null;
        }

        public async Task<string> Authorization(string email, string password, string capctha)
        {
            if(ProverkaCapcha(capctha) && Authorization(email, password)!=null)
                 return await GetAuthToken(email);

            return null;
        }

        private async Task<string> GetAuthToken(string email)
        {
           string AUTHToken= SequentialGuidGenerator.CreateGuid().ToString();
           User user=db.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return null;

            user.Access.Token = AUTHToken;
            await userProfileLogic.ChangeUserProfile(user);

            return AUTHToken;
        }

        public User Autondification(string email, string AuthToken)
        {
            return db.Users.FirstOrDefault(u => u.Email == email && u.Access.Token.Equals(AuthToken));
        }
    }
}
