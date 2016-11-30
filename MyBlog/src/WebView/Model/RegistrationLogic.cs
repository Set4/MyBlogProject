using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Model
{
    //отправка подтверждения регистрации почта, телеграм(bot)
   public interface ICodeSender
    {
        bool SendRegistrationCode(string Code);
    }


    public interface IRegistrationLogic
    {
        bool PreRegistration(string name, string email, string password);
        Task<bool> EndRegistration(string code);
        bool ProverkaSvobodenliEmail(string email);
        bool ProverkaSvobodenliUserName(string userName);
    }

    public class RegistrationLogic:IRegistrationLogic
    {
        string accesCode;
        const int sizeCode = 5;
      
        _user user;

        DataBaseContext db;

        ICodeSender sender;

        UserProfileLogic userProfile;

        public  class _user
        {
            public string name { get; private set; }
            public string email { get; private set; }
            public string password { get; private set; }

            public _user(string name, string email, string password)
            {
                this.name = name;
                this.email = email;
                this.password = password;
            }
        }


        public RegistrationLogic(DataBaseContext dbContext, ICodeSender sender)
        {
            db = dbContext;
            this.sender = sender;

            userProfile = new UserProfileLogic(db);
        }



        public bool PreRegistration(string name, string email, string password)
        {
            user = new _user(name, email, password);

            accesCode = CenerateCode();

            if (!OtpravkaCoda(accesCode))
                return false;

            return true;
        }

        public async Task<bool> EndRegistration(string code)
        {
            if (ProverkaCoda(code) && !String.IsNullOrEmpty(await CreateUser(user)))
                return true;
            else
            {
                user = null;
                return false;
            }      
        }

        public bool ProverkaSvobodenliEmail(string email)
        {
            return userProfile.ProverkaSvobodenliEmail(email);
        }

        public bool ProverkaSvobodenliUserName(string userName)
        {
            return userProfile.ProverkaSvobodenliUserName(userName);
        }





        private string CenerateCode()
        {
            return CodeGenerator.RandomString(sizeCode);
        }

        private bool OtpravkaCoda(string code)
        {
           return sender.SendRegistrationCode(code);
        }


        private bool ProverkaCoda(string Code)
        {
            return accesCode.Equals(Code);
        }










        private async Task<string> CreateUser(_user user)
        {

           string id= await CreateUserProfile(user.name, user.email);
            if (!await SavePasswordCode(id, user.password))
                return null;
            return id;
        }

        //сохраняет уже хеш пароля который сгенерирован js функцией на клиенте
        private async Task<bool> SavePasswordCode(string userId, string password)
        {
            User user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return false;

            user.Security =new Security(userId, password);
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return true;
        }

        private async Task<string> CreateUserProfile(string name, string email)
        {
            User newUser = new User(name, email);
            db.Users.Add(newUser);
            await db.SaveChangesAsync();
            return newUser.Id;
        }
    }
}
