using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelCore
{
   public interface ICodeSender
    {
        void Send(string Code);
    }

    public class RegistrationLogic
    {
        private string accesCode;

        DataBaseContext db;

        ICodeSender sender;

        public RegistrationLogic(DataBaseContext dbContext, ICodeSender sender)
        {
            db = dbContext;
            this.sender = sender;
        }

        public bool Registration()
        {
            accesCode = CenerateCode();
        } 

        private string CenerateCode()
        {

        }
            
        public bool ProverkaSvobodenliEmail(string email)
        { }

        public bool ProverkaSvobodenliUserName(string userName)
        { }

        public void OtpravkaPoEmailCoda(string code)
        {
            string message = "sdg" + code;
            sender.Send(message);
        }

        public bool ProverkaCoda(string Code)
        {

        }
    }
}
